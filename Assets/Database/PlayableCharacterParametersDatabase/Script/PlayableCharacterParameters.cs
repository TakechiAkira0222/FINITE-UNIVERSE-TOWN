using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

namespace Takechi.CharacterController.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayableCharacterParameters", menuName = "PlayableCharacterParameters")]
    public class PlayableCharacterParameters : ScriptableObject
    {
        public enum TypeOfCharacterParameters
        {
            AttackType,
            DefenseType,
            SpeedType,
            HealerType,
        }

        /// <summary>
        /// 特徴の種類
        /// </summary>
        [SerializeField]
        private TypeOfCharacterParameters m_typeOfParameters;
        private TypeOfCharacterParameters typeOfParameters => m_typeOfParameters;

        /// <summary>
        /// アイコン
        /// </summary>
        [SerializeField]
        private Sprite m_icon;
        private Sprite icon => m_icon;

        /// <summary>
        /// 名前
        /// </summary>
        [SerializeField]
        private string m_characterName = "nullname";
        private string characterName => m_characterName;

        /// <summary>
        /// 情報
        /// </summary>
        [SerializeField]
        private string m_information = "特徴や経歴";
        private string information => m_information;

        /// <summary>
        /// 質量
        /// </summary>
        [SerializeField]
        private int m_cleanMass = 60;
        private int cleanMass => m_cleanMass;

        /// <summary>
        /// 移動スピード
        /// </summary>
        [SerializeField]
        private float m_movingSpeed = 2.5f;
        private float movingSpeed => m_movingSpeed;

        /// <summary>
        /// 攻撃力
        /// </summary>
        [SerializeField]
        private float m_attackPower = 30;
        private float attackPower => m_attackPower;

        /// <summary>
        /// ジャンプ力 
        /// </summary>
        [SerializeField]
        private float m_jumpPower = 300;
        private float jumpPower => m_jumpPower;

        /// <summary>
        /// アビリティー１の回復時間
        /// </summary>
        [SerializeField]
        private float m_ability1_RecoveryTime_Seconds = 30;
        private float ability1_RecoveryTime_Seconds => m_ability1_RecoveryTime_Seconds;

        /// <summary>
        /// アビリティー2の回復時間
        /// </summary>
        [SerializeField]
        private float m_ability2_RecoveryTime_Seconds = 30;
        private float ability2_RecoveryTime_Seconds => m_ability2_RecoveryTime_Seconds;

        /// <summary>
        /// アビリティー3の回復時間
        /// </summary>
        [SerializeField]
        private float m_ability3_RecoveryTime_Seconds = 30;
        private float ability3_RecoveryTime_Seconds => m_ability3_RecoveryTime_Seconds;

        /// <summary>
        /// 必殺技回復時間
        /// </summary>
        [SerializeField]
        private float m_specialMoveRecoveryTime_Seconds = 60;
        private float specialMoveRecoveryTime_Seconds => m_specialMoveRecoveryTime_Seconds;

        #region GetFunction

        /// <summary>
        /// 特徴の種類
        /// </summary>
        /// <returns></returns>
        public TypeOfCharacterParameters GetTypeOfParameters() { return typeOfParameters; }
        /// <summary>
        /// アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetIcon() { return icon; }
        /// <summary>
        /// 名前
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return characterName; }
        /// <summary>
        /// 情報
        /// </summary>
        /// <returns></returns>
        public string GetInformation() { return information; }
        /// <summary>
        /// 移動スピード
        /// </summary>
        public float GetSpeed() { return movingSpeed; }
        /// <summary>
        /// 攻撃力
        /// </summary>
        /// <returns></returns>
        public float GetAttackPower() { return attackPower; }
        /// <summary>
        /// ジャンプ力
        /// </summary>
        /// <returns></returns>
        public float GetJumpPower() { return jumpPower; }
        /// <summary>
        /// 質量
        /// </summary>
        /// <returns> 干渉を受けていない質量を返します。</returns>
        public float GetCleanMass() { return cleanMass; }

        #endregion
    }
}