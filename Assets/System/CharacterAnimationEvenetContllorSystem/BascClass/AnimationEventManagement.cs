using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Reference;
using TakechiEngine.PUN;
using UnityEngine;
using UnityEngine.Playables;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.AnimationEvent
{
    public class AnimationEventManagement : TakechiPunCallbacks
    {
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== CharacterControllerReferenceManagement ===")]
        [SerializeField] private CharacterControllerReferenceManagement m_characterControllerReferenceManagement;
        protected CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        protected CharacterControllerReferenceManagement controllerReferenceManagement => m_characterControllerReferenceManagement;

        protected void SetLayerWeight(Animator animator, string layerName, float weight)
        {
            int LayerIndex = animator.GetLayerIndex(layerName);
            animator.SetLayerWeight(LayerIndex, weight);
            Debug.Log($" handNetworkModelAnimator.<color=yellow>SetLayerWeight</color>({LayerIndex}, {weight}); ");
        }
    }
}
