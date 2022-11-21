using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Movement
{
    public class CharacterBasicMovement : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private Rigidbody  m_rb;
        [SerializeField] private Vector3    m_movementVector;
        [SerializeField] private Vector3    m_movementVelocity;
        [SerializeField] private float      m_movementSpeed = 5;
        #endregion

        #region private
        private const float m_walkingSpeed = 1;
        private const float m_runningSpeed = 2.3f;
        #endregion

        #region GetProperty
        public Vector3 MovementVector => m_movementVector;
        public Vector3 MovementVelocity => m_movementVelocity;

        #endregion

        #region UnityEvent
        void Reset()
        {
           // m_rb = this.transform.GetComponent<Rigidbody>();
        }

        void Start()
        {

        }

        void Update()
        {
            MovementControll();

            MovementSpeedChange();
        }

        void FixedUpdate()
        {
            m_rb.velocity = 
                new Vector3( m_movementVelocity.x * m_movementSpeed,
                             m_rb.velocity.y,
                             m_movementVelocity.z * m_movementSpeed);
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
            {
                m_movementSpeed = m_runningSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_movementSpeed = m_walkingSpeed;
            }
        }

        #endregion
    }
}