using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.Ablity1AnimationEvent
{
    public class OfficeWorkerHandOnlyAblity1AnimationEventManager : MonoBehaviour
    {
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_characterStatusManagement;
        private Rigidbody m_rb => m_characterStatusManagement.GetMyRigidbody();
        // private Camera m_mainCamera => m_characterStatusManagement.GetMyMainCamera();

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
            m_rb.AddForce( m_rb.transform.forward * ( m_characterStatusManagement.GetAblity1FlyForce() * 1000 * ( m_rb.mass / m_characterStatusManagement.GetCleanMass())), ForceMode.Impulse);
        }

        /// <summary>
        /// OfficeWorker Ablity1 End
        /// </summary>
        void OfficeWorkerAblity1End()
        {

        }
    }
}
