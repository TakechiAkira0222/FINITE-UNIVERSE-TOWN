using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.AnimationParameter;
using Takechi.CharacterController.Parameters;
using System;

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
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] protected Animator m_networkRendererAnimator;
        [SerializeField] protected Animator m_handOnlyAnimator;

        [SerializeField] protected Rigidbody m_rb;
        [SerializeField] protected bool m_NotAttackAnimation = false;
        [SerializeField] protected bool m_NotDeathblowAnimation = false;

        #endregion

        #region protected
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;

        #endregion

        # region protected Event Action 
        protected event Action<Animator> m_movementAnimatoinAction = delegate { };
        protected event Action<Animator, float> m_dashAnimationAction = delegate { };
        protected event Action<Animator> m_jumpingAnimationAction = delegate { };
        protected event Action<Animator> m_attackAnimationAction = delegate { };
        protected event Action<Animator> m_deathblowAnimationAction = delegate { };


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
            m_networkRendererAnimator = this.transform.GetComponent<Animator>();
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

        protected virtual void Awake()
        {
            m_movementAnimatoinAction = (animator) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorXParameterName, Input.GetAxisRaw("Horizontal"));
                animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorZParameterName, Input.GetAxisRaw("Vertical"));
            };

            Debug.Log(" m_movementAnimatoinAction function to set.");

            m_dashAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_DashParameterName, parameter);
            };

            Debug.Log(" m_dashAnimationAction function to set.");

            m_attackAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_AttackParameterName);
            };

            Debug.Log(" m_attackAnimationAction function to set.");

            m_jumpingAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_JumpingParameterName);
            };

            Debug.Log(" m_jumpingAnimationAction function to set.");

            m_deathblowAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_DeathblowParameterName);
            };

            Debug.Log(" m_deathblowAnimationAction function to set.");
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_movementAnimatoinAction(m_handOnlyAnimator);
            m_movementAnimatoinAction(m_networkRendererAnimator);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_dashAnimationAction(m_networkRendererAnimator, 1);
                m_dashAnimationAction(m_handOnlyAnimator, 1);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_dashAnimationAction(m_networkRendererAnimator, 0);
                m_dashAnimationAction(m_handOnlyAnimator, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
            {
                m_jumpingAnimationAction(m_networkRendererAnimator);
                m_jumpingAnimationAction(m_handOnlyAnimator);
            }

            if (Input.GetMouseButtonDown(0) && !m_NotAttackAnimation)
            {
                m_attackAnimationAction(m_networkRendererAnimator);
                m_attackAnimationAction(m_handOnlyAnimator);
            }

            if (Input.GetKeyDown(KeyCode.Q) && !m_NotDeathblowAnimation)
            {
                m_deathblowAnimationAction(m_networkRendererAnimator);
                m_deathblowAnimationAction(m_handOnlyAnimator);
            }
        }
        #endregion
    }
}
