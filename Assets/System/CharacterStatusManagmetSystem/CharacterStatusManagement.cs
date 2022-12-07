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
        [SerializeField] private Rigidbody m_rigidbody;

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

        private void Awake()
        {
            settingCharacterParameters();
        }

        #region AwakeSetting

        private void settingCharacterParameters()
        {
            m_movingSpeed = setSpeed(0);
            m_attackPower = setAttackPower(0);
            m_jumpPower = setJumpPower(0);
            m_rigidbody.mass = setMass(0);

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" m_movingSpeed = {m_movingSpeed}\n" +
                      $" m_attackPower = {m_attackPower}\n" +
                      $" m_jumpPower = {m_jumpPower}\n" +
                      $" m_mass = {m_rigidbody.mass}\n"
                      );
        }

        #endregion

        #region setFunction

        public float setSpeed(int changeValue)
        {
            return m_characterParameters.GetSpeed() + changeValue;
        }

        public float setAttackPower(int changeValue)
        {
            return m_characterParameters.GetAttackPower() + changeValue;
        }

        public float setJumpPower(int changeValue)
        {
            return m_characterParameters.GetJumpPower() + changeValue;
        }

        public float setMass(int changeValue)
        {
            return m_characterParameters.GetMass() + changeValue;
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
            Debug.Log($"{m_rigidbody.mass} : {changeValue}");

            if ( m_rigidbody.mass + changeValue <= 1)
            {
                m_rigidbody.mass = 1;
                return 1;
            }
            else
            {
                return m_rigidbody.mass += changeValue;
            }
        }

        #endregion
    }
}
