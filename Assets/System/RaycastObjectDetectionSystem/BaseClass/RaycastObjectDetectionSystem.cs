using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;

namespace Takechi.RaycastObjectDetectionSystem
{

    public class RaycastObjectDetectionSystem : MonoBehaviour
    {
        #region PropertyClass
        public class RayProperty
        {
            public float m_distance;
            public Vector3 m_direction;
            public LayerMask m_layerMask;

            public RayProperty(float distance, Vector3 direction, LayerMask layerMask)
            {
                m_distance = distance;
                m_direction = direction;
                m_layerMask = layerMask;
            }
        }
        #endregion

        #region SsrializeFileld


        [SerializeField] private float m_distance = 1;

        [SerializeField] private LayerMask m_layerMask;

        [Header(" === BasicUiSoundEffects ===")]
        [SerializeField] BasicUiSoundEffects m_basicUiSound;
        [SerializeField] AudioSource m_audioSource;

        #endregion

        #region protected

        protected GameObject lookingObject => m_lookingObject;
        protected bool isLooking => m_isLooking;
        protected BasicUiSoundEffects basicUiSound => m_basicUiSound;
        protected AudioSource audioSource => m_audioSource;

        #endregion

        #region Event Action

        protected Action<GameObject> m_raycastHitAction = delegate { };
        protected Action<GameObject> m_raycastNotHitAction = delegate { };

        /// <summary>
        /// Hit���Ă���object���O��t���[���ƈ�������Ăяo����܂��B
        /// </summary>
        /// <remarks> ���������I�u�W�F�N�g�̑���O�ɌĂяo����A�O��t���[���܂œ������Ă��I�u�W�F�N�g�������Ɏ����܂��B </remarks>>
        protected Action<GameObject> m_raycastHitObjectChangeAction = delegate { };

        #endregion 

        #region private

        private GameObject m_lookingObject { get; set; }
        private bool m_isLooking
        {
            get
            {
                RayProperty rayProperty =
                     new RayProperty(m_distance, this.gameObject.transform.forward, m_layerMask);

                Ray ray =
                    new Ray(this.gameObject.transform.position,
                             rayProperty.m_direction * rayProperty.m_distance);

                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, rayProperty.m_distance, m_layerMask))
                {
                    Debug.DrawRay( ray.origin, ray.direction * rayProperty.m_distance, Color.green);

                    m_raycastHitAction( raycastHit.collider.gameObject);

                    if ( m_lookingObject != raycastHit.collider.gameObject && m_lookingObject!= null) 
                           m_raycastHitObjectChangeAction( m_lookingObject);

                    m_lookingObject = raycastHit.collider.gameObject;

                    return true;
                }
                else
                {
                    Debug.DrawRay( ray.origin, ray.direction * rayProperty.m_distance, Color.red);

                    if( m_lookingObject != null) m_raycastNotHitAction( m_lookingObject);

                    m_lookingObject = null;

                    return false;
                }
            }
        }

        #endregion
    }
}

