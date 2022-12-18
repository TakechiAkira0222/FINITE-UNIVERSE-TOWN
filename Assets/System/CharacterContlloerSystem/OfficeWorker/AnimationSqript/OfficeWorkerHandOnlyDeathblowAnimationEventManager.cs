using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerHandOnlyDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_officeWorkerStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;

        #endregion

        private OfficeWorkerStatusManagement officeWorkerStatusManagement => m_officeWorkerStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

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

            if (!officeWorkerStatusManagement.photonView.IsMine) return;

            officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(false);
            officeWorkerStatusManagement.SetIsKinematic(true);
        }

        // <summary>
        // ïKéEãZanimationÇÃèIóπ
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            if (!officeWorkerStatusManagement.photonView.IsMine) return;

            officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(true);
            officeWorkerStatusManagement.SetIsKinematic(false);

            ActivationStatusManagement();

            Invoke(nameof( ExitStatusManagement), officeWorkerStatusManagement.GetDeathblowMoveDuration_Seconds());
        }

        #endregion

        private void Director_Stopped(PlayableDirector obj)
        {
            m_characterKeyInputStateManagement.StartInput();
        }

        private void Director_Played(PlayableDirector obj)
        {
            m_characterKeyInputStateManagement.StopInput();
        }

        #region recursive function
        private void ActivationStatusManagement()
        {
            officeWorkerStatusManagement.UpdateAttackPower(officeWorkerStatusManagement.GetAttackPowerIncrease());
            officeWorkerStatusManagement.UpdateMovingSpeed(officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            officeWorkerStatusManagement.UpdateJumpPower(officeWorkerStatusManagement.GetJumpPowerIncrease());

            officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        private void ExitStatusManagement()
        {
            officeWorkerStatusManagement.UpdateAttackPower(-officeWorkerStatusManagement.GetAttackPowerIncrease());
            officeWorkerStatusManagement.UpdateMovingSpeed(-officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            officeWorkerStatusManagement.UpdateJumpPower(-officeWorkerStatusManagement.GetJumpPowerIncrease());

            officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        #endregion
    }
}