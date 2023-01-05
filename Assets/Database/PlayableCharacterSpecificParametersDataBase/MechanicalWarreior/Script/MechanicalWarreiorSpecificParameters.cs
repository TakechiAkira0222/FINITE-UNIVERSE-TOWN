using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificParameters.MechanicalWarreior
{
    [Serializable]
    [CreateAssetMenu(fileName = "MechanicalWarreiorSpecificParameters", menuName = "MechanicalWarreiorSpecificParameters")]
    public class MechanicalWarreiorSpecificParameters : ScriptableObject
    {
        [Header("=== ShootingSetting ===")]
        [SerializeField, Tooltip("BulletsÇ÷ÇÃFolderñº")] 
        private List<string> m_bulletsFolderName = new List<string>();
        [SerializeField, Range(1.0f, 4.0f), Tooltip("éÀåÇÇÃà–óÕ")]
        private float m_shootingForce  = 3.0f;
        [SerializeField, Range(3.0f, 10.0f), Tooltip("íeÇÃë∂ç›éûä‘")]
        private float m_durationOfBullet = 5;

        #region GetStatusFunction

        public float GetShootingForce() { return m_shootingForce; }
        public float GetDurationOfBullet() { return m_durationOfBullet; }

        public string GetBulletsPath()
        {
            string path = "";
            foreach ( string s in m_bulletsFolderName) { path += s + "/"; }

            return path;
        }

        #endregion
    }
}
