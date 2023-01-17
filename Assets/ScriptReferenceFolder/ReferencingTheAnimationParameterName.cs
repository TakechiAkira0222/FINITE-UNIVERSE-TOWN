using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ScriptReference.AnimatorControlVariables
{
    public class ReferencingTheAnimatorControlVariablesName : MonoBehaviour
    {
        public static class AnimatorParameter
        {
            public const string attackParameterName = "Attack";
            public const string deathblowParameterName = "Deathblow";
            public const string damageforceParameterName = "Damageforce";
            public const string movementVectorXParameterName = "movementVectorX";
            public const string movementVectorZParameterName = "movementVectorZ";
            public const string jumpingParameterName = "Jumping";
            public const string dashParameterName = "Dash";
            public const string Ablity1ParameterName = "Ablity1";
            public const string Ablity2ParameterName = "Ablity2";
            public const string Ablity3ParameterName = "Ablity3";
        }

        public static class AnimatorLayers 
        {
            public const string basicLayer = "BasicLayer";
            public const string additionalLayer = "AdditionalLayer";
            public const string overrideLayer = "OverrideLayer";
        }
    }
}