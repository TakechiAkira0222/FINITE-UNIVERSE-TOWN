using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.CharacterController.ViewpointOperation
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicViewpointOperation : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private GameObject m_character;

        #endregion
        #region private

        private Quaternion m_cameraRot, m_characterRot;

        #endregion

        #region Struct

        private struct limitedToCamera
        {
            public const float minX = -90f;
            public const float maxX = 35f;
            public const float Xsensityvity = 1f;
            public const float Ysensityvity = 1f;
        }

        #endregion

        #region EventAction

        protected Action m_viewpointOperationControll = delegate { };

        #endregion

        #region UnityEvent
        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
        }

        void Awake()
        {
            m_viewpointOperationControll += CameraInput;
        }

        void Start()
        {
            m_cameraRot = m_mainCamera.transform.localRotation;
            m_characterRot = m_character.transform.localRotation;
        }

        protected  virtual void Update()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

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

        #endregion

        #region RecursiveFunction 
        /// <summary>
        /// ClampRotation
        /// </summary>
        /// <param name="q"></param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <returns>Å@êßå¿ÇÃÇ©Ç©Ç¡ÇΩÇSéüå≥êîÇÅAÇ©Ç¶ÇµÇ‹Ç∑ÅB</returns>
        private Quaternion ClampRotation( Quaternion q, float minX ,float maxX)
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
        #endregion
    }
}