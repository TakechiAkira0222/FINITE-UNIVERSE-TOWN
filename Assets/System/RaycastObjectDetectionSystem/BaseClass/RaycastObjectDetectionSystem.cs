using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;

namespace Takechi.RaycastObjectDetectionSystem
{
    public class RaycastObjectDetectionSystem : MonoBehaviour
    {
        #region PropertyClass
        public class RayProperty
        {
            public float     distance;
            public Vector3   direction;
            public LayerMask layerMask;

            public RayProperty(float _distance, Vector3 _direction, LayerMask _layerMask)
            {
                distance  = _distance;
                direction = _direction;
                layerMask = _layerMask;
            }
        }
        #endregion

        #region srialize fileld
        [Header(" === Script Setting ===")]
        [SerializeField] private BasicUiSoundEffects m_basicUiSound;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private float m_distance = 1;
        [SerializeField] private LayerMask m_layerMask;

        #endregion

        #region get variable
        public bool GetIsOperation() { return isOperation; }

        #endregion

        #region set variable
        public void SetIsOperation(bool state)
        {
            isOperation = state;
            Debug.Log($" m_isOperation {isOperation} to set. ");
        }

        #endregion

        #region protected variable
        protected BasicUiSoundEffects basicUiSound => m_basicUiSound;
        protected GameObject  lookingObject => m_lookingObject;
        protected AudioSource audioSource => m_audioSource;
        protected LayerMask   layerMask => m_layerMask;
        protected float distance => m_distance;
        protected bool  isLooking => m_isLooking;
        protected bool  isOperation = true;

        #endregion

        #region Event Action

        protected Action<GameObject> m_raycastHitAction = delegate { };
        protected Action<GameObject> m_raycastNotHitAction = delegate { };
        /// <summary>
        /// Hitしているobjectが前回フレームと違った時呼び出されます。
        /// </summary>
        /// <remarks> 当たったオブジェクトの代入前に呼び出され、前回フレームまで当たってたオブジェクトを引数に持ちます。 </remarks>>
        protected Action<GameObject> m_raycastHitObjectChangeAction = delegate { };

        #endregion 

        #region private variable
        private GameObject m_lookingObject { get; set; }
        private bool m_isLooking
        {
            get
            {
                RayProperty rayProperty =
                     new RayProperty( distance, this.gameObject.transform.forward, layerMask);

                Ray ray =
                    new Ray(this.gameObject.transform.position,
                             rayProperty.direction * rayProperty.distance);

                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, rayProperty.distance, m_layerMask))
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.distance, Color.green);

                    m_raycastHitAction(raycastHit.collider.gameObject);

                    if (m_lookingObject != raycastHit.collider.gameObject && m_lookingObject != null)
                        m_raycastHitObjectChangeAction(m_lookingObject);

                    m_lookingObject = raycastHit.collider.gameObject;

                    return true;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.distance, Color.red);

                    if (m_lookingObject != null) m_raycastNotHitAction(m_lookingObject);

                    m_lookingObject = null;

                    return false;
                }
            }
        }

        #endregion
    }
}

