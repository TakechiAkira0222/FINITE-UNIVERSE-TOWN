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
        [SerializeField, Range( 0, 1)] private float m_lookingIkWeight = 0.5f;
        [SerializeField, Range( 0, 1)] private float m_rightHandIkWeight = 0.5f;
        [SerializeField, Range( 0, 1)] private float m_leftHandIkWeight = 0.5f;

        private float cleanLookingIkWeight;
        private float cleanRightHandIkWeight;
        private float cleanLeftHandIkWeight;

        #endregion

        #region Get 
        public float GetLookingIkWeight()   { return m_lookingIkWeight; }
        public float GetRightHandIkWeight() { return m_rightHandIkWeight; }
        public float GetLeftHandIkWeight()  { return m_leftHandIkWeight; }

        #endregion

        #region Set

        public void SetLookingIkWeight(float value)   
        {
            m_lookingIkWeight = value;
            Debug.Log($"{m_lookingIkWeight} = {value}");
        }
        public void SetRightHandIkWeight(float value) 
        {
            m_rightHandIkWeight = value;
            Debug.Log($"{m_rightHandIkWeight} = {value}");
        }
        public void SetLeftHandIkWeight(float value)  
        { 
            m_leftHandIkWeight = value;
            Debug.Log($"{m_leftHandIkWeight} = {value}");
        }
        public void SetAllIkWeight(float value)
        {
            SetLookingIkWeight(value);
            SetRightHandIkWeight(value);
            SetLeftHandIkWeight(value);
        }
        #endregion

        public void ResetAllIkWeight()
        {
            SetLookingIkWeight(  cleanLookingIkWeight);
            SetRightHandIkWeight(cleanRightHandIkWeight);
            SetLeftHandIkWeight( cleanLeftHandIkWeight);
        }

        private void Reset()
        {
            m_animator = this.GetComponent<Animator>();
        }

        private void Start()
        {
            cleanLookingIkWeight   = GetLookingIkWeight();
            cleanRightHandIkWeight = GetRightHandIkWeight();
            cleanLeftHandIkWeight  = GetLeftHandIkWeight();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LookAtWithAnimation( m_animator, m_lookingIkWeight, m_lookingIkTarget);

            HandIKAnimationSettings( m_animator, AvatarIKGoal.RightHand, m_rightHandIkWeight, m_rightHandIkTarget);
            HandIKAnimationSettings( m_animator, AvatarIKGoal.LeftHand, m_leftHandIkWeight, m_leftHandIkTarget);
        }

        #region recursive function

        /// <summary>
        /// アニメーションによって任意の方向を向く。
        /// </summary>
        /// <param name="animator"> アニメーター　</param>
        /// <param name="weight"> 重さ( オーバーライドの強さ ) </param>
        /// <param name="target"> ターゲット座標 </param>
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
        /// 手のIK アニメーションの設定
        /// </summary>
        /// <param name="animator">  アニメーター　</param>
        /// <param name="avatarIK">　IKによって変更したいRig </param>
        /// <param name="weight">    重さ </param>
        /// <param name="target">    ターゲット座標 </param>
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
