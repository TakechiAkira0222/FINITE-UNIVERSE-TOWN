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
        [SerializeField, Range( 1.0f, 10.0f),  Header(" �ˌ��̈З́@�ʏ�U�� ")]
        private float m_normalShootingForce = 3.0f;
        [SerializeField, Range( 1.0f, 10.0f), Header(" �e�̑��ݎ��� �ʏ� ")]
        private float m_normalDurationOfBullet = 5;
        [SerializeField, Range( 1.0f, 10.0f),  Header(" �ˌ��̈З́@�K�E�Z ")]
        private float m_deathblowShootingForce = 3.0f;
        [SerializeField, Range( 3.0f, 10.0f), Header(" �e�̑��ݎ��� �K�E�Z ")]
        private float m_deathblowDurationOfBullet = 5;
        [SerializeField, Range(10, 30), Header(" EnemySearch �p������")]
        private float m_enemySearch_Seconds;
        [SerializeField, Range(20, 50), Header(" Wall�̑��ݎ���")]
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
