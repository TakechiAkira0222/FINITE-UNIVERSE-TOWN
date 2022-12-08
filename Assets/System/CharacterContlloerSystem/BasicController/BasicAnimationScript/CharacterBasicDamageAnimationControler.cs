using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Takechi.CharacterController.BasicAnimation.Movement;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.DamagesThePlayerObject;
using Takechi.CharacterController.Parameters;

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
            if (!characterStatusManagement.PhotonView.IsMine) return;

            base.Update();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!characterStatusManagement.PhotonView.IsMine) return;

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
            if (!characterStatusManagement.PhotonView.IsMine) return;

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
