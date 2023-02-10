using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.GameManagerSystem.Domination
{
    [Serializable]
    [CreateAssetMenu(fileName = "DominationGameManagerParameters", menuName = "DominationGameManagerParameters")]
    public class DominationGameManagerParameters : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Range( 1, 3), Tooltip("intervalTime")]
        private int m_intervalTime_Second = 1;
        [SerializeField, Range( 250, 1000), Tooltip("victoryConditionPoint")]
        private int m_victoryConditionPoints = 500;
        [SerializeField, Range( 3, 10), Tooltip("endPerformanceTime")]
        private int m_endPerformanceTime_Seconds = 3;
        [SerializeField, Range( 50, 150), Tooltip("endPerformanceTime")]  
        private int m_areaLocationMaxPoints = 100;

        [SerializeField , Tooltip(" エリア　ポイント上昇 音")]
        private AudioClip m_risingAreaPointsSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_risingAreaPointsSoundClipVolume = 0.5f;

        //[SerializeField, Tooltip("　チーム ポイント上昇 音")]
        //private AudioClip m_risingTeamPointsSoundClip;
        //[SerializeField, Range( 0f, 1f)]
        //private float m_risingTeamPointsSoundClipVolume = 0.5f;


        #endregion

        #region GetStatusFunction
        public int GetIntervalTime_Second() { return m_intervalTime_Second; }
        public int GetVictoryConditionPoints() { return m_victoryConditionPoints; }
        public int GetEndPerformanceTime_Seconds() { return m_endPerformanceTime_Seconds; }
        public int GetAreaLocationMaxPoints() { return m_areaLocationMaxPoints; }
        public AudioClip GetRisingAreaPointsSoundClip() { return m_risingAreaPointsSoundClip; }
        public float GetRisingAreaPointsSoundClipVolume() { return m_risingAreaPointsSoundClipVolume; }

        //public AudioClip GetRisingTeamPointsSoundClip() { return m_risingTeamPointsSoundClip; }
        //public float GetRisingTeamPointsSoundClipVolume() { return m_risingTeamPointsSoundClipVolume; }

        #endregion
    }
}