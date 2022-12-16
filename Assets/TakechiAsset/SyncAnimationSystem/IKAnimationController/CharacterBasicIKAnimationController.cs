using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;

namespace Takechi.AnimationSystem.IK
{
    [RequireComponent(typeof(Animator))]
    public class CharacterBasicIKAnimationController : MonoBehaviour
    {
        #region SerializeField 

        [SerializeField] private Animator  m_animator;

        // Target
        [SerializeField] private Transform m_lookingIkTarget;
        [SerializeField] private Transform m_leftHandIkTarget;
        [SerializeField] private Transform m_rightHandIkTarget;

        // Weight
        [SerializeField, Range(0, 1)] private float m_lookingIkWeight = 0.5f;
        [SerializeField, Range(0, 1)] private float m_rightHandIkWeight = 0.5f;
        [SerializeField, Range(0, 1)] private float m_leftHandIkWeight = 0.5f;

        #endregion

        private void Reset()
        {
            m_animator = this.GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LookAtWithAnimation( m_animator, m_lookingIkWeight, m_lookingIkTarget);

            HandIKAnimationSettings( m_animator, AvatarIKGoal.RightHand, m_rightHandIkWeight, m_rightHandIkTarget);
            HandIKAnimationSettings( m_animator, AvatarIKGoal.LeftHand, m_leftHandIkWeight, m_leftHandIkTarget);
        }

        #region recursive function

        /// <summary>
        /// �A�j���[�V�����ɂ���ĔC�ӂ̕����������B
        /// </summary>
        /// <param name="animator"> �A�j���[�^�[�@</param>
        /// <param name="weight"> �d��( �I�[�o�[���C�h�̋��� ) </param>
        /// <param name="target"> �^�[�Q�b�g���W </param>
        private void LookAtWithAnimation( Animator animator, float weight, Transform target)
        {
            if ( target != null)
            {
                animator.SetLookAtWeight( weight);
                animator.SetLookAtPosition( target.position);
            }
            else
            {
                Debug.LogWarning($" ik target does not exist. TargetNaem : { target.name}");
            }
        }

        /// <summary>
        /// ���IK �A�j���[�V�����̐ݒ�
        /// </summary>
        /// <param name="animator">  �A�j���[�^�[�@</param>
        /// <param name="avatarIK">�@IK�ɂ���ĕύX������Rig </param>
        /// <param name="weight">    �d�� </param>
        /// <param name="target">    �^�[�Q�b�g���W </param>
        private void HandIKAnimationSettings( Animator animator, AvatarIKGoal avatarIK, float weight, Transform target)
        {
            if ( target != null)
            {
                animator.SetIKPositionWeight( avatarIK, weight);
                animator.SetIKPosition( avatarIK, target.position);
            }
            else
            {
                Debug.LogWarning($" IKtarget does not exist. TargetNaem : {target.name}");
            }
        }

        #endregion
    }
}
