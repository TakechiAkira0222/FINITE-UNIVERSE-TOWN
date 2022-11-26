using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.AnimationParameter;

namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] protected Animator m_animator;
        //[SerializeField] protected CharacterBasicMovement m_characterBasicMovement;
        #endregion

        #region UnityEvent
        void Reset()
        {
            m_animator = this.transform.GetComponent<Animator>();
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_JumpingParameterName);
            }

            if (Input.GetMouseButtonDown(0))
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_AttackParameterName);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_animator.SetTrigger( ReferencingTheAnimationParameterName.s_DeathblowParameterName);
            }
        }
        #endregion
    }
}
