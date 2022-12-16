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
    /// �p�����[�^�̒��ԊǗ����s���N���X
    /// </summary>
    public class CharacterStatusManagement : TakechiPunCustomProperties
    {
        #region SerializeField
        /// <summary>
        /// �}�X�^�[�f�[�^
        /// </summary>
        [SerializeField] private PlayableCharacterParameters m_characterParameters;
        /// <summary>
        /// this photonViwe
        /// </summary>
        [SerializeField] protected PhotonView m_photonView;
        /// <summary>
        /// main avater
        /// </summary>
        [SerializeField] protected GameObject m_avater;
        /// <summary>
        /// main colloder
        /// </summary>
        [SerializeField] protected Collider m_collider;
        /// <summary>
        /// main rb
        /// </summary>
        [SerializeField] protected Rigidbody m_rb;
        /// <summary>
        /// main camera
        /// </summary>
        [SerializeField] protected Camera m_mainCamera;

        #endregion

        #region protected member variable

        /// <summary>
        /// local player custom properties
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_localPlayerCustomProperties =
              new ExitGames.Client.Photon.Hashtable();
        /// <summary>
        /// �ړ��X�s�[�h
        /// </summary>
        protected float m_movingSpeed;
        /// <summary>
        /// ���ړ��̈ړ��� ����
        /// </summary>
        protected float m_lateralMovementRatio;
        /// <summary>
        /// �U����
        /// </summary>
        protected float m_attackPower;
        /// <summary>
        /// �W�����v��
        /// </summary>
        protected float m_jumpPower;
        /// <summary>
        /// ����
        /// </summary>
        protected float m_cleanMass => m_characterParameters.GetCleanMass();

        #endregion

        protected virtual void Awake()
        {
            if (!m_photonView.IsMine) return;

            setupCharacterParameters();

            setupLocalPlayerCustomProperties();
        }

        #region set up function

        /// <summary>
        /// CharacterStatus ���A�f�[�^�x�[�X�̕ϐ��Őݒ肵�܂��B
        /// </summary>
        private void setupCharacterParameters()
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
                      $" m_mass = {m_rb.mass}\n"
                      );
        }

        /// <summary>
        /// LocalPlayerCustomProperties���A�f�[�^�x�[�X�̕ϐ��Őݒ肵�܂��B
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
        public void SetMass(float changeValue) { m_rb.mass = changeValue; }
        public void SetVelocity(Vector3 velocityValue) { m_rb.velocity = velocityValue; }

        #endregion

        #region GetFunction

        public bool  GetIsGrounded()
        {
            Ray ray =
                   new Ray( m_rb.gameObject.transform.position + new Vector3( 0, 0.1f),
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
        public PhotonView GetMyPhotonView() { return m_photonView; }
        public Rigidbody  GetMyRigidbody() { return m_rb; }
        public Collider   GetMyCollider() { return m_collider; }
        public GameObject GetMyAvater() { return m_avater; }
        public Camera     GetMyMainCamera() { return m_mainCamera; }

        #endregion

        #region UpdateFunction

        /// <summary>
        /// LocalPlayerCustomProperties���Astatus �̕ϐ��ōX�V���܂��B
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
            if (m_rb.mass + changeValue <= 1)
            {
                m_rb.mass = 1;
                Debug.Log($"{m_rb.mass} : {1}");
                return 1;
            }
            else
            {
                Debug.Log($"{m_rb.mass} : {m_rb.mass + changeValue}");
                return m_rb.mass += changeValue;
            }
        }

        #endregion

        #region ResetParameters

        /// <summary>
        /// CharacterStatus ���A�f�[�^�x�[�X�̕ϐ��Őݒ肵�܂��B
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
                      $" m_mass = {m_rb.mass}\n"
                      );
        }

        #endregion
    }
}
