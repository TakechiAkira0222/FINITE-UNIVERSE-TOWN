using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificParameters.Slime
{
    [Serializable]
    [CreateAssetMenu(fileName = "SlimeSpecificParameters", menuName = "SlimeData/SlimeSpecificParameters")]
    public class SlimeSpecificParameters : ScriptableObject
    {
        [SerializeField, Range( 5, 15), Tooltip("トルネード　継続時間")] 
        private int trnadoEffectDsuration_seconds = 10;
        [SerializeField, Range( 5, 15), Tooltip(" 透明化　継続時間") ]
        private int transparencyDsuration_seconds = 15;
        [SerializeField, Range(5, 15), Tooltip(" スタン　継続時間")]
        private int stanDsuration_seconds = 15;

        #region Get function
        public int GetTrnadoEffectDsuration_seconds() { return trnadoEffectDsuration_seconds; }
        public int GetTransparencyDsuration_seconds() { return transparencyDsuration_seconds; }
        public int GetStanDsuration_seconds() { return stanDsuration_seconds; }

        #endregion
    }
}
