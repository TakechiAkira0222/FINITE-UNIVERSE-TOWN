using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.Ablity1AnimationEvent
{
    public class OfficeWorkerHandOnlyAblity1AnimationEventManager : MonoBehaviour
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
        /// OfficeWorker Ablity1 Start
        /// </summary>
        void OfficeWorkerAblity1Start()
        {

        }

        /// <summary>
        /// OfficeWorker Ablity1 Fly
        /// </summary>
        void OfficeWorkerAblity1Fly()
        {
            soundEffectsManagement.PlayOneShotAblity1Fly();
            rb.AddForce( rb.transform.forward * ( m_characterStatusManagement.GetAblity1FlyForce() * 1000 * ( rb.mass / m_characterStatusManagement.GetCleanMass())), ForceMode.Impulse);
        }

        /// <summary>
        /// OfficeWorker Ablity1 End
        /// </summary>
        void OfficeWorkerAblity1End()
        {

        }
    }
}
