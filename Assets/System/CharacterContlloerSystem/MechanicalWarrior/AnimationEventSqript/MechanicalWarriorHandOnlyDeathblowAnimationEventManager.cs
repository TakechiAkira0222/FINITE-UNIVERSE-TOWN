using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class MechanicalWarriorHandOnlyDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField

        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        private MechanicalWarriorStatusManagement mechanicalWarriorStatusManagement => m_mechanicalWarriorStatusManagement;
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;
    }
}