using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.Address
{
    public class MechanicalWarriorAddressManagement : CharacterAddressManagement
    {
        #region serialize field
        [Header("=== MechanicalWarriorAddressSetting ===")]
        /// <summary>
        /// assaultRifle handOnlyModel GameObject 
        /// </summary>
        [SerializeField] private GameObject   m_handOnlyModelAssaultRifleObject;
        /// <summary>
        /// assaultRifle networkModel GameObject 
        /// </summary>
        [SerializeField] private GameObject   m_networkModelAssaultRifleObject;
        /// <summary>
        /// magazine Transfrom 
        /// </summary>
        [SerializeField] private Transform  Å@m_magazineTransfrom;
        /// <summary>
        /// wallInstans Transfrom 
        /// </summary>
        [SerializeField] private Transform    m_wallInstantiateTransfrom;
        /// <summary>
        /// throwSmokeInstans Transfrom 
        /// </summary>
        [SerializeField] private Transform    m_throwSmokeInstantiateTransfrom;
        /// <summary>
        /// Bullets Path
        /// </summary>
        [SerializeField] private List<string> m_normalBulletsFolderName = new List<string>();
        /// <summary>
        /// bullets Instans
        /// </summary>
        [SerializeField] private GameObject   m_normalBulletsInstans;
        /// <summary>
        /// Bullets Path
        /// </summary>
        [SerializeField] private List<string> m_deathblowBulletsFolderName = new List<string>();
        /// <summary>
        /// bullets Instans
        /// </summary>
        [SerializeField] private GameObject   m_deathblowBulletsInstans;
        /// <summary>
        /// Wall Path
        /// </summary>
        [SerializeField] private List<string> m_wallFolderName = new List<string>();
        /// <summary>
        /// Wall Instans
        /// </summary>
        [SerializeField] private GameObject   m_wallInstans;
        /// <summary>
        /// throwSmoke Instans Path
        /// </summary>
        [SerializeField] private List<string> m_throwSmokeInstansFolderName = new List<string>();
        /// <summary>
        /// throwSmoke Instans
        /// </summary>
        [SerializeField] private GameObject   m_throwSmokeInstans;
        /// <summary>
        /// deathblow Timeline
        /// </summary>
        [SerializeField] private PlayableAsset m_deathblowTimeline;

        #endregion

        #region get variable
        public PlayableAsset GetDeathblowTimeline() { return m_deathblowTimeline; }
        public GameObject GetNetworkModelAssaultRifleObject() { return m_networkModelAssaultRifleObject; }
        public GameObject GetHandOnlyModelAssaultRifleObject() { return m_handOnlyModelAssaultRifleObject; }
        public Transform  GetMagazineTransfrom() { return m_magazineTransfrom; }
        public Transform  GetWallInstantiateTransfrom() { return m_wallInstantiateTransfrom; }
        public Transform  GetThrowSmokeInstantiateTransfrom() { return m_throwSmokeInstantiateTransfrom; }
        public GameObject GetNormalBulletsInstans() { return m_normalBulletsInstans; }
        public GameObject GetDeathblowBulletsInstans() { return m_deathblowBulletsInstans; }
        public GameObject GetWallInstans() { return m_wallInstans; }
        public GameObject GetthrowSmokeInstans() { return m_throwSmokeInstans; }
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
        public string GetWallPath()
        {
            string path = "";
            foreach (string s in m_wallFolderName) { path += s + "/"; }

            return path;
        }
        public string GetThrowSmokePath()
        {
            string path = "";
            foreach (string s in m_throwSmokeInstansFolderName) { path += s + "/"; }

            return path;
        }

        #endregion
    }
}

