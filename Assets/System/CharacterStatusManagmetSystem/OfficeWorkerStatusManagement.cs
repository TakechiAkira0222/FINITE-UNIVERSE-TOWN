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
        [SerializeField] private OfficeWorkerSpecificParameters m_officeWorkerSpecificParameters;

        /// <summary>
        /// 必殺技のステータス上昇幅　AttackPower
        /// </summary>
        private float m_attackPowerIncrease;
        /// <summary>
        /// 必殺技のステータス上昇幅　MoveingSpeed
        /// </summary>
        private float m_moveingSpeedIncrease;
        /// <summary>
        /// 必殺技のステータス上昇幅　JumpPower
        /// </summary>
        private float m_jumpPowerIncrease;
        /// <summary>
        /// 必殺技の継続時間
        /// </summary>
        private float m_specialMoveDuration_Seconds;

        protected override void Awake()
        {
            base.Awake();

            if (!base.m_photonView.IsMine) return;

            setupOfficeWorkerSpecificParameters();
        }

        #region set up function

        /// <summary>
        /// SpecificStatus を、データベースの変数で設定します。
        /// </summary>
        private void setupOfficeWorkerSpecificParameters()
        {
            SetMoveingSpeedIncrease(m_officeWorkerSpecificParameters.GetMoveingSpeedIncrease());
            SetJumpPowerIncrease(m_officeWorkerSpecificParameters.GetJumpPowerIncrease());
            SetAttackPowerIncrease(m_officeWorkerSpecificParameters.GetAttackPowerIncrease());
            SetSpecialMoveDuration_Seconds(m_officeWorkerSpecificParameters.GetSpecialMoveDuration_Seconds());

            Debug.Log($"<color=green> setupOfficeWorkerSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : { PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_moveingSpeedIncrease = { m_movingSpeed}\n" +
                      $" m_jumpPowerIncrease    = { m_attackPower}\n" +
                      $" m_attackPowerIncrease  = { m_rb.mass}\n"
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
            Debug.Log($" SpecialMoveDuration_Seconds {m_specialMoveDuration_Seconds} = {changeValue}");
            m_specialMoveDuration_Seconds = changeValue;
        }

        #endregion

        #region GetFunction

        public float GetAttackPowerIncrease() { return m_attackPowerIncrease; }
        public float GetMoveingSpeedIncrease() { return m_moveingSpeedIncrease; }
        public float GetJumpPowerIncrease() { return m_jumpPowerIncrease; }
        public float GetSpecialMoveDuration_Seconds() { return m_specialMoveDuration_Seconds; }

        #endregion
    }
}

