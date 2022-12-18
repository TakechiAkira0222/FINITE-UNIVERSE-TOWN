using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
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
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region private
        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;
        private GameObject avater => characterStatusManagement.GetMyAvater();
        private Camera mainCamera => characterStatusManagement.GetMyMainCamera();

        private Quaternion cameraRot, characterRot;

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

        #region UnityEvent
        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
        }

        void Start()
        {
            cameraRot = mainCamera.transform.localRotation;
            characterRot = avater.transform.localRotation;
        }

        private void OnEnable()
        {
            characterKeyInputStateManagement.InputToViewpoint += (characterStatusManagement, mouseX, mouseY) => { CameraControll(mouseX, mouseY); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToViewpoint function <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            characterKeyInputStateManagement.InputToViewpoint -= (characterStatusManagement, mouseX, mouseY) => { CameraControll(mouseX, mouseY); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToViewpoint function <color=green>to remove.</color>");
        }

        #endregion

        #region ViewpointOperationControll
        private void CameraControll(float mouseX, float mouseY) 
        {
            float xRot = mouseX * limitedToCamera.Ysensityvity;
            float yRot = mouseY * limitedToCamera.Xsensityvity;

            cameraRot *= Quaternion.Euler(-yRot, 0, 0);
            characterRot *= Quaternion.Euler(0, xRot, 0);

            cameraRot =
                ClampRotation( cameraRot, limitedToCamera.minX, limitedToCamera.maxX);

            mainCamera.transform.localRotation = cameraRot;
            avater.transform.localRotation = characterRot;
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