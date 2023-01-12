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
        [SerializeField] private GameObject m_deathblowAreaEffect;

        #endregion

        private OfficeWorkerStatusManagement officeWorkerStatusManagement => m_officeWorkerStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        private Animator handOnlyModelAnimator => officeWorkerStatusManagement.GetHandOnlyModelAnimator();
        private Animator handNetworkModelAnimator => officeWorkerStatusManagement.GetNetworkModelAnimator();

        #region UnityAnimatorEvent

        /// <summary>
        /// 重みの一時保管
        /// </summary>
        private float m_weightTemporaryComplement = 0;

        private void Awake()
        {
            m_playableDirector.played += Director_Played;
            m_playableDirector.stopped += Director_Stopped;
        }

        // <summary>
        // 必殺技animationの開始
        // </summary>
        void OfficeWorkerDeathblowStart()
        {
            m_playableDirector.Play();

            // rb
            officeWorkerStatusManagement.SetIsKinematic(true);

            // animation weiht
            m_weightTemporaryComplement = handNetworkModelAnimator.GetLayerWeight(handNetworkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, 0.1f);

            if (officeWorkerStatusManagement.photonView.IsMine)
            {
                officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(false);
            }
        }

        // <summary>
        // 必殺技animationの終了
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            if (officeWorkerStatusManagement.photonView.IsMine)
            {
                ActivationStatusManagement();
                Invoke(nameof(ExitStatusManagement), officeWorkerStatusManagement.GetDeathblowMoveDuration_Seconds());
                officeWorkerStatusManagement.GetHandOnlyModelObject().SetActive(true);
            }

            // rb
            officeWorkerStatusManagement.SetIsKinematic(false);

            // animation weiht
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, m_weightTemporaryComplement);

            // effect 
            ActivationDeathblowAreaEffect();
            Invoke(nameof(ExitDeathblowAreaEffect), officeWorkerStatusManagement.GetDeathblowMoveDuration_Seconds());
        }

        #endregion

        private void Director_Stopped(PlayableDirector obj)
        {
            m_characterKeyInputStateManagement.SetOperation(true);
        }

        private void Director_Played(PlayableDirector obj)
        {
            m_characterKeyInputStateManagement.SetOperation(false);
        }

        private void SetLayerWeight(Animator animator, string layerName, float weight)
        {
            int LayerIndex = animator.GetLayerIndex(layerName);
            animator.SetLayerWeight(LayerIndex, weight);
            Debug.Log($" handNetworkModelAnimator.<color=yellow>SetLayerWeight</color>({LayerIndex}, {weight}); ");
        }

        #region recursive function

        /// <summary>
        /// ステータスの変更　開始
        /// </summary>
        private void ActivationStatusManagement()
        {
            officeWorkerStatusManagement.UpdateAttackPower(officeWorkerStatusManagement.GetAttackPowerIncrease());
            officeWorkerStatusManagement.UpdateMovingSpeed(officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            officeWorkerStatusManagement.UpdateJumpPower(officeWorkerStatusManagement.GetJumpPowerIncrease());

            officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// ステータス変更　終了
        /// </summary>
        private void ExitStatusManagement()
        {
            officeWorkerStatusManagement.UpdateAttackPower(-officeWorkerStatusManagement.GetAttackPowerIncrease());
            officeWorkerStatusManagement.UpdateMovingSpeed(-officeWorkerStatusManagement.GetMoveingSpeedIncrease());
            officeWorkerStatusManagement.UpdateJumpPower(-officeWorkerStatusManagement.GetJumpPowerIncrease());

            officeWorkerStatusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// 必殺技areaエフェクト　開始
        /// </summary>
        private void ActivationDeathblowAreaEffect()
        {
            m_deathblowAreaEffect.SetActive(true);
        }

        /// <summary>
        /// 必殺技Areaエフェクト　終了
        /// </summary>
        private void ExitDeathblowAreaEffect()
        {
            m_deathblowAreaEffect.SetActive(false);
        }

        #endregion
    }
}
