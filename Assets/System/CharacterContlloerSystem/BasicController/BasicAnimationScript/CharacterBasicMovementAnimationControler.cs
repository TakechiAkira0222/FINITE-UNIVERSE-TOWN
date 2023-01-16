using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.ScriptReference.DamagesThePlayerObject;

using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.KeyInputStete;

using System;

using Photon.Pun;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.DamagesThePlayerObject.ObjectReferenceThatDamagesThePlayer;


namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotnView;
        [SerializeField] private bool       m_notAttackAnimation = false;
        [SerializeField] private bool       m_notDeathblowAnimation = false;
        #endregion

        #region protected
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement KeyInputStateManagement => m_characterKeyInputStateManagement;
        protected PhotonView thisPhotnView => m_thisPhotnView;
        protected bool notAttackAnimation => m_notAttackAnimation;
        protected bool notDeathblowAnimation => m_notDeathblowAnimation;
        protected Rigidbody  rb => m_characterStatusManagement.GetMyRigidbody();
        #endregion

        # region protected Event Action 
        protected event Action<Animator, float, float> m_movementAnimatoinAction = delegate { };
        protected event Action<Animator, float> m_dashAnimationAction = delegate { };
        protected event Action<Animator> m_jumpingAnimationAction = delegate { };
        protected event Action<Animator> m_attackAnimationAction = delegate { };
        protected event Action<Animator> m_deathblowAnimationAction = delegate { };
        protected event Action<Animator, float> m_damageAnimationAction = delegate { };

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_thisPhotnView = this.GetComponent<PhotonView>();
        }

        protected virtual void Awake()
        {
            m_movementAnimatoinAction = (animator, horivec, vertVec) =>
            {
                animator.SetFloat(AnimatorParameter.movementVectorXParameterName, horivec);
                animator.SetFloat(AnimatorParameter.movementVectorZParameterName, vertVec);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to set.</color>");

            m_dashAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(AnimatorParameter.dashParameterName, parameter);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_dashAnimationAction function <color=green>to set.</color>");

            m_attackAnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.attackParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_attackAnimationAction function <color=green>to set.</color>");

            m_jumpingAnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.jumpingParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_jumpingAnimationAction function <color=green>to set.</color>");

            m_deathblowAnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.deathblowParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_deathblowAnimationAction function <color=green>to set.</color>");

            m_damageAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(AnimatorParameter.damageforceParameterName, parameter);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_damageAnimationAction function <color=green> to set.</color>");
        }

        private void OnEnable()
        {
            if (!PhotonNetwork.InRoom) return;

            m_characterKeyInputStateManagement.InputToMovement += (characterStatusManagement, horiVec, vertVec) =>
            {
                m_movementAnimatoinAction(characterStatusManagement.GetHandOnlyModelAnimator(), horiVec, vertVec);
                m_movementAnimatoinAction(characterStatusManagement.GetNetworkModelAnimator(), horiVec, vertVec);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToMovement function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToStartDash += (characterStatusManagement) =>
            {
                m_dashAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator(), 1);
                m_dashAnimationAction(characterStatusManagement.GetNetworkModelAnimator(), 1);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStartDash function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToStopDash += (characterStatusManagement) =>
            {
                m_dashAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator(), 0);
                m_dashAnimationAction(characterStatusManagement.GetNetworkModelAnimator(), 0);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStopDash function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToJump += (characterStatusManagement) =>
            {
                if (!characterStatusManagement.GetIsGrounded()) return;
                m_thisPhotnView.RPC(nameof(RPC_JumpingAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToNormalAttack += (characterStatusManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_AttackAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToNormalAttack function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToDeathblow += (characterStatusManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_DeathblowAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToDeathblow function <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            if (!PhotonNetwork.InRoom) return;

            m_characterKeyInputStateManagement.InputToMovement -= (characterStatusManagement, horiVec, vertVec) =>
            {
                m_movementAnimatoinAction(characterStatusManagement.GetHandOnlyModelAnimator(), horiVec, vertVec);
                m_movementAnimatoinAction(characterStatusManagement.GetNetworkModelAnimator(), horiVec, vertVec);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToMovement function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToStartDash -= (characterStatusManagement) =>
            {
                m_dashAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator(), 1);
                m_dashAnimationAction(characterStatusManagement.GetNetworkModelAnimator(), 1);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStartDash function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToStopDash -= (characterStatusManagement) =>
            {
                m_dashAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator(), 0);
                m_dashAnimationAction(characterStatusManagement.GetNetworkModelAnimator(), 0);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStopDash function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToJump -= (characterStatusManagement) =>
            {
                if (!characterStatusManagement.GetIsGrounded()) return;
                m_thisPhotnView.RPC(nameof(RPC_JumpingAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToNormalAttack -= (characterStatusManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_AttackAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToNormalAttack function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToDeathblow -= (characterStatusManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_DeathblowAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToDeathblow function <color=green>to remove.</color>");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            if (collision.gameObject.tag == DamageFromPlayerToPlayer.weaponTagName)
            {
                int number =
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
            }
            else if (collision.gameObject.tag == DamageFromPlayerToPlayer.bulletsTagName)
            {
                int number =
                    collision.transform.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
            }

            foreach (string s in DamageFromObjectToPlayer.objectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    float power = DamageFromObjectToPlayer.objectDamagesDictionary[s];
                    thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!PhotonNetwork.InRoom) return;

            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, 0f);
        }

        #endregion

        #region PunRPCFanction

        [PunRPC]
        protected void RPC_AttackAnimationTrigger()
        {
            m_attackAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator());
            m_attackAnimationAction(characterStatusManagement.GetNetworkModelAnimator());
        }

        [PunRPC]
        protected void RPC_JumpingAnimationTrigger()
        {
            m_jumpingAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator());
            m_jumpingAnimationAction(characterStatusManagement.GetNetworkModelAnimator());
        }

        [PunRPC]
        protected void RPC_DeathblowAnimationTrigger()
        {
            m_deathblowAnimationAction(characterStatusManagement.GetHandOnlyModelAnimator());
            m_deathblowAnimationAction(characterStatusManagement.GetNetworkModelAnimator());
        }

        [PunRPC]
        protected void RPC_DamageAnimationSetFloat(float power)
        {
            m_damageAnimationAction(characterStatusManagement.GetNetworkModelAnimator(), power);
        }

        #endregion

        /// <summary>
        /// check Teammember
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool checkTeammember(int number)
        {
            return
                PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.teamKey] ==
                PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey];
        }
    }
}
