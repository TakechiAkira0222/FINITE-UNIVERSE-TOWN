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
        [SerializeField] private Transform m_lookingTarget;
        [SerializeField] private Transform m_leftHandIkTarget;
        // [SerializeField] private Transform m_leftFootIkTarget;
        [SerializeField] private Transform m_rightHandIkTarget;
        // [SerializeField] private Transform m_rightFootIkTarget;
        [SerializeField, Range(0, 1)] private float m_ikWeight = 0.5f;

        #endregion

        private void Reset()
        {
            m_animator = this.GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LookAtWithAnimation( m_animator, m_ikWeight, m_lookingTarget);

            HandIKAnimationSettings( m_animator, AvatarIKGoal.RightHand, m_ikWeight, m_rightHandIkTarget);
            HandIKAnimationSettings( m_animator, AvatarIKGoal.LeftHand,  m_ikWeight, m_leftHandIkTarget);

            //HandIKAnimationSettings( m_animator, AvatarIKGoal.RightFoot,  m_ikWeight, m_leftFootIkTarget);
            //HandIKAnimationSettings( m_animator, AvatarIKGoal.LeftFoot,  m_ikWeight, m_leftFootIkTarget);
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
                // animator.SetIKRotationWeight( avatarIK, weight);
                animator.SetIKPosition( avatarIK, target.position);
                // animator.SetIKRotation( avatarIK, target.rotation);
            }
            else
            {
                Debug.LogWarning($" IKtarget does not exist. TargetNaem : {target.name}");
            }
        }

        #endregion
    }
}
