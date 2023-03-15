using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.GameManagerSystem.Hardpoint
{
    [Serializable]
    [CreateAssetMenu(fileName = "HardpointGameManagerParameters", menuName = "GameManagerData/HardpointGameManagerParameters")]
    public class HardpointGameManagerParameters : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Range(30, 90), Tooltip("intervalTime")]
        private int m_intervalTime_Seconds = 60;
#if UNITY_EDITOR
        [SerializeField, Range(100, 5000), Tooltip("victoryConditionPoint")]
        private int m_victoryConditionPoints = 2000;
#else
        private int m_victoryConditionPoints = 3000;
#endif
        [SerializeField, Range(3, 10), Tooltip("endPerformanceTime")]
        private int m_endPerformanceTime_Seconds = 3;
        [SerializeField, Tooltip(" チーム ポイント 上昇 音")]
        private AudioClip m_risingTeamPointsSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float     m_risingTeamPointsSoundClipVolume = 0.5f;
        [SerializeField, Tooltip(" ゲーム終了 音")]
        private AudioClip m_gameEndSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_gameEndSoundVolume = 0.5f;

        #endregion

        #region GetStatusFunction
        public int GetIntervalTime_Seconds() => m_intervalTime_Seconds;
        public int GetVictoryConditionPoints() => m_victoryConditionPoints;
        public int GetEndPerformanceTime_Seconds() => m_endPerformanceTime_Seconds; 
        public AudioClip GetRisingTeamPointsSoundClip() => m_risingTeamPointsSoundClip; 
        public float GetRisingTeamPointsSoundClipVolume() => m_risingTeamPointsSoundClipVolume; 
        public AudioClip GetGameEndSoundClip() => m_gameEndSoundClip;
        public float GetGameEndSoundVolume() => m_gameEndSoundVolume;

        #endregion
    }
}