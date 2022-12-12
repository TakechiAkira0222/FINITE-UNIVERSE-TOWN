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
        private float       m_movementSpeed = 5;
        private float       m_pushPower = 2.0f;
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
                new Vector3( m_movementVelocity.x * m_movementSpeed,
                             m_rb.velocity.y,
                             m_movementVelocity.z * m_movementSpeed);
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
            {
                return;
            }

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3)
            {
                return;
            }

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * m_pushPower;
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