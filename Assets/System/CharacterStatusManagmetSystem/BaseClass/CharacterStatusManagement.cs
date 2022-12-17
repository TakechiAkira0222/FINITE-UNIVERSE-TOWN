using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Takechi.ScriptReference.CustomPropertyKey;
using TakechiEngine.PUN.CustomProperties;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    /// <summary>
    /// パラメータの中間管理を行うクラス
    /// </summary>
    public class CharacterStatusManagement : TakechiPunCustomProperties
    {
        #region SerializeField
        /// <summary>
        /// マスターデータ
        /// </summary>
        [SerializeField] private PlayableCharacterParameters m_characterParameters;
        /// <summary>
        /// this photonViwe
        /// </summary>
        [SerializeField] private PhotonView m_thisPhotonView;
        /// <summary>
        /// main avater
        /// </summary>
        [SerializeField] private GameObject m_avater;
        /// <summary>
        /// main colloder
        /// </summary>
        [SerializeField] private Collider m_mainCollider;
        /// <summary>
        /// main rb
        /// </summary>
        [SerializeField] private Rigidbody m_rb;
        /// <summary>
        /// main camera
        /// </summary>
        [SerializeField] private Camera m_mainCamera;
        /// <summary>
        /// hand only animetor
        /// </summary>
        [SerializeField] private Animator m_handOnlyAnimator;
        /// <summary>
        /// network model animator
        /// </summary>
        [SerializeField] private Animator m_networkModelAnimator;

        #endregion

        #region protected member variable
        /// <summary>
        /// this photonViwe
        /// </summary>
        protected PhotonView thisPhotonView => m_thisPhotonView;
        /// <summary>
        /// main avater
        /// </summary>
        protected GameObject avater => m_avater;
        /// <summary>
        /// main colloder
        /// </summary>
        protected Collider mainCollider => m_mainCollider;
        /// <summary>
        /// main rb
        /// </summary>
        protected Rigidbody rb => m_rb;
        /// <summary>
        /// main camera
        /// </summary>
        protected Camera mainCamera => m_mainCamera;
        /// <summary>
        /// hand only animetor
        /// </summary>
        protected Animator handOnlyAnimator => m_handOnlyAnimator;
        /// <summary>
        /// network model animator
        /// </summary>
        protected Animator networkModelAnimator => m_networkModelAnimator;
        /// <summary>
        /// local player custom properties
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_localPlayerCustomProperties =
              new ExitGames.Client.Photon.Hashtable();
        /// <summary>
        /// 移動スピード
        /// </summary>
        protected float m_movingSpeed;
        /// <summary>
        /// 横移動の移動量 割合
        /// </summary>
        protected float m_lateralMovementRatio;
        /// <summary>
        /// 攻撃力
        /// </summary>
        protected float m_attackPower;
        /// <summary>
        /// ジャンプ力
        /// </summary>
        protected float m_jumpPower;
        /// <summary>
        /// 干渉を受けていない質量
        /// </summary>
        protected float m_cleanMass => m_characterParameters.GetCleanMass();

        #endregion

        protected virtual void Awake()
        {
            if (!thisPhotonView.IsMine) return;

            setupCharacterParameters();

            setupLocalPlayerCustomProperties();
        }

        #region set up function

        /// <summary>
        /// CharacterStatus を、データベースの変数で設定します。
        /// </summary>
        private void setupCharacterParameters()
        {
            SetMovingSpeed( m_characterParameters.GetSpeed());
            SetLateralMovementRatio( m_characterParameters.GetLateralMovementRatio());
            SetAttackPower( m_characterParameters.GetAttackPower());
            SetJumpPower( m_characterParameters.GetJumpPower());
            SetMass( m_characterParameters.GetCleanMass());

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_movingSpeed = {m_movingSpeed}\n" +
                      $" m_attackPower = {m_attackPower}\n" +
                      $" m_jumpPower = {m_jumpPower}\n" +
                      $" m_mass = {m_rb.mass}\n"
                      );
        }

        /// <summary>
        /// LocalPlayerCustomPropertiesを、データベースの変数で設定します。
        /// </summary>
        private void setupLocalPlayerCustomProperties()
        {
            m_localPlayerCustomProperties =
               new ExitGames.Client.Photon.Hashtable
               {
                    { CustomPropertyKeyReference.s_CharacterStatusAttackPower , m_characterParameters.GetAttackPower()},
                    { CustomPropertyKeyReference.s_CharacterStatusMass , m_characterParameters.GetCleanMass()},
               };

            setLocalPlayerCustomProperties(m_localPlayerCustomProperties);

            Debug.Log($"<color=green> setLocalPlayerCustomProrerties </color>\n" +
                   $"<color=blue> info</color>\n" +
                   $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                   $" {CustomPropertyKeyReference.s_CharacterStatusAttackPower} = {m_localPlayerCustomProperties[CustomPropertyKeyReference.s_CharacterStatusAttackPower]}\n" +
                   $" {CustomPropertyKeyReference.s_CharacterStatusMass} = {m_localPlayerCustomProperties[CustomPropertyKeyReference.s_CharacterStatusMass]}\n"
                   );
        }

        #endregion

        #region SetFunction

        public void SetMovingSpeed(float changeValue) { m_movingSpeed = changeValue; }
        public void SetLateralMovementRatio(float changeValue){ m_lateralMovementRatio = changeValue;}
        public void SetAttackPower(float changeValue) { m_attackPower = changeValue; }
        public void SetJumpPower(float changeValue) { m_jumpPower = changeValue; }
        public void SetMass(float changeValue) { rb.mass = changeValue; }
        public void SetVelocity(Vector3 velocityValue) { rb.velocity = velocityValue; }

        #endregion

        #region GetFunction

        public bool  GetIsGrounded()
        {
            Ray ray =
                   new Ray( rb.gameObject.transform.position + new Vector3( 0, 0.1f),
                            Vector3.down * 0.1f);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 0.1f))
            {
                Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * 0.1f, Color.red);
                return false;
            }
        }
        public float GetMovingSpeed() { return m_movingSpeed; }
        public float GetLateralMovementRatio() { return m_lateralMovementRatio; }
        public float GetAttackPower() { return m_attackPower; }
        public float GetJumpPower() { return m_jumpPower; }
        public float GetCleanMass() { return m_characterParameters.GetCleanMass(); }
        public PhotonView GetMyPhotonView() { return thisPhotonView; }
        public Rigidbody  GetMyRigidbody() { return rb; }
        public Collider   GetMyCollider() { return mainCollider; }
        public GameObject GetMyAvater() { return avater; }
        public Camera     GetMyMainCamera() { return mainCamera; }
        public Animator GetHandOnlyAnimater() { return handOnlyAnimator; }
        public Animator GetNetworkModelAnimator() { return networkModelAnimator; }

        #endregion

        #region UpdateFunction

        /// <summary>
        /// LocalPlayerCustomPropertiesを、status の変数で更新します。
        /// </summary>
        public void UpdateLocalPlayerCustomProrerties()
        {
            m_localPlayerCustomProperties =
              new ExitGames.Client.Photon.Hashtable
              {
                    {CustomPropertyKeyReference.s_CharacterStatusAttackPower, m_attackPower},
                    {CustomPropertyKeyReference.s_CharacterStatusMass , m_rb.mass},
              };

            setLocalPlayerCustomProperties(m_localPlayerCustomProperties);

            Debug.Log($"<color=green> updateLocalPlayerCustomProrerties </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" {CustomPropertyKeyReference.s_CharacterStatusAttackPower} = {m_localPlayerCustomProperties[CustomPropertyKeyReference.s_CharacterStatusAttackPower]}\n" +
                      $" {CustomPropertyKeyReference.s_CharacterStatusMass} = {m_localPlayerCustomProperties[CustomPropertyKeyReference.s_CharacterStatusMass]}\n"
                    );
        }

        public float UpdateMovingSpeed(float changeValue)
        {
            Debug.Log($" UpdateSpeed {m_movingSpeed} : { m_movingSpeed + changeValue}");
            return m_movingSpeed += changeValue;
        }

        public float UpdateAttackPower(float changeValue)
        {
            Debug.Log($" UpdateAttackPower {m_attackPower} : { m_attackPower + changeValue}");
            return m_attackPower += changeValue;
        }

        public float UpdateJumpPower(float changeValue)
        {
            Debug.Log($" UpdateJumpPower {m_jumpPower} : { m_jumpPower + changeValue}");
            return m_jumpPower += changeValue;
        }

        public float UpdateMass(float changeValue)
        {
            if (rb.mass + changeValue <= 1)
            {
                rb.mass = 1;
                Debug.Log($"{rb.mass} : {1}");
                return 1;
            }
            else
            {
                Debug.Log($"{rb.mass} : {rb.mass + changeValue}");
                return rb.mass += changeValue;
            }
        }

        #endregion

        #region ResetParameters

        /// <summary>
        /// CharacterStatus を、データベースの変数で設定します。
        /// </summary>
        public void ResetCharacterParameters()
        {
            SetMovingSpeed(m_characterParameters.GetSpeed());
            SetLateralMovementRatio(m_characterParameters.GetLateralMovementRatio());
            SetAttackPower(m_characterParameters.GetAttackPower());
            SetJumpPower(m_characterParameters.GetJumpPower());
            SetMass(m_characterParameters.GetCleanMass());

            Debug.Log($"<color=green> settingCharacterParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : {PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_movingSpeed = {m_movingSpeed}\n" +
                      $" m_attackPower = {m_attackPower}\n" +
                      $" m_jumpPower = {m_jumpPower}\n" +
                      $" m_mass = {rb.mass}\n"
                      );
        }

        #endregion
    }
}
