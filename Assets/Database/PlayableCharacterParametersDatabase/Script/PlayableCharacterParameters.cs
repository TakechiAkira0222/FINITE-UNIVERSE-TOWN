using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

namespace Takechi.CharacterController.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayableCharacterParameters", menuName = "PlayableCharacterData/PlayableCharacterParameters")]
    public class PlayableCharacterParameters : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Tooltip("キャラクター 名前")]
        private string m_characterName = "nullname";
        [SerializeField, Tooltip("質量")]
        private int m_cleanMass = 60;
        [SerializeField, Tooltip("リスポーン時間(秒)")]
        private int m_respawnTime_Seconds = 5;
        [SerializeField, Tooltip("移動スピード")]
        private float m_movingSpeed = 2.5f;
        [SerializeField, Tooltip("横移動の移動量 割合")]
        private float m_lateralMovementRatio = 0.8f;
        [SerializeField, Tooltip("攻撃力")]
        private float m_attackPower = 30;
        [SerializeField, Tooltip("ジャンプ力 ")]
        private float m_jumpPower = 300;
        [SerializeField, Tooltip("アビリティー１の回復時間")]
        private float m_ability1_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("アビリティー2の回復時間")]
        private float m_ability2_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("アビリティー3の回復時間")]
        private float m_ability3_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("必殺技回復時間")]
        private float m_deathblow_RecoveryTime_Seconds = 60;

        #endregion

        public enum TypeOfCharacterParameters
        {
            AttackType,
            DefenseType,
            SpeedType,
            HealerType,
        }

        #region GetFunction
        /// <summary>
        /// 名前
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return m_characterName; }
        /// <summary>
        /// 移動スピード
        /// </summary>
        public float  GetSpeed() { return m_movingSpeed; }
        /// <summary>
        /// 横移動の移動量 割合
        /// </summary>
        public float  GetLateralMovementRatio() { return m_lateralMovementRatio; }
        /// <summary>
        /// 攻撃力
        /// </summary>
        /// <returns></returns>
        public float  GetAttackPower() { return m_attackPower; }
        /// <summary>
        /// ジャンプ力
        /// </summary>
        /// <returns></returns>
        public float  GetJumpPower() { return m_jumpPower; }
        /// <summary>
        /// 質量
        /// </summary>
        /// <returns> 干渉を受けていない質量を返します。</returns>
        public float  GetCleanMass() { return m_cleanMass; }
        /// <summary>
        /// リスポーン時間
        /// </summary>
        /// <returns></returns>
        public int    GetRespawnTime_Seconds() { return m_respawnTime_Seconds; }
        /// <summary>
        /// アビリティー1の回復時間
        /// </summary>
        /// <returns></returns>
        public float  GetAbility1_RecoveryTime_Seconds() { return m_ability1_RecoveryTime_Seconds; }
        /// <summary>
        /// アビリティー2の回復時間
        /// </summary>
        /// <returns></returns>
        public float  GetAbility2_RecoveryTime_Seconds() { return m_ability2_RecoveryTime_Seconds; }
        /// <summary>
        /// アビリティー3の回復時間
        /// </summary>
        /// <returns></returns>
        public float  GetAbility3_RecoveryTime_Seconds() { return m_ability3_RecoveryTime_Seconds; }
        /// <summary>
        /// 必殺技回復時間
        /// </summary>
        /// <returns></returns>
        public float  GetDeathblow_RecoveryTime_Seconds() { return m_deathblow_RecoveryTime_Seconds; }

        #endregion
    }
}