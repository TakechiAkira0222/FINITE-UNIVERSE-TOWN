using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerHandOnlyDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_officeWorkerStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;
        [SerializeField] private CharacterBasicMovement m_basicMovement;
        [SerializeField] private CharacterBasicJump m_basicJump;
        [SerializeField] private CharacterBasicViewpointOperation m_basicViewpoint;
        [SerializeField] private GameObject m_handOnlyModel;

        #endregion

        #region UnityAnimatorEvent

        private void Awake()
        {
            m_playableDirector.played +=  Director_Played;
            m_playableDirector.stopped += Director_Stopped;
        }

        // <summary>
        // ïKéEãZanimationÇÃäJén
        // </summary>
        void OfficeWorkerDeathblowStart()
        {
            m_playableDirector.Play();

            if (m_officeWorkerStatusManagement.photonView.IsMine)
            {
                m_handOnlyModel.gameObject.SetActive(false);
            }
        }

        // <summary>
        // ïKéEãZanimationÇÃèIóπ
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            if (m_officeWorkerStatusManagement.photonView.IsMine)
            {
                m_handOnlyModel.gameObject.SetActive(true);

                ActivationStatusManagement();

                Invoke(nameof( ExitStatusManagement) , m_officeWorkerStatusManagement.GetSpecialMoveDuration_Seconds());
            }
        }

        #endregion

        private void Director_Stopped(PlayableDirector obj)
        {
            m_basicJump.enabled = true;
            m_basicMovement.enabled = true;
            m_basicViewpoint.enabled = true;
        }

        private void Director_Played(PlayableDirector obj)
        {
            m_basicJump.enabled = false;
            m_basicMovement.enabled = false;
            m_basicViewpoint.enabled = false;
        }


        #region recursive function
        private void ActivationStatusManagement()
        {
            m_officeWorkerStatusManagement.UpdateAttackPower(m_officeWorkerStatusManagement.GetAttackPowerIncrease());
            m_officeWorkerStatusManagement.UpdateMovingSpeed(m_officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            m_officeWorkerStatusManagement.UpdateJumpPower(m_officeWorkerStatusManagement.GetJumpPowerIncrease());

            m_officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        private void ExitStatusManagement()
        {
            m_officeWorkerStatusManagement.UpdateAttackPower(-m_officeWorkerStatusManagement.GetAttackPowerIncrease());
            m_officeWorkerStatusManagement.UpdateMovingSpeed(-m_officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            m_officeWorkerStatusManagement.UpdateJumpPower(-m_officeWorkerStatusManagement.GetJumpPowerIncrease());

            m_officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        #endregion
    }
}