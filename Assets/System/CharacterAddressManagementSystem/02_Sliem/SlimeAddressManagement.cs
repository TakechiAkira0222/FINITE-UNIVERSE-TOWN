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
        /// attack laser effect
        /// </summary>
        [SerializeField] private GameObject m_attackLaserEffectObject;
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

        #endregion

        #region get function
        public GameObject GetAttackLaserEffectObject() { return m_attackLaserEffectObject; }

        public GameObject GetTrnadoEffectObject() { return m_trnadoEffectObject; }

        public GameObject[] GetStanEffectObjectArray() { return m_stanEffectObjectArray; }

        #endregion
    }
}
