using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    public class OfficeWorkerStatusManagement : CharacterStatusManagement
    {
        [Header("=== OfficeWorkerStatus Setting===")]
        [SerializeField] private OfficeWorkerSpecificParameters m_officeWorkerSpecificParameters;
        /// <summary>
        /// �K�E�Z�̃X�e�[�^�X�㏸���@AttackPower
        /// </summary>
        private float m_attackPowerIncrease;
        /// <summary>
        /// �K�E�Z�̃X�e�[�^�X�㏸���@MoveingSpeed
        /// </summary>
        private float m_moveingSpeedIncrease;
        /// <summary>
        /// �K�E�Z�̃X�e�[�^�X�㏸���@JumpPower
        /// </summary>
        private float m_jumpPowerIncrease;
        /// <summary>
        /// �K�E�Z�̌p������
        /// </summary>
        private float m_deathblowMoveDuration_Seconds;
        /// <summary>
        /// ��s��
        /// </summary>
        private float m_flyingForce;
        /// <summary>
        /// ������З�
        /// </summary>
        private float m_throwForce;
        /// <summary>
        /// ���ʂ̉񕜗�
        /// </summary>
        private int   m_amountOfMassRecovered;

        protected override void Awake()
        {
            base.Awake();

            if (!base.thisPhotonView.IsMine) return;

            SetupOfficeWorkerSpecificParameters();
        }

        #region set up function

        /// <summary>
        /// SpecificStatus ���A�f�[�^�x�[�X�̕ϐ��Őݒ肵�܂��B
        /// </summary>
        private void SetupOfficeWorkerSpecificParameters()
        {
            SetMoveingSpeedIncrease(m_officeWorkerSpecificParameters.GetMoveingSpeedIncrease());
            SetJumpPowerIncrease(m_officeWorkerSpecificParameters.GetJumpPowerIncrease());
            SetAttackPowerIncrease(m_officeWorkerSpecificParameters.GetAttackPowerIncrease());
            SetSpecialMoveDuration_Seconds(m_officeWorkerSpecificParameters.GetSpecialMoveDuration_Seconds());
            SetFlyingForce(m_officeWorkerSpecificParameters.GetFlyingForce());
            SetThrowForce(m_officeWorkerSpecificParameters.GetThrowForce());
            SetAmountOfMassRecovered(m_officeWorkerSpecificParameters.GetAmountOfMassRecovered());

            Debug.Log($"<color=green> setupOfficeWorkerSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : { PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_moveingSpeedIncrease = { m_moveingSpeedIncrease}\n" +
                      $" m_jumpPowerIncrease    = { m_jumpPowerIncrease}\n" +
                      $" m_attackPowerIncrease  = { m_attackPowerIncrease}\n"+
                      $" m_flyingForce = {m_flyingForce}\n"+
                      $" m_throwForce  = {m_throwForce}\n"+
                      $" m_amountOfMassRecovered  = {m_amountOfMassRecovered}\n"
                      );
        }

        #endregion

        #region SetFunction
        public void SetAttackPowerIncrease(float changeValue)
        {
            m_attackPowerIncrease = changeValue;
            Debug.Log($" AttackPowerIncrease {m_attackPowerIncrease} = {changeValue}");
        }
        public void SetMoveingSpeedIncrease(float changeValue)
        {
            m_moveingSpeedIncrease = changeValue;
            Debug.Log($" MoveingSpeedIncrease {m_moveingSpeedIncrease} = {changeValue}");
        }
        public void SetJumpPowerIncrease(float changeValue)
        {
            m_jumpPowerIncrease = changeValue;
            Debug.Log($" JumpPowerIncrease {m_jumpPowerIncrease} = { changeValue}");
        }
        public void SetSpecialMoveDuration_Seconds(float changeValue)
        {
            m_deathblowMoveDuration_Seconds = changeValue;
            Debug.Log($" SpecialMoveDuration_Seconds {m_deathblowMoveDuration_Seconds} = {changeValue}");
        }
        public void SetFlyingForce(float changeValue)
        {
            m_flyingForce = changeValue;
            Debug.Log($" flyingForce {m_flyingForce} = {changeValue}");
        }
        public void SetThrowForce(float changeValue)
        {
            m_throwForce = changeValue;
            Debug.Log($" throwForce {m_throwForce} = {changeValue}");
        }
        public void SetAmountOfMassRecovered(int changeValue)
        {
            m_amountOfMassRecovered = changeValue;
            Debug.Log($" m_amountOfMassRecovered { m_amountOfMassRecovered} = {changeValue}");
        }

        #endregion

        #region GetFunction
        public float GetAttackPowerIncrease() { return m_attackPowerIncrease; }
        public float GetMoveingSpeedIncrease() { return m_moveingSpeedIncrease; }
        public float GetJumpPowerIncrease() { return m_jumpPowerIncrease; }
        public float GetDeathblowMoveDuration_Seconds() { return m_deathblowMoveDuration_Seconds; }
        public float GetFlyingForce() { return m_flyingForce; }
        public float GetThrowForce() {  return m_throwForce; }
        public int   GetAmountOfMassRecovered() { return m_amountOfMassRecovered; }

        #endregion
    }
}
