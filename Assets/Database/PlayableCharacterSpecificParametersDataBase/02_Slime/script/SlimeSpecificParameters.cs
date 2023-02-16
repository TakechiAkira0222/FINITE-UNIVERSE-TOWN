using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Takechi.CharacterController.SpecificParameters.Slime
{
    [Serializable]
    [CreateAssetMenu(fileName = "SlimeSpecificParameters", menuName = "SlimeData/SlimeSpecificParameters")]
    public class SlimeSpecificParameters : ScriptableObject
    {
        [SerializeField, Range(1, 3), Tooltip("レイザー　継続時間")]
        private float m_attackLaserEffectDsuration_seconds;
        [SerializeField, Range(0.1f, 1f), Tooltip(" AIスライム　スピード　変化量")]
        private float m_amountOfSpeedChange;
        [SerializeField, Range( 10, 20),Tooltip(" AIスライム　継続時間")]
        private int m_aiSlimeDsuration_seconds;
        [SerializeField, Range( 5, 15), Tooltip("トルネード　継続時間")] 
        private int trnadoEffectDsuration_seconds = 10;
        [SerializeField, Range( 5, 15), Tooltip(" 透明化　継続時間") ]
        private int transparencyDsuration_seconds = 15;
        [SerializeField, Range(5, 15), Tooltip(" スタン　継続時間")]
        private int stanDsuration_seconds = 15;

        #region Get function
        public float GetAttackLaserEffectDsuration_seconds() => m_attackLaserEffectDsuration_seconds;
        public float GetAmountOfSpeedChange() => m_amountOfSpeedChange;
        public int GetAiSlimeDsuration_seconds() => m_aiSlimeDsuration_seconds;
        public int GetTrnadoEffectDsuration_seconds() => trnadoEffectDsuration_seconds; 
        public int GetTransparencyDsuration_seconds() => transparencyDsuration_seconds; 
        public int GetStanDsuration_seconds() => stanDsuration_seconds; 

        #endregion
    }
}
