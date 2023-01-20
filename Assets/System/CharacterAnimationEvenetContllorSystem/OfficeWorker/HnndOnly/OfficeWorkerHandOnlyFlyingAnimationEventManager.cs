using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;
using UnityEditor;
using UnityEngine;

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
        }
    }
}
