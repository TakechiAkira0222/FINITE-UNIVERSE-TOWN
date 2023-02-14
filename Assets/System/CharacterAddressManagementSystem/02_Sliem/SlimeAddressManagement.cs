using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using UnityEngine;

namespace Takechi.CharacterController.Address
{
    public class SlimeAddressManagement : CharacterAddressManagement
    {
        #region serialize field
        [Header("=== SlimeAddressManagementSetting ===")]
        /// <summary>
        /// attackLaser point Transfrom 
        /// </summary>
        [SerializeField] private Transform    m_attackLaserPointTransfrom;
        /// <summary>
        /// attack laser Path
        /// </summary>
        [SerializeField] private List<string> m_attackLaserEffectFolderName = new List<string>();
        /// <summary>
        /// attack laser effect
        /// </summary>
        [SerializeField] private GameObject   m_attackLaserEffectInstans;
        /// <summary>
        /// trnado attack effect
        /// </summary>
        /// <returns></returns>
        [SerializeField] private GameObject m_trnadoEffectObject;
        /// <summary>
        /// stan effect array
        /// </summary>
        /// <returns></returns>
        [NamedArrayAttribute(new string[]{ "SliemAblity2Stan1Effect", "SliemAblity2Stan2Effect" })]
        [SerializeField] private GameObject[] m_stanEffectObjectArray;
        /// <summary>
        /// transparency paticleSystem
        /// </summary>
        [SerializeField] private ParticleSystem m_transparencyPaticleSystem;


        #endregion

        #region get function
        public Transform    GetAttackLaserPointTransfrom() => m_attackLaserPointTransfrom;
        public GameObject   GetAttackLaserEffectInstans() => m_attackLaserEffectInstans; 
        public GameObject   GetTrnadoEffectObject() => m_trnadoEffectObject;
        public GameObject[] GetStanEffectObjectArray() => m_stanEffectObjectArray;
        public ParticleSystem GetTransparencyPaticleSystem() => m_transparencyPaticleSystem;
        public string GetAttackLaserEffectPath()
        {
            string path = "";
            foreach (string s in m_attackLaserEffectFolderName) { path += s + "/"; }

            return path;
        }

        #endregion
    }
}
