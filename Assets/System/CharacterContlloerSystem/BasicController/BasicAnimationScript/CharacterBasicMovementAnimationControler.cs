using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.ScriptReference.AnimationParameter;
using Takechi.CharacterController.Parameters;
using System;
using Photon.Pun;
using System.Threading;

namespace Takechi.CharacterController.BasicAnimation.Movement
{
    public class CharacterBasicMovementAnimationControler : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotnView;
        [SerializeField] private Animator   m_networkRendererAnimator;
        [SerializeField] private Animator   m_handOnlyAnimator;
        [SerializeField] private bool       m_NotAttackAnimation = false;
        [SerializeField] private bool       m_NotDeathblowAnimation = false;
        #endregion

        #region protected
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        protected PhotonView thisPhotnView => m_thisPhotnView;
        protected Animator   networkRendererAnimator => m_networkRendererAnimator;
        protected Rigidbody  rb => m_characterStatusManagement.GetMyRigidbody();
        #endregion

        # region protected Event Action 
        protected event Action<Animator> m_movementAnimatoinAction = delegate { };
        protected event Action<Animator, float> m_dashAnimationAction = delegate { };
        protected event Action<Animator> m_jumpingAnimationAction = delegate { };
        protected event Action<Animator> m_attackAnimationAction = delegate { };
        protected event Action<Animator> m_deathblowAnimationAction = delegate { };

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_thisPhotnView           = this.GetComponent<PhotonView>();
            m_networkRendererAnimator = this.transform.GetComponent<Animator>();
        }

        protected virtual void Awake()
        {
            m_movementAnimatoinAction = (animator) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorXParameterName, Input.GetAxisRaw("Horizontal"));
                animator.SetFloat(ReferencingTheAnimationParameterName.s_MovementVectorZParameterName, Input.GetAxisRaw("Vertical"));
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to set.</color>");

            m_dashAnimationAction = (animator, parameter) =>
            {
                animator.SetFloat(ReferencingTheAnimationParameterName.s_DashParameterName, parameter);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_dashAnimationAction function <color=green>to set.</color>");

            m_attackAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_AttackParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_attackAnimationAction function <color=green>to set.</color>");

            m_jumpingAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_JumpingParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_jumpingAnimationAction function <color=green>to set.</color>");

            m_deathblowAnimationAction = (animator) =>
            {
                animator.SetTrigger(ReferencingTheAnimationParameterName.s_DeathblowParameterName);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_deathblowAnimationAction function <color=green>to set.</color>");
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (!m_characterStatusManagement.GetMyPhotonView().IsMine) return;

            m_movementAnimatoinAction(m_handOnlyAnimator);
            m_movementAnimatoinAction(m_networkRendererAnimator);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                m_dashAnimationAction(m_networkRendererAnimator, 1);
                m_dashAnimationAction(m_handOnlyAnimator, 1);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                m_dashAnimationAction(m_networkRendererAnimator, 0);
                m_dashAnimationAction(m_handOnlyAnimator, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && m_characterStatusManagement.GetIsGrounded())
            {
                m_thisPhotnView.RPC(nameof(RPC_JumpingAnimationTrigger), RpcTarget.AllBufferedViaServer);
            }

            if (Input.GetMouseButtonDown(0) && !m_NotAttackAnimation)
            {
                m_thisPhotnView.RPC(nameof(RPC_AttackAnimationTrigger), RpcTarget.AllBufferedViaServer);
            }

            if (Input.GetKeyDown(KeyCode.Q) && !m_NotDeathblowAnimation)
            {
                m_thisPhotnView.RPC(nameof(RPC_DeathblowAnimationTrigger), RpcTarget.AllBufferedViaServer);
            }
        }
        #endregion

        #region PunRPCFanction

        [PunRPC]
        protected void RPC_AttackAnimationTrigger()
        {
            m_attackAnimationAction(m_networkRendererAnimator);
            m_attackAnimationAction(m_handOnlyAnimator);
        }

        [PunRPC]
        protected void RPC_JumpingAnimationTrigger()
        {
            m_jumpingAnimationAction(m_networkRendererAnimator);
            m_jumpingAnimationAction(m_handOnlyAnimator);
        }

        [PunRPC]
        protected void RPC_DeathblowAnimationTrigger()
        {
            m_deathblowAnimationAction(m_networkRendererAnimator);
            m_deathblowAnimationAction(m_handOnlyAnimator);
        }

        #endregion
    }
}
