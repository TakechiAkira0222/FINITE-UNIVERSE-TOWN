using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.ScriptReference.DamagesThePlayerObject;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.KeyInputStete;

using System;

using Photon.Pun;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;


namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
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
        protected CharacterAddressManagement addressManagement => m_characterAddressManagement;
        protected CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        protected PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        protected Rigidbody  rb => addressManagement.GetMyRigidbody();
        protected Animator   handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        protected Animator   networkModelAnimator =>  addressManagement.GetNetworkModelAnimator();
        protected PhotonView thisPhotnView   => m_thisPhotnView;
        protected bool notAttackAnimation    => m_notAttackAnimation;
        protected bool notDeathblowAnimation => m_notDeathblowAnimation;

        #endregion

        #region private variable
        private bool m_interfere = false;

        #endregion


        #region protected Event Action 
        protected event Action<Animator> m_jumpingAnimationAction = delegate { };
        protected event Action<Animator> m_attackAnimationAction = delegate { };
        protected event Action<Animator> m_deathblowAnimationAction = delegate { };
        protected event Action<Animator> m_ablity1AnimationAction = delegate { };
        protected event Action<Animator> m_ablity2AnimationAction = delegate { };
        protected event Action<Animator> m_ablity3AnimationAction = delegate { };
        protected event Action<Animator, float> m_dashAnimationAction = delegate { };
        protected event Action<Animator, float> m_damageAnimationAction = delegate { };
        protected event Action<Animator, float, float> m_movementAnimatoinAction = delegate { };

        #endregion

        public bool GetInterfere() { return m_interfere; }
        public void SetInterfere(bool value) { m_interfere = value; }

        #region UnityEvent
        private void Reset() { m_thisPhotnView = this.GetComponent<PhotonView>(); }
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

            m_ablity1AnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.Ablity1ParameterName);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_ablity1AnimationAction function <color=green>to set.</color>");

            m_ablity2AnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.Ablity2ParameterName);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_ablity2AnimationAction function <color=green>to set.</color>");

            m_ablity3AnimationAction = (animator) =>
            {
                animator.SetTrigger(AnimatorParameter.Ablity3ParameterName);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_ablity3AnimationAction function <color=green>to set.</color>");

            m_damageAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(AnimatorParameter.damageforceParameterName, parameter);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_damageAnimationAction function <color=green> to set.</color>");
        }
        private void OnEnable()
        {
            if (!PhotonNetwork.InRoom) return;
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            m_characterKeyInputStateManagement.InputToMovement += (statusManagement, addressManagement, horiVec, vertVec) =>
            {
                m_movementAnimatoinAction(handOnlyModelAnimator, horiVec, vertVec);
                m_movementAnimatoinAction(networkModelAnimator, horiVec, vertVec);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToMovement function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToStartDash += (statusManagement, addressManagement) =>
            {
                m_dashAnimationAction(handOnlyModelAnimator, 1);
                m_dashAnimationAction(networkModelAnimator, 1);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStartDash function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToStopDash += (statusManagement, addressManagement) =>
            {
                m_dashAnimationAction(handOnlyModelAnimator, 0);
                m_dashAnimationAction(networkModelAnimator, 0);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStopDash function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToJump += (statusManagement, addressManagement) =>
            {
                if (!statusManagement.GetIsGrounded()) return;
                m_thisPhotnView.RPC(nameof(RPC_JumpingAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToNormalAttack += (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_AttackAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToNormalAttack function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToDeathblow += (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_DeathblowAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToDeathblow function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToAblity1 += (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity1AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity1 function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToAblity2 += (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity2AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity2 function <color=green>to add.</color>");

            m_characterKeyInputStateManagement.InputToAblity3 += (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity3AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity3 function <color=green>to add.</color>");
        }
        private void OnDisable()
        {
            if (!PhotonNetwork.InRoom) return;
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            m_characterKeyInputStateManagement.InputToMovement -= (statusManagement, addressManagement, horiVec, vertVec) =>
            {
                m_movementAnimatoinAction(handOnlyModelAnimator, horiVec, vertVec);
                m_movementAnimatoinAction(networkModelAnimator, horiVec, vertVec);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToMovement function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToStartDash -= (statusManagement, addressManagement) =>
            {
                m_dashAnimationAction(handOnlyModelAnimator, 1);
                m_dashAnimationAction(networkModelAnimator, 1);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStartDash function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToStopDash -= (statusManagement, addressManagement) =>
            {
                m_dashAnimationAction(handOnlyModelAnimator, 0);
                m_dashAnimationAction(networkModelAnimator, 0);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToStopDash function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToJump -= (statusManagement, addressManagement) =>
            {
                if (!statusManagement.GetIsGrounded()) return;
                m_thisPhotnView.RPC(nameof(RPC_JumpingAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToNormalAttack -= (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_AttackAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToNormalAttack function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToDeathblow -= (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_DeathblowAnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToDeathblow function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToAblity1 -= (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity1AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity1 function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToAblity2 -= (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity2AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity2 function <color=green>to remove.</color>");

            m_characterKeyInputStateManagement.InputToAblity3 -= (statusManagement, addressManagement) =>
            {
                if (notAttackAnimation) return;
                m_thisPhotnView.RPC(nameof(RPC_Ablity3AnimationTrigger), RpcTarget.AllBufferedViaServer);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToAblity3 function <color=green>to remove.</color>");
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!m_interfere) return;
            if (!myPhotonView.IsMine) return;

            if (collision.gameObject.tag == DamageFromPlayerToPlayer.weaponTagName)
            {
                int number =
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, power);
            }
            else if (collision.gameObject.tag == DamageFromPlayerToPlayer.bulletsTagName)
            {
                int number =
                    collision.transform.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

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
            if (!m_interfere) return;
            if (!myPhotonView.IsMine) return;
            if (!PhotonNetwork.InRoom) return;

            thisPhotnView.RPC(nameof(RPC_DamageAnimationSetFloat), RpcTarget.AllBufferedViaServer, 0f);
        }

        #endregion

        #region PunRPCFanction
        [PunRPC]
        protected void RPC_AttackAnimationTrigger()
        {
            m_attackAnimationAction(handOnlyModelAnimator);
            m_attackAnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_JumpingAnimationTrigger()
        {
            m_jumpingAnimationAction(handOnlyModelAnimator);
            m_jumpingAnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_DeathblowAnimationTrigger()
        {
            m_deathblowAnimationAction(handOnlyModelAnimator);
            m_deathblowAnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_Ablity1AnimationTrigger()
        {
            m_ablity1AnimationAction(handOnlyModelAnimator);
            m_ablity1AnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_Ablity2AnimationTrigger()
        {
            m_ablity2AnimationAction(handOnlyModelAnimator);
            m_ablity2AnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_Ablity3AnimationTrigger()
        {
            m_ablity3AnimationAction(handOnlyModelAnimator);
            m_ablity3AnimationAction(networkModelAnimator);
        }
        [PunRPC]
        protected void RPC_DamageAnimationSetFloat(float power)
        {
            m_damageAnimationAction(networkModelAnimator, power);
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
                statusManagement.GetCustomPropertiesTeamName(number) == statusManagement.GetCustomPropertiesTeamName();
        }
    }
}
