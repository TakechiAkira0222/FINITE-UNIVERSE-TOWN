using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;

namespace Takechi.RaycastObjectDetectionSystem
{

    public class RaycastObjectDetectionSystemInNetwork : MonoBehaviour
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
        [SerializeField] private Vector3 m_direction = Vector3.forward;
        [SerializeField] private LayerMask m_layerMask;
        [SerializeField] private PhotonView m_photonView;
        #endregion

        #region GetProperty
        public GameObject lookingObject => m_lookingObject;

        #endregion

        #region private

        private GameObject m_lookingObject;

        private bool m_isLooking
        {
            get
            {
                RayProperty rayProperty =
                     new RayProperty(m_distance, m_direction, m_layerMask);

                Ray ray =
                    new Ray(this.gameObject.transform.position,
                             rayProperty.m_direction * rayProperty.m_distance);

                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, rayProperty.m_distance))
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.green);
                    m_lookingObject = raycastHit.collider.gameObject;

                    return true;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.red);

                    m_lookingObject = null;
                    return false;
                }
            }
        }

        #endregion  

        protected virtual void Update()
        {
            if (!m_photonView.IsMine)
                return;

            if (!m_isLooking)
                return;
        }
    }
}
