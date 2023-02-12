using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;
using UnityEditor;
using UnityEngine;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.FlyingAnimationEvent
{
    public class OfficeWorkerHandOnlyFlyingAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_characterStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private OfficeWorkerSoundEffectsManagement soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private Rigidbody rb => addressManagement.GetMyRigidbody();
        private Animator networkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private Animator handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;

        #endregion
        /// <summary>
        /// OfficeWorker Flying Start
        /// </summary>
        void OfficeWorkerFlyingStart()
        {
            // operation
            keyInputStateManagement.SetOperation(false);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            // animation weiht
            m_networkModelAnimatorWeight  = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator,  AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
        }

        /// <summary>
        /// OfficeWorker Flying Point
        /// </summary>
        void OfficeWorkerFlyingPoint()
        {
            soundEffectsManagement.PlayOneShotFlying();
            rb.AddForce( rb.transform.forward * ( m_characterStatusManagement.GetFlyingForce() * 100 * ( rb.mass / m_characterStatusManagement.GetCleanMass())), ForceMode.Impulse);
        }

        /// <summary>
        /// OfficeWorker Flying End
        /// </summary>
        void OfficeWorkerFlyingEnd()
        {
            // operation
            keyInputStateManagement.SetOperation(true);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);

            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer,  m_networkModelAnimatorWeight);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
        }
    }
}
