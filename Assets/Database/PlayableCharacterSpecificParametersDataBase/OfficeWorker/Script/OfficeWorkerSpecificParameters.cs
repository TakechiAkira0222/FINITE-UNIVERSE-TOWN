using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificParameters.OfficeWorker
{
    [Serializable]
    [CreateAssetMenu(fileName = "OfficeWorkerSpecificParameters", menuName = "OfficeWorkerSpecificParameters")]
    public class OfficeWorkerSpecificParameters : ScriptableObject
    {
        #region SerializeField
        [Header("=== DeathblowStatusIncreaseSetting ===")]
        [SerializeField, Range(10.0f, 30.0f), Tooltip("必殺技のステータス上昇幅　AttackPower")]
        private float m_attackPowerIncrease  = 30.0f;
        [SerializeField, Range(3.0f, 10.0f),  Tooltip("必殺技のステータス上昇幅　MoveingSpeed")]
        private float m_moveingSpeedIncrease = 30.0f;
        [SerializeField, Range(30.0f, 200.0f),Tooltip("必殺技のステータス上昇幅　JumpPower")]
        private float m_jumpPowerIncrease    = 300.0f;
        [SerializeField, Tooltip("必殺技の継続時間　Seconds")]
        private float m_specialMoveDuration_Seconds = 30;
        [SerializeField, Tooltip("飛ぶ力")]
        private float m_flyingForce = 500;
        [SerializeField, Tooltip("投げる力")]
        private float m_throwForce = 500;
        [SerializeField, Range(10.0f, 30.0f) ,Tooltip("質量の回復量")]
        private int   m_amountOfMassRecovered = 10;

        #endregion

        #region GetStatusFunction
        public float GetAttackPowerIncrease() { return m_attackPowerIncrease; }
        public float GetMoveingSpeedIncrease() { return m_moveingSpeedIncrease; }
        public float GetJumpPowerIncrease() { return m_jumpPowerIncrease; }
        public float GetSpecialMoveDuration_Seconds() {return m_specialMoveDuration_Seconds;}
        public float GetFlyingForce() { return m_flyingForce; }
        public float GetThrowForce() { return m_throwForce; }
        public int   GetAmountOfMassRecovered() { return m_amountOfMassRecovered; }

        #endregion
    }
}
