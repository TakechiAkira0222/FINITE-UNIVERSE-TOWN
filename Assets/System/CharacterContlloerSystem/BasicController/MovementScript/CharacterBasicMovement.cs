using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.Movement
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicMovement : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        #endregion

        #region private
        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;
        private float       movementStatusSpeed => m_characterStatusManagement.GetMovingSpeed();
        private float       lateralMovementRate => m_characterStatusManagement.GetLateralMovementRatio();

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
            characterKeyInputStateManagement.InputToStartDash += (characterStatusManagement) => { SetMovementSpeed(movementStatusSpeed, m_runningSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToStartDash function <color=green>to add.</color>");

            characterKeyInputStateManagement.InputToStopDash += (characterStatusManagement) => { SetMovementSpeed( movementStatusSpeed, m_walkingSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToStopDash function <color=green>to add.</color>");

            characterKeyInputStateManagement.InputToMovement += (characterStatusManagement, h, v) => { MovementControll(h, v); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToMovement function <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            characterKeyInputStateManagement.InputToStartDash -= (characterStatusManagement) => { SetMovementSpeed( movementStatusSpeed, m_runningSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");

            characterKeyInputStateManagement.InputToStopDash -= (characterStatusManagement) => { SetMovementSpeed( movementStatusSpeed, m_walkingSpeed); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");

            characterKeyInputStateManagement.InputToMovement -= (characterStatusManagement, h, v) => { MovementControll(h, v); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");
        }

        void FixedUpdate()
        {
            Vector3 velocityValue =
                new Vector3( m_movementVelocity.x * m_movementSpeed * lateralMovementRate,
                             m_characterStatusManagement.GetMyRigidbody().velocity.y,
                             m_movementVelocity.z * m_movementSpeed);

            m_movementVelocity = Vector3.zero;

            m_characterStatusManagement.SetVelocity(velocityValue);

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
                  m_characterStatusManagement.GetMyRigidbody().gameObject.transform.forward * m_movementVector.z +
                  m_characterStatusManagement.GetMyRigidbody().gameObject.transform.right * m_movementVector.x;

            m_movementVelocity.Normalize();
        }

        #endregion
    }
}