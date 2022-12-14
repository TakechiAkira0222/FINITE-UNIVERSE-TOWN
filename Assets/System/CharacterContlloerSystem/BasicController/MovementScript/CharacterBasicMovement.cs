using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
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

        [Header("=== ScriptSetting ===")]
        [SerializeField] private Rigidbody  m_rb;
        [SerializeField] private Vector3    m_movementVector;
        [SerializeField] private Vector3    m_movementVelocity;
        
        #endregion

        #region private
        private float       m_movementCleanSpeed => m_characterStatusManagement.MovingSpeed;
        private float       m_lateralMovementRate => m_characterStatusManagement.LateralMovementRatio;

        private float       m_movementSpeed = 5;

        #endregion

        #region private const 

        private const float m_walkingSpeed = 1;
        private const float m_runningSpeed = 3.5f;

        #endregion

        #region GetProperty
        public Vector3 MovementVector => m_movementVector;
        public Vector3 MovementVelocity => m_movementVelocity;

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

        void Start()
        {
            if( m_characterStatusManagement == null)
            {
                m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
                Debug.LogWarning(" m_characterStatusManagement It wasn't set, so I set it.");
            }

            SetMovementSpeed(ref m_movementSpeed, m_movementCleanSpeed, m_walkingSpeed);
        }

        void Update()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            MovementControll();
            MovementSpeedChange();
        }

        void FixedUpdate()
        {
            m_rb.velocity = 
                new Vector3( m_movementVelocity.x * m_movementSpeed * m_lateralMovementRate,
                             m_rb.velocity.y,
                             m_movementVelocity.z * m_movementSpeed);
        }

        #endregion

        #region SetProperty

        void SetMovementSpeed(ref float movementSpeed, float cleanSpeed, float force)
        {
            movementSpeed = cleanSpeed + force;
        }

        #endregion

        #region ControllFinctoin
        void MovementControll()
        {
            m_movementVector = 
                new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            m_movementVelocity =
                 m_rb.gameObject.transform.forward * m_movementVector.z +
                 m_rb.gameObject.transform.right * m_movementVector.x;

            m_movementVelocity.Normalize();
        }

        void MovementSpeedChange()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                SetMovementSpeed(ref m_movementSpeed, m_movementCleanSpeed, m_runningSpeed);

            if (Input.GetKeyUp(KeyCode.LeftShift))
                SetMovementSpeed(ref m_movementSpeed, m_movementCleanSpeed, m_walkingSpeed);
        }

      
        #endregion
    }
}