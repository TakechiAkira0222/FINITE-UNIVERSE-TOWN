using UnityEngine;
using UnityEngine.Playables;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.PrayingAnimationEvent
{
    public class OfficeWorkerHandOnlyPrayingAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== OfficeWorkerAddressManagement === ")]
        [SerializeField] private OfficeWorkerAddressManagement      m_officeWorkerAddressManagement;
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement       m_officeWorkerStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;
        #endregion

        #region private variable
        private OfficeWorkerAddressManagement addressManagement => m_officeWorkerAddressManagement;
        private OfficeWorkerSoundEffectsManagement soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private OfficeWorkerStatusManagement statusManagement => m_officeWorkerStatusManagement;
        private int amountOfMassRecovered => statusManagement.GetAmountOfMassRecovered();
        private PlayableAsset    prayingTimeline => addressManagement.GetPrayingTimeline();
        private PlayableDirector myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private Animator handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private Animator networkModelAnimator  => addressManagement.GetNetworkModelAnimator();
        private GameObject networkModelObject  => addressManagement.GetNetworkModelObject();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject handOnlyModelSword  => addressManagement.GetHandOnlyModelSwordObject();
        private GameObject networkModelSword   => addressManagement.GetNetworkModelSwordObject();
        /// <summary>
        /// 重みの一時保管
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;

        #endregion
        /// <summary>
        /// OfficeWorker Praying Start
        /// </summary>
        void OfficeWorkerPrayingStart()
        {
            // sword setActive
            setSwordSetActive(false);

            // sound
            soundEffectsManagement.PlayOneShotPraySound();

            // set operation
            keyInputStateManagement.SetOperation(false);

            // play and set playableDirector
            playAndSettingOfPlayableDirector();

            // save animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));

            // set animation weiht
            SetLayerWeight(networkModelAnimator,  AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            // set animation Controler
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            // isMine active
            if (statusManagement.photonView.IsMine)
            {
                changeModelObjectSetActive();
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
            }
        }

        /// <summary>
        /// OfficeWorker Throw End
        /// </summary>
        void OfficeWorkerPrayingEnd()
        {
            updateStatusManagement();

            // sword setActive
            setSwordSetActive(true);

            // set operation
            keyInputStateManagement.SetOperation(true);

            // set animation weiht
            SetLayerWeight( networkModelAnimator,  AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight( handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            // set animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
         
            // isMine active
            if (statusManagement.photonView.IsMine)
            {
                changeModelObjectSetActive();
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
            }
        }

        #region private finction
        private void setSwordSetActive(bool flag)
        {
            handOnlyModelSword.SetActive(flag);
            networkModelSword.SetActive(flag);
        }

        private void changeModelObjectSetActive()
        {
            handOnlyModelObject.SetActive(!handOnlyModelObject.activeSelf);
            networkModelObject.SetActive(!networkModelObject.activeSelf);
        }

        /// <summary>
        /// ステータスの変更　開始
        /// </summary>
        private void updateStatusManagement()
        {
            statusManagement.UpdateMass( amountOfMassRecovered);
            statusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// PlayableDirector 再生と設定
        /// </summary>
        private void playAndSettingOfPlayableDirector()
        {
            myAvatarPlayableDirector.playableAsset = prayingTimeline;
            Debug.Log(" myAvatarPlayableDirector.playableAsset prayingTimeline to set.");

            myAvatarPlayableDirector.Play();
        }

        #endregion
    }
}
