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
        private PhotonView thisPhotonView => m_thisPhotonView;
        private GameObject deathblowAreaEffect => addressManagement.GetDeathblowAuraEffect4();
        private PlayableAsset      deathblowTimeline => addressManagement.GetDeathblowTimeline();
        private PlayableDirector   myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private Animator   handOnlyModelAnimator =>      addressManagement.GetHandOnlyModelAnimator();
        private Animator   networkModelAnimator =>   addressManagement.GetNetworkModelAnimator();
        private GameObject handOnlyModelObject  =>   addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject   =>   addressManagement.GetNetworkModelObject();
        /// <summary>
        /// �d�݂̈ꎞ�ۊ�
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;

        private void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => { resetOfficeWorkerDeathblowState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetOfficeWorkerDeathblowState</color>() <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => { resetOfficeWorkerDeathblowState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetOfficeWorkerDeathblowState</color>() <color=green>to remove.</color>");
        }

        #region UnityAnimatorEvent
        // <summary>
        // �K�E�Zanimation�̊J�n
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
            if (statusManagement.photonView.IsMine)
            {
                handOnlyModelObject.SetActive(false);
                networkModelObject.SetActive(true);
            }
        }

        /// <summary>
        /// �K�E�Z���т̊J�n
        /// </summary>
        void OfficeWorkerDeathblowCallOutVoice()
        {
            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfCallOut();
        }

        // <summary>
        // �K�E�Zanimation�̏I��
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
            if ( statusManagement.photonView.IsMine)
            {
                // effect 
                thisPhotonView.RPC(nameof(RPC_ActivationDeathblowAreaEffect), RpcTarget.AllBufferedViaServer);
                StartCoroutine(DelayMethod( statusManagement.GetDeathblowMoveDuration_Seconds(), () => thisPhotonView.RPC(nameof(RPC_ExitDeathblowAreaEffect), RpcTarget.AllBufferedViaServer)));

                handOnlyModelObject.SetActive(true);
                networkModelObject.SetActive(false);
            }
        }

        #endregion

        #region recursive function
        /// <summary>
        /// �X�e�[�^�X�̕ύX�@�J�n
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
        /// �X�e�[�^�X�ύX�@�I��
        /// </summary>
        private void ExitStatusManagement()
        {
            statusManagement.ResetCharacterParameters();

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }
        /// <summary>
        /// PlayableDirector �Đ��Ɛݒ�
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
        /// �K�E�Z��Ԃ̏�����
        /// </summary>
        private void resetOfficeWorkerDeathblowState()
        {
            deathblowAreaEffect.SetActive(false);
        }

        #endregion

        #region RPC function
        /// <summary>
        /// �K�E�Zarea�G�t�F�N�g�@�J�n
        /// </summary>
        [PunRPC]
        private void RPC_ActivationDeathblowAreaEffect()
        {
            deathblowAreaEffect.SetActive(true);
        }

        /// <summary>
        /// �K�E�ZArea�G�t�F�N�g�@�I��
        /// </summary>
        [PunRPC]
        private void RPC_ExitDeathblowAreaEffect()
        {
            deathblowAreaEffect.SetActive(false);
        }

        #endregion
    }
}
