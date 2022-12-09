using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    /// <summary>
    /// パラメータの中間管理を行うクラス
    /// </summary>
    public class CharacterStatusManagement : MonoBehaviour
    {
        [SerializeField] private PlayableCharacterParameters m_characterParameters;
        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private Rigidbody  m_rb;
        [SerializeField] private Collider   m_collider;

        /// <summary>
        /// 移動スピード
        /// </summary>
        private float m_movingSpeed;
        public float MovingSpeed => m_movingSpeed;
        /// <summary>
        /// 攻撃力
        /// </summary>
        private float m_attackPower;
        public float AttackPower => m_attackPower;
        /// <summary>
        /// ジャンプ力
        /// </summary>
        private float m_jumpPower;
        public float JumpPower => m_jumpPower;
        /// <summary>
        /// 質量
        /// </summary>
        public float CleanMass => m_characterParameters.GetCleanMass();
        /// <summary>
        /// PhotonView
        /// </summary>
        public PhotonView PhotonView => m_photonView;
        /// <summary>
        /// Rigidbody
        /// </summary>
        public Rigidbody Rigidbody => m_rb;
        /// <summary>
        /// Collider
        /// </summary>
        public Collider Collider => m_collider;

        private void Awake()
        {
            if (!m_photonView.IsMine) return;
                
            settingCharacterParameters();
        }

        #region AwakeSetting

        private void settingCharacterParameters()
        {
            m_movingSpeed = m_characterParameters.GetSpeed();
            m_attackPower = m_characterParameters.GetAttackPower();
            m_jumpPower = m_characterParameters.GetJumpPower();
            m_rb.mass = m_characterParameters.GetCleanMass();

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" m_movingSpeed = {m_movingSpeed}\n" +
                      $" m_attackPower = {m_attackPower}\n" +
                      $" m_jumpPower = {m_jumpPower}\n" +
                      $" m_mass = {m_rb.mass}\n"
                      );
        }

        #endregion

        #region setFunction

        public float setSpeed(int changeValue)
        {
            return m_movingSpeed + changeValue;
        }

        public float setAttackPower(int changeValue)
        {
            return m_attackPower + changeValue;
        }

        public float setJumpPower(int changeValue)
        {
            return m_jumpPower + changeValue;
        }

        public float setCleanMass(int changeValue)
        {
            return m_rb.mass + changeValue;
        }

        #endregion

        #region UpdateFunction

        public float updateSpeed(int changeValue)
        {
            Debug.Log($" updateSpeed {m_movingSpeed} : {changeValue}");
            return m_movingSpeed += changeValue;
        }

        public float updateAttackPower(int changeValue)
        {
            Debug.Log($" updateAttackPower {m_attackPower} : {changeValue}");
            return m_attackPower += changeValue;
        }

        public float updateJumpPower(int changeValue)
        {
            Debug.Log($" updateJumpPower {m_jumpPower} : {changeValue}");
            return m_jumpPower += changeValue;
        }

        public float updateMass(float changeValue)
        {
            Debug.Log($"{m_rb.mass} : {changeValue}");

            if (m_rb.mass + changeValue <= 1)
            {
                m_rb.mass = 1;
                return 1;
            }
            else
            {
                return m_rb.mass += changeValue;
            }
        }

        #endregion
    }
}
