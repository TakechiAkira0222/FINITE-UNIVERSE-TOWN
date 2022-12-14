using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Takechi.ScriptReference.CustomPropertyKey;
using TakechiEngine.PUN.CustomProperties;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    /// <summary>
    /// パラメータの中間管理を行うクラス
    /// </summary>
    public class CharacterStatusManagement : TakechiPunCustomProperties
    {
        [SerializeField] private PlayableCharacterParameters m_characterParameters;
        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private Rigidbody  m_rb;
        [SerializeField] private Collider   m_collider;

        /// <summary>
        /// プレイヤーのカスタムプロパティー
        /// </summary>
        private ExitGames.Client.Photon.Hashtable m_customPlayerProperties =
            new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// 移動スピード
        /// </summary>
        private float m_movingSpeed;
        public float MovingSpeed => m_movingSpeed;
        /// <summary>
        /// 横移動の移動量 割合
        /// </summary>
        private float m_lateralMovementRatio;
        public float LateralMovementRatio => m_lateralMovementRatio;
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

            settingLocalPlayerCustomProperties();
        }

        #region AwakeSettingFunction

        /// <summary>
        /// CharacterStatus を、データベースの変数で設定します。
        /// </summary>
        private void settingCharacterParameters()
        {
            m_movingSpeed = m_characterParameters.GetSpeed();
            m_lateralMovementRatio = m_characterParameters.GetLateralMovementRatio();
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

        /// <summary>
        /// LocalPlayerCustomPropertiesを、データベースの変数で設定します。
        /// </summary>
        private void settingLocalPlayerCustomProperties()
        {
            m_customPlayerProperties =
               new ExitGames.Client.Photon.Hashtable
               {
                    { CustomPropertyKeyReference.s_CharacterStatusAttackPower , m_characterParameters.GetAttackPower()},
                    { CustomPropertyKeyReference.s_CharacterStatusMass , m_characterParameters.GetCleanMass()},
               };

            setLocalPlayerCustomProperties( m_customPlayerProperties );

            Debug.Log($"<color=green> setLocalPlayerCustomProrerties </color>\n" +
                   $"<color=blue> info</color>\n" +
                   $" {CustomPropertyKeyReference.s_CharacterStatusAttackPower} = {m_customPlayerProperties[CustomPropertyKeyReference.s_CharacterStatusAttackPower]}\n" +
                   $" {CustomPropertyKeyReference.s_CharacterStatusMass} = {m_customPlayerProperties[CustomPropertyKeyReference.s_CharacterStatusMass]}\n"
                   );
        }

        #endregion

        #region setFunction

        public float setSpeed(int changeValue)
        {
            return m_movingSpeed + changeValue;
        }

        public float setlateralMovementRatio(int changeValue)
        {
            return m_lateralMovementRatio + changeValue;
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

        /// <summary>
        /// LocalPlayerCustomPropertiesを、status の変数で更新します。
        /// </summary>
        public void updateLocalPlayerCustomProrerties()
        {
            m_customPlayerProperties =
              new ExitGames.Client.Photon.Hashtable
              {
                    {CustomPropertyKeyReference.s_CharacterStatusAttackPower, m_attackPower},
                    {CustomPropertyKeyReference.s_CharacterStatusMass , m_rb.mass},
              };

            setLocalPlayerCustomProperties(m_customPlayerProperties);

            Debug.Log($"<color=green> updateLocalPlayerCustomProrerties </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" {CustomPropertyKeyReference.s_CharacterStatusAttackPower} = {m_customPlayerProperties[CustomPropertyKeyReference.s_CharacterStatusAttackPower]}\n" +
                      $" {CustomPropertyKeyReference.s_CharacterStatusMass} = {m_customPlayerProperties[CustomPropertyKeyReference.s_CharacterStatusMass]}\n"
                    );
        }

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
