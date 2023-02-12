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
        [SerializeField, Tooltip("�L�����N�^�[ ����")]
        private FeaturesOfCharacter m_featuresOfCharacter;
        [SerializeField, Tooltip("�L�����N�^�[ �U�����")]
        private TypeOfAttack        m_attackType;

        [Header(" character infomation ")]
        [SerializeField, Tooltip("�L�����N�^�[ ���O")]
        private string m_characterName = "nullname";
        [SerializeField, Tooltip("�L�����N�^�[ �A�C�R��")]
        private Sprite m_characterIcon;
        [SerializeField, Tooltip("�L�����N�^�[ �U����� �A�C�R��")]
        private Sprite m_characterAttackTypeIcon;
        [SerializeField, Tooltip("�L�����N�^�[ ���� �A�C�R��")]
        private Sprite m_characterFeaturesIcon;
        [SerializeField, Tooltip("�L�����N�^�[ ����")]
        private string m_characterExplanation = "����";

        [Header(" deathblow infomation ")]
        [SerializeField, Tooltip("�K�E�Z ���O")]
        private string m_deathblowName = "";
        [SerializeField, Tooltip("�K�E�Z �A�C�R��")]
        private Sprite m_deathblowIcon;
        [SerializeField, Tooltip("�K�E�Z ���")]
        private string m_deathblowExplanation = "����";

        [Header(" ability1 infomation ")]
        [SerializeField, Tooltip("�A�r���e�B�[1 ���O")]
        private string m_ability1Name = "";
        [SerializeField, Tooltip("�A�r���e�B�[1 �A�C�R��")]
        private Sprite m_ability1Icon;
        [SerializeField, Tooltip("�A�r���e�B�[1 ���")]
        private string m_ability1Explanation = "����";

        [Header(" ability2 infomation ")]
        [SerializeField, Tooltip("�A�r���e�B�[2 ���O")]
        private string m_ability2Name = "";
        [SerializeField, Tooltip("�A�r���e�B�[2 �A�C�R��")]
        private Sprite m_ability2Icon;
        [SerializeField, Tooltip("�A�r���e�B�[2 ����")]
        private string m_ability2Explanation = "����";

        [Header(" ability3 infomation ")]
        [SerializeField, Tooltip("�A�r���e�B�[3 ���O")]
        private string m_ability3Name = "";
        [SerializeField, Tooltip("�A�r���e�B�[3 �A�C�R��")]
        private Sprite m_ability3Icon;
        [SerializeField, Tooltip("�A�r���e�B�[3 ����")]
        private string m_ability3Explanation = "����";
        #endregion

        #region GetFunction
        /// <summary>
        /// �L�����N�^�[�@����
        /// </summary>
        /// <returns></returns>
        public FeaturesOfCharacter GetFeaturesOfCharacter() { return m_featuresOfCharacter; }
        /// <summary>
        /// �L�����N�^�[ �U�����
        /// </summary>
        /// <returns></returns>
        public TypeOfAttack GetTypeOfAttack() { return m_attackType; }
        /// <summary>
        /// �L�����N�^�[ ���O
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return m_characterName; }
        /// <summary>
        /// �L�����N�^�[ �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterIcon() { return m_characterIcon; }
        /// <summary>
        /// �L�����N�^�[ �U����� �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterAttackTypeIcon() { return m_characterAttackTypeIcon; }
        /// <summary>
        /// �L�����N�^�[ ���� �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetCharacterFeaturesIcon() { return m_characterFeaturesIcon; }
        /// <summary>
        /// �L�����N�^�[ ����
        /// </summary>
        /// <returns></returns>
        public string GetCharacterExplanation() { return m_characterExplanation; }
        /// <summary>
        /// �K�E�Z�@���O
        /// </summary>
        /// <returns></returns>
        public string GetdeathblowName() { return m_deathblowName; }
        /// <summary>
        /// �K�E�Z �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetdeathblowIcon() { return m_deathblowIcon; }
        /// <summary>
        /// �K�E�Z ����
        /// </summary>
        /// <returns></returns>
        public string GetdeathblowExplanation() { return m_deathblowExplanation; }
        /// <summary>
        /// �A�r���e�B�[1 ���O
        /// </summary>
        /// <returns></returns>
        public string GetAbility1Name() { return m_ability1Name; }
        /// <summary>
        /// �A�r���e�B�[1 �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility1Icon() { return m_ability1Icon; }
        /// <summary>
        /// �A�r���e�B�[1 ����
        /// </summary>
        /// <returns></returns>
        public string GetAbility1Explanation() { return m_ability1Explanation; }
        /// <summary>
        /// �A�r���e�B�[2 ���O
        /// </summary>
        /// <returns></returns>
        public string GetAbility2Name() { return m_ability2Name; }
        /// <summary>
        /// �A�r���e�B�[2 �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility2Icon() { return m_ability1Icon; }
        /// <summary>
        /// �A�r���e�B�[2 ����
        /// </summary>
        /// <returns></returns>
        public string GetAbility2Explanation() { return m_ability2Explanation; }
        /// <summary>
        /// �A�r���e�B�[3 ���O
        /// </summary>
        /// <returns></returns>
        public string GetAbility3Name() { return m_ability3Name; }
        /// <summary>
        /// �A�r���e�B�[3 �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetAbility3Icon() { return m_ability3Icon; }
        /// <summary>
        /// �A�r���e�B�[3 ����
        /// </summary>
        /// <returns></returns>
        public string GetAbility3Explanation() { return m_ability3Explanation; }

        #endregion
    }
}