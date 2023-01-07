using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Jump;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

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
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;

        #endregion

        private OfficeWorkerStatusManagement officeWorkerStatusManagement => m_officeWorkerStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        private Animator handOnlyModelAnimator => officeWorkerStatusManagement.GetHandOnlyModelAnimator();
        private Animator handNetworkModelAnimator => officeWorkerStatusManagement.GetNetworkModelAnimator();


        #region UnityAnimatorEvent

        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_weightTemporaryComplement = 0;

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
            officeWorkerStatusManagement.SetIsKinematic(true);

            m_weightTemporaryComplement = handNetworkModelAnimator.GetLayerWeight(handNetworkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight( handNetworkModelAnimator, AnimatorLayers.overrideLayer, 0.1f);

            if (!officeWorkerStatusManagement.photonView.IsMine) return;
          
            officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(false);
        }

        // <summary>
        // ïKéEãZanimationÇÃèIóπ
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            officeWorkerStatusManagement.SetIsKinematic(false);

            SetLayerWeight( handNetworkModelAnimator, AnimatorLayers.overrideLayer, m_weightTemporaryComplement);

            if (!officeWorkerStatusManagement.photonView.IsMine) return;

            ActivationStatusManagement();
            Invoke(nameof( ExitStatusManagement), officeWorkerStatusManagement.GetDeathblowMoveDuration_Seconds());

            officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(true);
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

        private void SetLayerWeight(Animator animator, string layerName, float weight)
        {
            int LayerIndex = animator.GetLayerIndex(layerName);
            animator.SetLayerWeight(LayerIndex, weight);
            Debug.Log($" handNetworkModelAnimator.<color=yellow>SetLayerWeight</color>({LayerIndex}, {weight}); ");
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