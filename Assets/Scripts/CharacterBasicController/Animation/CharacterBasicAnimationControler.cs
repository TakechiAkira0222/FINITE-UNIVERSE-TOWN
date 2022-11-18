using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Takechi.CharacterController.Movement;

namespace Takechi.CharacterController.Animation
{
    public class CharacterBasicAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private Animator m_animator;
        [SerializeField] private CharacterBasicMovement m_characterBasicMovement;
        #endregion

        void Reset()
        {
            m_animator = this.transform.GetComponent<Animator>();
        }

        void Start()
        {

        }

        void Update()
        {
            m_animator.SetFloat("movementVectorX", m_characterBasicMovement.MovementVector.x);
            m_animator.SetFloat("movementVectorZ", m_characterBasicMovement.MovementVector.z);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_animator.SetTrigger("Jumping");
            }
        }
    }
}
