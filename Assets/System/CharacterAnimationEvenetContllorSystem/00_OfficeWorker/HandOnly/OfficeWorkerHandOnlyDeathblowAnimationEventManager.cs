using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Parameters;

using UnityEngine;
using UnityEngine.Playables;
using Photon.Pun;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using UnityEngine.Rendering;
using System.Data.SqlClient;
using Takechi.UI.GameLogTextScrollView;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerHandOnlyDeathblowAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private OfficeWorkerAddressManagement m_officeWorkerAddressManagement;
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement  m_officeWorkerStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView    m_thisPhotonView;

        #endregion
        private OfficeWorkerAddressManagement addressManagement => m_officeWorkerAddressManagement;
        private OfficeWorkerStatusManagement        statusManagement => m_officeWorkerStatusManagement;
        private OfficeWorkerSoundEffectsManagement  soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private GameLogTextScrollViewController logTextScrollViewController => addressManagement.GetGameLogTextScrollViewController();
        private PhotonView thisPhotonView => m_thisPhotonView;
        private GameObject deathblowAreaEffect => addressManagement.GetDeathblowAuraEffect4();
        private PlayableAsset      deathblowTimeline => addressManagement.GetDeathblowTimeline();
        private PlayableDirector   myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private Animator   handOnlyModelAnimator =>      addressManagement.GetHandOnlyModelAnimator();
        private Animator   networkModelAnimator =>   addressManagement.GetNetworkModelAnimator();
        private GameObject handOnlyModelObject  =>   addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject   =>   addressManagement.GetNetworkModelObject();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private bool       isMine => myPhotonView.IsMine;
        /// <summary>
        /// 重みの一時保管
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;

        public override void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => { resetOfficeWorkerDeathblowState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetOfficeWorkerDeathblowState</color>() <color=green>to add.</color>");
        }

        public override void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => { resetOfficeWorkerDeathblowState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetOfficeWorkerDeathblowState</color>() <color=green>to remove.</color>");
        }

        #region UnityAnimatorEvent
        // <summary>
        // 必殺技animationの開始
        // </summary>
        void OfficeWorkerDeathblowStart()
        {
            // play and set playableDirector
            PlayAndSettingOfPlayableDirector();

            // operation
            keyInputStateManagement.SetOperation(false);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            // rb
            statusManagement.SetIsKinematic(true);

            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfDeathblowStart();

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0.1f);

            // isMine active
            if (isMine)
            {
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
                handOnlyModelObject.SetActive(false);
                networkModelObject.SetActive(true);

                // logText
                if (statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                {
                    logTextScrollViewController.AddTextContent($"<color=red>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技発動");
                }
                else if ((statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamBName))
                {
                    logTextScrollViewController.AddTextContent($"<color=blue>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技発動");
                }
            }
        }

        /// <summary>
        /// 必殺技叫びの開始
        /// </summary>
        void OfficeWorkerDeathblowCallOutVoice()
        {
            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfCallOut();
        }

        // <summary>
        // 必殺技animationの終了
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            // operation
            keyInputStateManagement.SetOperation(true);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);

            // rb
            statusManagement.SetIsKinematic(false);

            // audioSource
            soundEffectsManagement.PlayOneShotPowerUp();

            // status
            ActivationStatusManagement();
            Invoke(nameof(ExitStatusManagement), statusManagement.GetDeathblowMoveDuration_Seconds());

            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);

            // isMine active
            if (isMine)
            {
                // effect 
                thisPhotonView.RPC(nameof(RPC_ActivationDeathblowAreaEffect), RpcTarget.AllBufferedViaServer);
                StartCoroutine(DelayMethod(statusManagement.GetDeathblowMoveDuration_Seconds(), () =>
                {
                    thisPhotonView.RPC(nameof(RPC_ExitDeathblowAreaEffect), RpcTarget.AllBufferedViaServer);

                    // logText
                    if (statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamAName)
                    {
                        logTextScrollViewController.AddTextContent($"<color=red>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技終了");
                    }
                    else if ((statusManagement.GetCustomPropertiesTeamName() == CharacterTeamStatusName.teamBName))
                    {
                        logTextScrollViewController.AddTextContent($"<color=blue>{PhotonNetwork.LocalPlayer.NickName}</color> 必殺技終了");
                    }
                }
                ));

                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
                handOnlyModelObject.SetActive(true);
                networkModelObject.SetActive(false);
            }
        }

        #endregion

        #region recursive function
        /// <summary>
        /// ステータスの変更　開始
        /// </summary>
        private void ActivationStatusManagement()
        {
            statusManagement.ResetCharacterParameters();

            statusManagement.UpdateAttackPower(statusManagement.GetAttackPowerIncrease());
            statusManagement.UpdateMovingSpeed(statusManagement.GetMoveingSpeedIncrease());
            statusManagement.UpdateJumpPower(statusManagement.GetJumpPowerIncrease());

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }
        /// <summary>
        /// ステータス変更　終了
        /// </summary>
        private void ExitStatusManagement()
        {
            statusManagement.ResetCharacterParameters();

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }
        /// <summary>
        /// PlayableDirector 再生と設定
        /// </summary>
        private void PlayAndSettingOfPlayableDirector()
        {
            // set playableDirector
            myAvatarPlayableDirector.playableAsset = deathblowTimeline;
            Debug.Log(" myAvatarPlayableDirector.playableAsset deathblowTimeline to set. ");

            // playableDirector
            myAvatarPlayableDirector.Play();
        }
        /// <summary>
        /// 必殺技状態の初期化
        /// </summary>
        private void resetOfficeWorkerDeathblowState()
        {
            deathblowAreaEffect.SetActive(false);
        }

        #endregion

        #region RPC function
        /// <summary>
        /// 必殺技areaエフェクト　開始
        /// </summary>
        [PunRPC]
        private void RPC_ActivationDeathblowAreaEffect()
        {
            deathblowAreaEffect.SetActive(true);
        }

        /// <summary>
        /// 必殺技Areaエフェクト　終了
        /// </summary>
        [PunRPC]
        private void RPC_ExitDeathblowAreaEffect()
        {
            deathblowAreaEffect.SetActive(false);
        }

        #endregion
    }
}
