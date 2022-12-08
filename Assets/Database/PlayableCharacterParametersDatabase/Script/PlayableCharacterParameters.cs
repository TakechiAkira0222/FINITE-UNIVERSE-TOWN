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
        /// �����̎��
        /// </summary>
        [SerializeField]
        private TypeOfCharacterParameters m_typeOfParameters;
        private TypeOfCharacterParameters typeOfParameters => m_typeOfParameters;

        /// <summary>
        /// �A�C�R��
        /// </summary>
        [SerializeField]
        private Sprite m_icon;
        private Sprite icon => m_icon;

        /// <summary>
        /// ���O
        /// </summary>
        [SerializeField]
        private string m_characterName = "nullname";
        private string characterName => m_characterName;

        /// <summary>
        /// ���
        /// </summary>
        [SerializeField]
        private string m_information = "������o��";
        private string information => m_information;

        /// <summary>
        /// ����
        /// </summary>
        [SerializeField]
        private int m_cleanMass = 60;
        private int cleanMass => m_cleanMass;

        /// <summary>
        /// �ړ��X�s�[�h
        /// </summary>
        [SerializeField]
        private float m_movingSpeed = 2.5f;
        private float movingSpeed => m_movingSpeed;

        /// <summary>
        /// �U����
        /// </summary>
        [SerializeField]
        private float m_attackPower = 30;
        private float attackPower => m_attackPower;

        /// <summary>
        /// �W�����v�� 
        /// </summary>
        [SerializeField]
        private float m_jumpPower = 300;
        private float jumpPower => m_jumpPower;

        /// <summary>
        /// �A�r���e�B�[�P�̉񕜎���
        /// </summary>
        [SerializeField]
        private float m_ability1_RecoveryTime_Seconds = 30;
        private float ability1_RecoveryTime_Seconds => m_ability1_RecoveryTime_Seconds;

        /// <summary>
        /// �A�r���e�B�[2�̉񕜎���
        /// </summary>
        [SerializeField]
        private float m_ability2_RecoveryTime_Seconds = 30;
        private float ability2_RecoveryTime_Seconds => m_ability2_RecoveryTime_Seconds;

        /// <summary>
        /// �A�r���e�B�[3�̉񕜎���
        /// </summary>
        [SerializeField]
        private float m_ability3_RecoveryTime_Seconds = 30;
        private float ability3_RecoveryTime_Seconds => m_ability3_RecoveryTime_Seconds;

        /// <summary>
        /// �K�E�Z�񕜎���
        /// </summary>
        [SerializeField]
        private float m_specialMoveRecoveryTime_Seconds = 60;
        private float specialMoveRecoveryTime_Seconds => m_specialMoveRecoveryTime_Seconds;

        #region GetFunction

        /// <summary>
        /// �����̎��
        /// </summary>
        /// <returns></returns>
        public TypeOfCharacterParameters GetTypeOfParameters() { return typeOfParameters; }
        /// <summary>
        /// �A�C�R��
        /// </summary>
        /// <returns></returns>
        public Sprite GetIcon() { return icon; }
        /// <summary>
        /// ���O
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return characterName; }
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public string GetInformation() { return information; }
        /// <summary>
        /// �ړ��X�s�[�h
        /// </summary>
        public float GetSpeed() { return movingSpeed; }
        /// <summary>
        /// �U����
        /// </summary>
        /// <returns></returns>
        public float GetAttackPower() { return attackPower; }
        /// <summary>
        /// �W�����v��
        /// </summary>
        /// <returns></returns>
        public float GetJumpPower() { return jumpPower; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns> �����󂯂Ă��Ȃ����ʂ�Ԃ��܂��B</returns>
        public float GetCleanMass() { return cleanMass; }

        #endregion
    }
}