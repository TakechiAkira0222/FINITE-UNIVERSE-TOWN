using System.Collections;
using System.Collections.Generic;
using Takechi.AnimationSystem.IK;
using Takechi.CharacterController.BasicAnimation.Movement;
using UnityEngine;

namespace Takechi.CharacterController.Reference
{
    public class CharacterControllerReferenceManagement : MonoBehaviour
    {
        [SerializeField] private CharacterBasicIKAnimationController characterBasicIKAnimationController;
        [SerializeField] private CharacterBasicMovementAnimationControler characterBasicMovementAnimationControler;

        public CharacterBasicIKAnimationController GetIKAnimationController() { return characterBasicIKAnimationController; }
        public CharacterBasicMovementAnimationControler GetMovementAnimationControler() { return characterBasicMovementAnimationControler; }
    }
}
