using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerNetworkRendererDeathblowAnimationEventManager : MonoBehaviour
    {
        #region UnityAnimatorEvent

        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private GameObject m_model;

        private void Awake()
        {

        }

        /// <summary>
        /// �K�E�Zanimation�̊J�n
        /// </summary>
        void OfficeWorkerDeathblowStart()
        {
            if (m_characterStatusManagement.photonView.IsMine)
            {
                m_model.SetActive(true);
                
            }
            else
            {

            }
        }

        /// <summary>
        /// �K�E�Zanimation�̏I��
        /// </summary>
        void OfficeWorkerDeathblowEnd()
        {
            if (m_characterStatusManagement.photonView.IsMine)
            {
                m_model.SetActive(false);
            }
            else
            {

            }
        }

        #endregion
    }
}