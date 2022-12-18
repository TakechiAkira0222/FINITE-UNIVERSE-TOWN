using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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

            Debug.Log($"<color=green> setupOfficeWorkerSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : { PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_moveingSpeedIncrease = { m_moveingSpeedIncrease}\n" +
                      $" m_jumpPowerIncrease    = { m_jumpPowerIncrease}\n" +
                      $" m_attackPowerIncrease  = { m_attackPowerIncrease}\n"
                      );
        }

        #endregion

        #region SetFunction

        public void SetAttackPowerIncrease(float changeValue)
        {
            Debug.Log($" AttackPowerIncrease {m_attackPowerIncrease} = {changeValue}");
            m_attackPowerIncrease = changeValue;
        }

        public void SetMoveingSpeedIncrease(float changeValue)
        {
            Debug.Log($" MoveingSpeedIncrease {m_moveingSpeedIncrease} = {changeValue}");
            m_moveingSpeedIncrease = changeValue;
        }

        public void SetJumpPowerIncrease(float changeValue)
        {
            Debug.Log($" JumpPowerIncrease {m_jumpPowerIncrease} = { changeValue}");
            m_jumpPowerIncrease = changeValue;
        }

        public void SetSpecialMoveDuration_Seconds(float changeValue)
        {
            Debug.Log($" SpecialMoveDuration_Seconds {m_deathblowMoveDuration_Seconds} = {changeValue}");
            m_deathblowMoveDuration_Seconds = changeValue;
        }

        #endregion

        #region GetFunction
        public float GetAttackPowerIncrease() { return m_attackPowerIncrease; }
        public float GetMoveingSpeedIncrease() { return m_moveingSpeedIncrease; }
        public float GetJumpPowerIncrease() { return m_jumpPowerIncrease; }
        public float GetDeathblowMoveDuration_Seconds() { return m_deathblowMoveDuration_Seconds; }

        #endregion
    }
}

