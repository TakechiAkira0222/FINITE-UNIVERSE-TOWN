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
        [SerializeField, Range( 1.0f, 10.0f),  Header(" ËŒ‚‚ÌˆĞ—Í@’ÊíUŒ‚ ")]
        private float m_normalShootingForce = 3.0f;
        [SerializeField, Range( 1.0f, 10.0f), Header(" ’e‚Ì‘¶İŠÔ ’Êí ")]
        private float m_normalDurationOfBullet = 5;
        [SerializeField, Range( 1.0f, 10.0f),  Header(" ËŒ‚‚ÌˆĞ—Í@•KE‹Z ")]
        private float m_deathblowShootingForce = 3.0f;
        [SerializeField, Range( 3.0f, 10.0f), Header(" ’e‚Ì‘¶İŠÔ •KE‹Z ")]
        private float m_deathblowDurationOfBullet = 5;
        [SerializeField, Range(10, 30), Header(" EnemySearch Œp‘±ŠÔ")]
        private float m_enemySearch_Seconds;
        [SerializeField, Range(20, 50), Header(" Wall‚Ì‘¶İŠÔ")]
        private float m_wallDuration_Seconds;

        #region GetStatusFunction
        public float GetNormalShootingForce() { return m_normalShootingForce; }
        public float GetNormalDurationOfBullet() { return m_normalDurationOfBullet; }
        public float GetDeathblowShootingForce() { return m_deathblowShootingForce; }
        public float GetDeathblowDurationOfBullet() { return m_deathblowDurationOfBullet; }
        public float GetEnemySearch_Seconds() { return m_enemySearch_Seconds; }
        public float GetWallDuration_Seconds() { return m_wallDuration_Seconds; }

        #endregion
    }
}
