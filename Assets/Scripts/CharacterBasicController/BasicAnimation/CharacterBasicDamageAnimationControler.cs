using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Takechi.CharacterController.BasicAnimation.Movement;
using Photon.Pun;

namespace Takechi.CharacterController.BasicAnimation.Damage
{
    public class CharacterBasicDamageAnimationControler : CharacterBasicMovementAnimationControler
    {
        #region UnityEvent
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            if ( Input.GetMouseButtonDown(0))
            {
                m_animator.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_animator.SetTrigger("Deathblow");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Cube")
            {
                m_animator.SetFloat("Damageforce", 20f);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            m_animator.SetFloat("Damageforce", 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {

        }

        #endregion
    }
}
