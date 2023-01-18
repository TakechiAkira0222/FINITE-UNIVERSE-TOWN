using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;
using UnityEngine;

namespace Takechi.CharacterController.Address
{
    public class OfficeWorkerAddressManagement : CharacterAddressManagement
    {
        [Header("=== OfficeWorkerAddressSetting ===")]
        /// <summary>
        /// swordObject Transfrom 
        /// </summary>
        [SerializeField] private GameObject m_swordObject;
        /// <summary>
        /// swordObject EffectTrail
        /// </summary>
        [SerializeField] private GameObject m_swordEffectTrail;

        public GameObject GetSwordObject() { return m_swordObject; }
        public GameObject GetSwordEffectTrail() { return m_swordEffectTrail; }
    }
}

