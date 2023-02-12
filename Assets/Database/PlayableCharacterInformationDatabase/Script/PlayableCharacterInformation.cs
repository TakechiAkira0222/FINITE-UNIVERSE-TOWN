using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using static Takechi.CharacterController.Information.PlayableCharacterInformation;

namespace Takechi.CharacterController.Information
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayableCharacterInformation", menuName = "PlayableCharacterData/PlayableCharacterInformation")]
    public class PlayableCharacterInformation : ScriptableObject
    {
        public enum FeaturesOfCharacter
        {
            AttackType,
            DefenseType,
            SpeedType,
            HealerType,
        }

        public enum TypeOfAttack 
        {
            AttackTypeOfSword,
            AttackTypeOfGun,
        }

        #region SerializeField
        [SerializeField, Tooltip("キャラクター 特徴")]
        private FeaturesOfCharacter m_featuresOfCharacter;
        [SerializeField, Tooltip("キャラクター 攻撃種類")]
        private TypeOfAttack        m_attackType;

        [Header(" character infomation ")]
        [SerializeField, Tooltip("キャラクター 名前")]
        private string m_characterName = "nullname";
        [SerializeField, Tooltip("キャラクター アイコン")]
        private Sprite m_characterIcon;
        [SerializeField, Tooltip("キャラクター 攻撃種類 アイコン")]
        private Sprite m_characterAttackTypeIcon;
        [SerializeField, Tooltip("キャラクター 特徴 アイコン")]
        private Sprite m_characterFeaturesIcon;
        [SerializeField, Tooltip("キャラクター 説明")]
        private string m_characterExplanation = "説明";

        [Header(" deathblow infomation ")]
        [SerializeField, Tooltip("必殺技 名前")]
        private string m_deathblowName = "";
        [SerializeField, Tooltip("必殺技 アイコン")]
        private Sprite m_deathblowIcon;
        [SerializeField, Tooltip("必殺技 情報")]
        private string m_deathblowExplanation = "説明";

        [Header(" ability1 infomation ")]
        [SerializeField, Tooltip("アビリティー1 名前")]
        private string m_ability1Name = "";
        [SerializeField, Tooltip("アビリティー1 アイコン")]
        private Sprite m_ability1Icon;
        [SerializeField, Tooltip("アビリティー1 情報")]
        private string m_ability1Explanation = "説明";

        [Header(" ability2 infomation ")]
        [SerializeField, Tooltip("アビリティー2 名前")]
        private string m_ability2Name = "";
        [SerializeField, Tooltip("アビリティー2 アイコン")]
        private Sprite m_ability2Icon;
        [SerializeField, Tooltip("アビリティー2 説明")]
        private string m_ability2Explanation = "説明";

        [Header(" ability3 infomation ")]
        [SerializeField, Tooltip("アビリティー3 名前")]
        private string m_ability3Name = "";
        [SerializeField, Tooltip("アビリティー3 アイコン")]
        private Sprite m_ability3Icon;
        [SerializeField, Tooltip("アビリティー3 説明")]
        private string m_ability3Explanation = "説明";
        #endregion

        #region GetFunction
        /// <summary>
        /// キャラクター　特徴
        /// </summary>
        /// <returns></returns>
        public FeaturesOfCharacter GetFeaturesOfCharacter() { return m_featuresOfCharacter; }
        /// <summary>
        /// キャラクター 攻撃種類
        /// </summary>
        /// <returns></returns>
        public TypeOfAttack GetTypeOfAttack() { return m_attackType; }
        /// <summary>
        /// キャラクター 名前
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return m_characterName; }
        /// <summary>
        /// キャラクター アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterIcon() { return m_characterIcon; }
        /// <summary>
        /// キャラクター 攻撃種類 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterAttackTypeIcon() { return m_characterAttackTypeIcon; }
        /// <summary>
        /// キャラクター 特徴 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterFeaturesIcon() { return m_characterFeaturesIcon; }
        /// <summary>
        /// キャラクター 説明
        /// </summary>
        /// <returns></returns>
        public string GetCharacterExplanation() { return m_characterExplanation; }
        /// <summary>
        /// 必殺技　名前
        /// </summary>
        /// <returns></returns>
        public string GetdeathblowName() { return m_deathblowName; }
        /// <summary>
        /// 必殺技 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetdeathblowIcon() { return m_deathblowIcon; }
        /// <summary>
        /// 必殺技 説明
        /// </summary>
        /// <returns></returns>
        public string GetdeathblowExplanation() { return m_deathblowExplanation; }
        /// <summary>
        /// アビリティー1 名前
        /// </summary>
        /// <returns></returns>
        public string GetAbility1Name() { return m_ability1Name; }
        /// <summary>
        /// アビリティー1 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility1Icon() { return m_ability1Icon; }
        /// <summary>
        /// アビリティー1 説明
        /// </summary>
        /// <returns></returns>
        public string GetAbility1Explanation() { return m_ability1Explanation; }
        /// <summary>
        /// アビリティー2 名前
        /// </summary>
        /// <returns></returns>
        public string GetAbility2Name() { return m_ability2Name; }
        /// <summary>
        /// アビリティー2 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility2Icon() { return m_ability1Icon; }
        /// <summary>
        /// アビリティー2 説明
        /// </summary>
        /// <returns></returns>
        public string GetAbility2Explanation() { return m_ability2Explanation; }
        /// <summary>
        /// アビリティー3 名前
        /// </summary>
        /// <returns></returns>
        public string GetAbility3Name() { return m_ability3Name; }
        /// <summary>
        /// アビリティー3 アイコン
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility3Icon() { return m_ability3Icon; }
        /// <summary>
        /// アビリティー3 説明
        /// </summary>
        /// <returns></returns>
        public string GetAbility3Explanation() { return m_ability3Explanation; }

        #endregion
    }
}