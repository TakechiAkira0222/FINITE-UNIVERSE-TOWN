using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using System.ComponentModel;
using UnityEngine.Playables;

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
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableAsset m_prayingTimeline;
        #endregion

        #region private variable
        private OfficeWorkerAddressManagement addressManagement => m_officeWorkerAddressManagement;
        private OfficeWorkerSoundEffectsManagement soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private OfficeWorkerStatusManagement statusManagement => m_officeWorkerStatusManagement;
        private PlayableDirector myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private Animator handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private Animator networkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private GameObject handOnlyModelSword => addressManagement.GetHandOnlyModelSwordObject();
        private GameObject networkModelSword => addressManagement.GetNetworkModelSwordObject();
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

            myAvatarPlayableDirector.playableAsset = m_prayingTimeline;
            myAvatarPlayableDirector.Play();

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            // animation Controler
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            // operation
            keyInputStateManagement.SetOperation(false);
        }

        /// <summary>
        /// OfficeWorker Throw End
        /// </summary>
        void OfficeWorkerPrayingEnd()
        {
            ActivationStatusManagement();

            // sword setActive
            setSwordSetActive(true);

            // animation weiht
            SetLayerWeight( networkModelAnimator,  AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight( handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();

            // operation
            keyInputStateManagement.SetOperation(true);
        }

        #region private finction
        private void setSwordSetActive(bool flag)
        {
            handOnlyModelSword.SetActive(flag);
            networkModelSword.SetActive(flag);
        }

        /// <summary>
        /// ステータスの変更　開始
        /// </summary>
        private void ActivationStatusManagement()
        {
            statusManagement.UpdateMass(10);

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// ステータス変更　終了
        /// </summary>
        private void ExitStatusManagement()
        {
            statusManagement.UpdateLocalPlayerCustomProrerties();
        }

        #endregion
    }
}
