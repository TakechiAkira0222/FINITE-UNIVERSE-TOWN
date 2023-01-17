using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        private float m_deathblowMoveDuration_Seconds;
        /// <summary>
        /// 飛行力
        /// </summary>
        private float m_ablity1FlyForce;

        protected override void Awake()
        {
            base.Awake();

            if (!base.thisPhotonView.IsMine) return;

            SetupOfficeWorkerSpecificParameters();
        }

        #region set up function

        /// <summary>
        /// SpecificStatus を、データベースの変数で設定します。
        /// </summary>
        private void SetupOfficeWorkerSpecificParameters()
        {
            SetMoveingSpeedIncrease(m_officeWorkerSpecificParameters.GetMoveingSpeedIncrease());
            SetJumpPowerIncrease(m_officeWorkerSpecificParameters.GetJumpPowerIncrease());
            SetAttackPowerIncrease(m_officeWorkerSpecificParameters.GetAttackPowerIncrease());
            SetSpecialMoveDuration_Seconds(m_officeWorkerSpecificParameters.GetSpecialMoveDuration_Seconds());
            SetAblity1FlyForce(m_officeWorkerSpecificParameters.GetAblity1FlyForce());

            Debug.Log($"<color=green> setupOfficeWorkerSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : { PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_moveingSpeedIncrease = { m_moveingSpeedIncrease}\n" +
                      $" m_jumpPowerIncrease    = { m_jumpPowerIncrease}\n" +
                      $" m_attackPowerIncrease  = { m_attackPowerIncrease}\n"+
                      $" m_ablity1FlyForce      = { m_ablity1FlyForce}\n"
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
        public void SetAblity1FlyForce(float changeValue)
        {
            m_ablity1FlyForce = changeValue;
            Debug.Log($" Ablity1FlyForce {m_ablity1FlyForce} = {changeValue}");
        }

        #endregion

        #region GetFunction
        public float GetAttackPowerIncrease() { return m_attackPowerIncrease; }
        public float GetMoveingSpeedIncrease() { return m_moveingSpeedIncrease; }
        public float GetJumpPowerIncrease() { return m_jumpPowerIncrease; }
        public float GetDeathblowMoveDuration_Seconds() { return m_deathblowMoveDuration_Seconds; }
        public float GetAblity1FlyForce() { return m_ablity1FlyForce; }

        #endregion
    }
}

