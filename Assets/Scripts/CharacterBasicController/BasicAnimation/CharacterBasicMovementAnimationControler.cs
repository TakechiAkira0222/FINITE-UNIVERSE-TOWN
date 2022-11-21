using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Takechi.CharacterController.Movement;

namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] protected Animator m_animator;
        //[SerializeField] protected CharacterBasicMovement m_characterBasicMovement;
        #endregion

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

            m_animator.SetFloat("movementVectorX", Input.GetAxisRaw("Horizontal"));
            m_animator.SetFloat("movementVectorZ", Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_animator.SetFloat("Dash", 1f);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_animator.SetFloat("Dash", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_animator.SetTrigger("Jumping");
            }
        }
    }
}
