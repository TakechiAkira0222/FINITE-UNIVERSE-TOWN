using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Takechi.CharacterController.BasicAnimation.Movement;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.DamagesThePlayerObject;

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
        }

        private void OnCollisionEnter(Collision collision)
        {
            foreach (string s in ObjectReferenceThatDamagesThePlayer.s_DamagesThePlayerObjectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    m_animator.SetFloat(ReferencingTheAnimationParameterName.s_DamageforceParameterName, 20f);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            m_animator.SetFloat( ReferencingTheAnimationParameterName.s_DamageforceParameterName, 0f);
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
