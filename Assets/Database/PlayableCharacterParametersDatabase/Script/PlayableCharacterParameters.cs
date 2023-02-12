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
        [SerializeField, Tooltip("�L�����N�^�[ ���O")]
        private string m_characterName = "nullname";
        [SerializeField, Tooltip("����")]
        private int m_cleanMass = 60;
        [SerializeField, Tooltip("���X�|�[������(�b)")]
        private int m_respawnTime_Seconds = 5;
        [SerializeField, Tooltip("�ړ��X�s�[�h")]
        private float m_movingSpeed = 2.5f;
        [SerializeField, Tooltip("���ړ��̈ړ��� ����")]
        private float m_lateralMovementRatio = 0.8f;
        [SerializeField, Tooltip("�U����")]
        private float m_attackPower = 30;
        [SerializeField, Tooltip("�W�����v�� ")]
        private float m_jumpPower = 300;
        [SerializeField, Tooltip("�A�r���e�B�[�P�̉񕜎���")]
        private float m_ability1_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("�A�r���e�B�[2�̉񕜎���")]
        private float m_ability2_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("�A�r���e�B�[3�̉񕜎���")]
        private float m_ability3_RecoveryTime_Seconds = 30;
        [SerializeField, Tooltip("�K�E�Z�񕜎���")]
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
        /// ���O
        /// </summary>
        /// <returns></returns>
        public string GetCharacterName() { return m_characterName; }
        /// <summary>
        /// �ړ��X�s�[�h
        /// </summary>
        public float  GetSpeed() { return m_movingSpeed; }
        /// <summary>
        /// ���ړ��̈ړ��� ����
        /// </summary>
        public float  GetLateralMovementRatio() { return m_lateralMovementRatio; }
        /// <summary>
        /// �U����
        /// </summary>
        /// <returns></returns>
        public float  GetAttackPower() { return m_attackPower; }
        /// <summary>
        /// �W�����v��
        /// </summary>
        /// <returns></returns>
        public float  GetJumpPower() { return m_jumpPower; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns> �����󂯂Ă��Ȃ����ʂ�Ԃ��܂��B</returns>
        public float  GetCleanMass() { return m_cleanMass; }
        /// <summary>
        /// ���X�|�[������
        /// </summary>
        /// <returns></returns>
        public int    GetRespawnTime_Seconds() { return m_respawnTime_Seconds; }
        /// <summary>
        /// �A�r���e�B�[1�̉񕜎���
        /// </summary>
        /// <returns></returns>
        public float  GetAbility1_RecoveryTime_Seconds() { return m_ability1_RecoveryTime_Seconds; }
        /// <summary>
        /// �A�r���e�B�[2�̉񕜎���
        /// </summary>
        /// <returns></returns>
        public float  GetAbility2_RecoveryTime_Seconds() { return m_ability2_RecoveryTime_Seconds; }
        /// <summary>
        /// �A�r���e�B�[3�̉񕜎���
        /// </summary>
        /// <returns></returns>
        public float  GetAbility3_RecoveryTime_Seconds() { return m_ability3_RecoveryTime_Seconds; }
        /// <summary>
        /// �K�E�Z�񕜎���
        /// </summary>
        /// <returns></returns>
        public float  GetDeathblow_RecoveryTime_Seconds() { return m_deathblow_RecoveryTime_Seconds; }

        #endregion
    }
}