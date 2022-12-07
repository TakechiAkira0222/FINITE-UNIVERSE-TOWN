using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.AnimationParameter;

namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region PropertyClass
        public class RayProperty
        {
            public float m_distance;
            public Vector3 m_direction;

            public RayProperty(float distance, Vector3 direction)
            {
                m_distance = distance;
                m_direction = direction;
            }
        }
        #endregion

        #region SerializeField
        [SerializeField] protected Animator m_animator;
        [SerializeField] protected Rigidbody m_rb;
        [SerializeField] protected bool     m_NotAttackAnimation = false;
        [SerializeField] protected bool     m_NotDeathblowAnimation = false;

        //[SerializeField] protected CharacterBasicMovement m_characterBasicMovement;
        #endregion

        #region private
        private RayProperty rayProperty =
            new RayProperty(0.1f, Vector3.down);

        /// <summary>
        /// ’…’n”»’è
        /// </summary>
        private bool m_isGrounded
        {
            get
            {
                Ray ray =
                    new Ray(m_rb.gameObject.transform.position + new Vector3(0, 0.1f),
                             rayProperty.m_direction * rayProperty.m_distance);

                RaycastHit raycastHit;


                if (Physics.Raycast(ray, out raycastHit, rayProperty.m_distance))
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.green);
                    return true;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.red);
                    return false;
                }
            }
        }

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_animator = this.transform.GetComponent<Animator>();
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            //m_animator.SetFloat("movementVectorX", m_characterBasicMovement.MovementVector.x);
            //m_animator.SetFloat("movementVectorZ", m_characterBasicMovement.MovementVector.z);

            m_animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorXParameterName, Input.GetAxisRaw("Horizontal"));
            m_animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorZParameterName, Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_animator.SetFloat(ReferencingTheAnimationParameterName.s_DashParameterName, 1f);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_animator.SetFloat(ReferencingTheAnimationParameterName.s_DashParameterName, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_JumpingParameterName);
            }

            if (Input.GetMouseButtonDown(0) && !m_NotAttackAnimation)
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_AttackParameterName);
            }

            if (Input.GetKeyDown(KeyCode.Q) && !m_NotDeathblowAnimation)
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_DeathblowParameterName);
            }
        }
        #endregion
    }
}
