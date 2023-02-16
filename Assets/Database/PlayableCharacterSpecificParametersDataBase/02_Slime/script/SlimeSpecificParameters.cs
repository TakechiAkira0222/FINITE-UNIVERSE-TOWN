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
        [SerializeField, Range(1, 3), Tooltip("���C�U�[�@�p������")]
        private float m_attackLaserEffectDsuration_seconds;
        [SerializeField, Range(0.1f, 1f), Tooltip(" AI�X���C���@�X�s�[�h�@�ω���")]
        private float m_amountOfSpeedChange;
        [SerializeField, Range( 10, 20),Tooltip(" AI�X���C���@�p������")]
        private int m_aiSlimeDsuration_seconds;
        [SerializeField, Range( 5, 15), Tooltip("�g���l�[�h�@�p������")] 
        private int trnadoEffectDsuration_seconds = 10;
        [SerializeField, Range( 5, 15), Tooltip(" �������@�p������") ]
        private int transparencyDsuration_seconds = 15;
        [SerializeField, Range(5, 15), Tooltip(" �X�^���@�p������")]
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
