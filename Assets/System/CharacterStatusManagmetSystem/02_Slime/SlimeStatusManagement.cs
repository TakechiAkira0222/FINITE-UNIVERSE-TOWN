using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificParameters.Slime;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    public class SlimeStatusManagement : CharacterStatusManagement
    {
        [Header("=== MechanicalWarreiorStatus Setting===")]
        [SerializeField] private SlimeSpecificParameters m_slimeSpecificParameters;
    }
}
