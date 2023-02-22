using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine.Playables;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class SlimeHandOnlyDeathblowAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== SlimeAddressManagement === ")]
        [SerializeField] private SlimeAddressManagement m_slimeAddressManagement;
        [Header("=== SlimeStatusManagement ===")]
        [SerializeField] private SlimeStatusManagement  m_slimeStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private SlimeSoundEffectsManagement m_slimeSoundEffectsManagement;

        #endregion

        #region private variable
        private SlimeAddressManagement addressManagement => m_slimeAddressManagement;
        private SlimeStatusManagement  statusManagement => m_slimeStatusManagement;
        private SlimeSoundEffectsManagement soundEffectsManagement => m_slimeSoundEffectsManagement;
        private GameObject myAvater => addressManagement.GetMyAvater();
        private PlayableDirector myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private PlayableAsset deathblowTimeline => addressManagement.GetDeathblowTimeline();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Animator   networkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private GameObject networkModelObject => addressManagement.GetNetworkModelObject();
        private Animator   handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject aiSlimeObjectInstans => addressManagement.GetAiSlimeObjectInstans();
        private string     aiSlimeObjectPath => addressManagement.GetAiSlimeObjectFolderPath();
        private int  dsuration => statusManagement.GetAiSlimeDsuration_Seconds();
        private bool isMine => myPhotonView.IsMine;
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;

        #endregion

        public void SlimeDeathblowStart()
        {
            PlayAndSettingOfPlayableDirector();

            keyInputStateManagement.SetOperation(false);

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            if (isMine)
            {
                handOnlyModelObject.SetActive(false);
                networkModelObject.SetActive(true);
            };
        }

        public void SlimeDeathblowInstantiate()
        {
            soundEffectsManagement.PlayOneShotDeathblowInstantiate();

            if (!isMine) return;

            AiSliemsInstantiate();
        }

        public void SlimeDeathblowEnd()
        {
            keyInputStateManagement.SetOperation(true);

            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            if (isMine)
            {
                handOnlyModelObject.SetActive(true);
                networkModelObject.SetActive(false);
            };
        }

        /// <summary>
        /// PlayableDirector çƒê∂Ç∆ê›íË
        /// </summary>
        private void PlayAndSettingOfPlayableDirector()
        {
            // set playableDirector
            myAvatarPlayableDirector.playableAsset = deathblowTimeline;
            Debug.Log(" myAvatarPlayableDirector.playableAsset deathblowTimeline to set.");

            // playableDirector
            myAvatarPlayableDirector.Play();
        }


        private void AiSliemsInstantiate()
        {
            GameObject instans = PhotonNetwork.Instantiate
                  (aiSlimeObjectPath + aiSlimeObjectInstans.name, myAvater.transform.position, myAvater.transform.rotation);

            StartCoroutine(DelayMethod(dsuration, () => { PhotonNetwork.Destroy(instans);}));
        }
    }
}
