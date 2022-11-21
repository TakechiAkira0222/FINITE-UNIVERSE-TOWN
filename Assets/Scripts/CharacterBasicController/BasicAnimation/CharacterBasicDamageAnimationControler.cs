using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Takechi.CharacterController.BasicAnimation.Movement;

namespace Takechi.CharacterController.BasicAnimation.Damage
{
    public class CharacterBasicDamageAnimationControler : CharacterBasicMovementAnimationControler
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
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
    }
}
