using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.GameManagerSystem.Domination
{
    [Serializable]
    [CreateAssetMenu(fileName = "DominationGameManagerParameters", menuName = "GameManagerData/DominationGameManagerParameters")]
    public class DominationGameManagerParameters : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Range( 1, 3), Tooltip("intervalTime")]
        private int m_intervalTime_Seconds = 1;

#if UNITY_EDITOR
        [SerializeField, Range(250, 1000), Tooltip("victoryConditionPoint")]
        private int m_victoryConditionPoints = 500;
#else
        private int m_victoryConditionPoints = 500;
#endif

        [SerializeField, Range( 3, 10), Tooltip("endPerformanceTime")]
        private int m_endPerformanceTime_Seconds = 3;
        [SerializeField, Range( 50, 150), Tooltip("endPerformanceTime")]  
        private int m_areaLocationMaxPoints = 100;

        [SerializeField , Tooltip(" エリア　ポイント上昇 音")]
        private AudioClip m_risingAreaPointsSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float     m_risingAreaPointsSoundClipVolume = 0.5f;

        [SerializeField, Tooltip(" ゲーム終了 音")]
        private AudioClip m_gameEndSoundClip;
        [SerializeField, Range( 0f, 1f)]
        private float     m_gameEndSoundVolume = 0.5f;

        #endregion

        #region GetStatusFunction
        public int GetIntervalTime_Seconds() => m_intervalTime_Seconds;
        public int GetVictoryConditionPoints() => m_victoryConditionPoints;
        public int GetEndPerformanceTime_Seconds() => m_endPerformanceTime_Seconds;
        public int GetAreaLocationMaxPoints() => m_areaLocationMaxPoints;
        public AudioClip GetRisingAreaPointsSoundClip() => m_risingAreaPointsSoundClip;
        public float GetRisingAreaPointsSoundClipVolume() => m_risingAreaPointsSoundClipVolume;
        public AudioClip GetGameEndSoundClip() => m_gameEndSoundClip;
        public float GetGameEndSoundVolume()   => m_gameEndSoundVolume;

        #endregion
    }
}