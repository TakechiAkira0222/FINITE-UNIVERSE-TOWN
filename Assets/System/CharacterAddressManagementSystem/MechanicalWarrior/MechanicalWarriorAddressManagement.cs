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
        /// Bullets Path
        /// </summary>
        [SerializeField] private List<string> m_normalBulletsFolderName = new List<string>();
        /// <summary>
        /// bullets Instans
        /// </summary>
        [SerializeField] private GameObject m_normalBulletsInstans;
        /// <summary>
        /// Bullets Path
        /// </summary>
        [SerializeField] private List<string> m_deathblowBulletsFolderName = new List<string>();
        /// <summary>
        /// bullets Instans
        /// </summary>
        [SerializeField] private GameObject m_deathblowBulletsInstans;

        public Transform  GetMagazineTransfrom() { return m_magazineTransfrom; }
        public GameObject GetNormalBulletsInstans() { return m_normalBulletsInstans; }
        public GameObject GetDeathblowBulletsInstans() { return m_deathblowBulletsInstans; }
        public string GetNormalBulletsPath()
        {
            string path = "";
            foreach (string s in m_normalBulletsFolderName) { path += s + "/"; }

            return path;
        }
        public string GetDeathblowBulletsPath()
        {
            string path = "";
            foreach (string s in m_deathblowBulletsFolderName) { path += s + "/"; }

            return path;
        }
    }
}

