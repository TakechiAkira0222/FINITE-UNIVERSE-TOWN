using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.CharacterController.ViewpointOperation
{
    public class CharacterBasicViewpointOperation : MonoBehaviour
    {
        #region SerializeField

        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private GameObject m_character;

        #endregion

        #region private

        private Quaternion m_cameraRot, m_characterRot;
        private bool cursorLock = true;

        #endregion

        #region Struct

        private struct limitedToCamera
        {
            public const float minX = -90f;
            public const float maxX = 35f;
            public const float Xsensityvity = 3f;
            public const float Ysensityvity = 3f;
        }

        #endregion

        #region EventAction

        protected Action m_viewpointOperationControll = delegate { };

        #endregion

        #region UnityEvent
        void Reset()
        {
            
        }

        void Awake()
        {
            m_viewpointOperationControll += CameraInput;
            m_viewpointOperationControll += UpdateCursorLock;
        }

        void Start()
        {
            m_cameraRot = m_mainCamera.transform.localRotation;
            m_characterRot = m_character.transform.localRotation;
        }

        protected  virtual void Update()
        {
            m_viewpointOperationControll();
        }
        #endregion

        #region ViewpointOperationControll
        private void CameraInput()
        {
            float xRot = Input.GetAxis("Mouse X") * limitedToCamera.Ysensityvity;
            float yRot = Input.GetAxis("Mouse Y") * limitedToCamera.Xsensityvity;

            m_cameraRot *= Quaternion.Euler(-yRot, 0, 0);
            m_characterRot *= Quaternion.Euler(0, xRot, 0);

            m_cameraRot =
                ClampRotation( m_cameraRot, limitedToCamera.minX, limitedToCamera.maxX);

            m_mainCamera.transform.localRotation = m_cameraRot;
            m_character.transform.localRotation = m_characterRot;
        }

        private void UpdateCursorLock()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cursorLock = false;
            }
            else if (Input.GetMouseButton(0))
            {
                cursorLock = true;
            }


            if ( cursorLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (!cursorLock)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        #endregion

        /// <summary>
        /// ClampRotation
        /// </summary>
        /// <param name="q"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <returns>Å@êßå¿ÇÃÇ©Ç©Ç¡ÇΩÇSéüå≥êîÇÅAÇ©Ç¶ÇµÇ‹Ç∑ÅB</returns>
        public Quaternion ClampRotation( Quaternion q, float minX ,float maxX)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1f;

            float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

            angleX = Mathf.Clamp(angleX, minX, maxX);

            q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

            return q;
        }
    }
}