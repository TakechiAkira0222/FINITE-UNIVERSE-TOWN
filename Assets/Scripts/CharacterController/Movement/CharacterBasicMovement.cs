using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace takechi.CharacterController.Movement
{
    public class CharacterBasicMovement : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private Rigidbody  m_rb;
        [SerializeField] private Vector3    m_movementVector;
        [SerializeField] private float      m_movementSpeed = 5;
        #endregion

        #region GetProperty
        public Vector3 MovementVector => m_movementVector;

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

        void Start()
        {

        }

        void Update()
        {
            MovementControll();
        }

        void FixedUpdate()
        {
            m_rb.velocity = 
                new Vector3( m_movementVector.x, m_rb.velocity.y, m_movementVector.z) * m_movementSpeed;
        }

        #endregion

        #region ControllFinctoin
        void MovementControll()
        {
            m_movementVector = 
                new Vector3( Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            m_movementVector = 
                 this.transform.forward * m_movementVector.z + 
                 this.transform.right * m_movementVector.x;

            m_movementVector.Normalize();
        }

        #endregion
    }
}