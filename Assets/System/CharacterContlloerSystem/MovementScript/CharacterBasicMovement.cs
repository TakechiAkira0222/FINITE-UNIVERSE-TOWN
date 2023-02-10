using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.Movement
{
    [RequireComponent(typeof(CharacterAddressManagement))]
    [RequireComponent(typeof(CharacterStatusManagement))]
    [RequireComponent(typeof(CharacterKeyInputStateManagement))]
    public class CharacterBasicMovement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        private PhotonView  myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody   myRb => addressManagement.GetMyRigidbody();
        private float       movementStatusSpeed => statusManagement.GetMovingSpeed();
        private float       lateralMovementRate => statusManagement.GetLateralMovementRatio();

        private float       m_movementSpeed;
        private Vector3     m_movementVector;
        private Vector3     m_movementVelocity;

        #endregion

        #region private const 

        private const float m_walkingSpeed = 1;
        private const float m_runningSpeed = 3.5f;

        #endregion

        #region UnityEvent

        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
            m_characterAddressManagement = this.transform.GetComponent<CharacterAddressManagement>();
            m_characterKeyInputStateManagement = this.transform.GetComponent<CharacterKeyInputStateManagement>();
        }

        private void Awake()
        {
            if (m_characterStatusManagement == null)
            {
                m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
                Debug.LogWarning(" m_characterStatusManagement It wasn't set, so I set it.");
            }

            SetMovementSpeed( movementStatusSpeed, m_walkingSpeed);
        }

        private void OnEnable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            keyInputStateManagement.InputToStartDash += (statusManagement, addressManagement) => { SetMovementSpeed(movementStatusSpeed, m_runningSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToStartDash function <color=green>to add.</color>");

            keyInputStateManagement.InputToStopDash += (statusManagement, addressManagement) => { SetMovementSpeed( movementStatusSpeed, m_walkingSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToStopDash function <color=green>to add.</color>");

            keyInputStateManagement.InputToMovement += (statusManagement, addressManagement, h, v) => { MovementControll(h, v); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToMovement function <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            keyInputStateManagement.InputToStartDash -= (statusManagement, addressManagement) => { SetMovementSpeed( movementStatusSpeed, m_runningSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");

            keyInputStateManagement.InputToStopDash -= (statusManagement, addressManagement) => { SetMovementSpeed( movementStatusSpeed, m_walkingSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");

            keyInputStateManagement.InputToMovement -= (statusManagement, addressManagement, h, v) => { MovementControll(h, v); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");
        }

        void FixedUpdate()
        {
            Vector3 velocityValue =
                new Vector3( m_movementVelocity.x * m_movementSpeed * lateralMovementRate,
                             myRb.velocity.y,
                             m_movementVelocity.z * m_movementSpeed);

            m_movementVelocity = Vector3.zero;

            statusManagement.SetMovingScalar(m_movementSpeed);
            statusManagement.SetMovingVector(velocityValue);
            statusManagement.SetVelocity(velocityValue);
        }

        #endregion

        #region SetProperty

        void SetMovementSpeed(float cleanSpeed, float force)
        {
            m_movementSpeed = cleanSpeed + force;
        }

        #endregion

        #region ControllFinctoin

        void MovementControll(float horizontalVec, float verticalVec)
        {
            m_movementVector = 
                new Vector3(horizontalVec, 0, verticalVec);

            m_movementVelocity =
                  myRb.gameObject.transform.forward * m_movementVector.z +
                  myRb.gameObject.transform.right * m_movementVector.x;

            m_movementVelocity.Normalize();
        }

        #endregion
    }
}