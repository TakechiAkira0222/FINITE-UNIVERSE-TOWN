using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using UnityEngine;

namespace Takechi.CharacterController.Address
{
    public class MechanicalWarriorAddressManagement : CharacterAddressManagement
    {
        [Header("=== MechanicalWarriorAddressSetting ===")]
        /// <summary>
        /// magazine Transfrom 
        /// </summary>
        [SerializeField] private Transform m_magazineTransfrom;
        /// <summary>
        /// bullets Instans
        /// </summary>
        [SerializeField] private GameObject m_bulletsInstans;

        public Transform GetMagazineTransfrom() { return m_magazineTransfrom; }
        public GameObject GetBulletsInstans() { return m_bulletsInstans; }
    }
}

