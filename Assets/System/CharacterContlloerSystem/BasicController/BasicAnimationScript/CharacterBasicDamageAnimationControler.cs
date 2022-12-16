using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Takechi.CharacterController.BasicAnimation.Movement;

using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.DamagesThePlayerObject;

using System;
using Photon.Pun;

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

            m_damageAnimationAction = ( animator, parameter) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_DamageforceParameterName, parameter);
            };

            Debug.Log(" m_damageAnimationAction function <color=green> to set.</color>");
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            base.Update();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            if (collision.gameObject.tag == ObjectReferenceThatDamagesThePlayer.s_PlayerCharacterWeaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                float power = 
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CustomPropertyKeyReference.s_CharacterStatusAttackPower];

                thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
            }

            foreach (string s in ObjectReferenceThatDamagesThePlayer.s_DamagesThePlayerObjectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    float power = ObjectReferenceThatDamagesThePlayer.s_DamageObjectPowerDictionary[s];

                    thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, 0f);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {

        }

        #endregion

        #region PunRPCFanction

        [PunRPC]
        protected void RPC_DamageAnimationSetFloat(float power)
        {
            m_damageAnimationAction(networkRendererAnimator, power);
        }

        #endregion

    }
}
