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
        #endregion

        #region get function
        public GameObject GetAttackLaserEffectObject() { return m_attackLaserEffectObject; }

        #endregion
    }
}
