using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Takechi.CharacterController.BasicAnimation.Movement;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.DamagesThePlayerObject;
using Takechi.CharacterController.Parameters;
using System;

namespace Takechi.CharacterController.BasicAnimation.Damage
{
    public class CharacterBasicDamageAnimationControler : CharacterBasicMovementAnimationControler
    {
        #region protected Event Action 
        protected event Action<Animator, float> m_damageAnimationAction = delegate { };

        #endregion

        #region UnityEvent

        protected override void Awake()
        {
            base.Awake();

            m_damageAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_DamageforceParameterName, parameter);
            };

            Debug.Log(" m_damageAnimationAction function to set.");
        }

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
                    m_damageAnimationAction(m_networkRendererAnimator, 20f);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!characterStatusManagement.PhotonView.IsMine) return;

            m_damageAnimationAction(m_networkRendererAnimator, 0f);
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
