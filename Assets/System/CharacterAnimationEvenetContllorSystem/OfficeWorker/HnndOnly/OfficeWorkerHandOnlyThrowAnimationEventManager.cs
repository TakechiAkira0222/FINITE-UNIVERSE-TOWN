using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;

using UnityEngine;

namespace Takechi.CharacterController.ThrowAnimationEvent
{
    public class OfficeWorkerHandOnlyThrowAnimationEventManager : AnimationEventManagement
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
        private GameObject throwInstans => addressManagement.GetThrowInstans();
        private Transform  throwTransfrom => addressManagement.GetThrowTransform();
        private string throwInstansPath => addressManagement.GetThrowInstansFolderNamePath();
        private float  force => statusManagement.GetThrowForce();

        #endregion
        /// <summary>
        /// OfficeWorker Throw Start
        /// </summary>
        void OfficeWorkerThrowStart()
        {
            keyInputStateManagement.SetOperation(false);
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);
        }

        /// <summary>
        /// OfficeWorker Throw Timing
        /// </summary>
        void OfficeWorkerThrowTiming()
        {
            throwing( throwTransfrom, force);
        }

        /// <summary>
        /// OfficeWorker Throw End
        /// </summary>
        void OfficeWorkerThrowEnd()
        {
            keyInputStateManagement.SetOperation(true);
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
        }

        private void throwing( Transform throwTransform, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( throwInstansPath + throwInstans.name, throwTransform.position, Quaternion.identity);

            instans.GetComponent<Rigidbody>().AddForce( (throwTransform.forward + throwTransform.up) * force, ForceMode.Impulse);

            // PhotonNetwork.Destroy(instans);
        }
    }
}
