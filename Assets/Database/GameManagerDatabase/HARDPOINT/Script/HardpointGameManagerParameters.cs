using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.GameManagerSystem.Hardpoint
{
    [Serializable]
    [CreateAssetMenu(fileName = "HardpointGameManagerParameters", menuName = "HardpointGameManagerParameters")]
    public class HardpointGameManagerParameters : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Range(30, 90), Tooltip("intervalTime")]
        private int m_intervalTime_Second = 60;
        [SerializeField, Range(100, 5000), Tooltip("victoryConditionPoint")]
        private int m_victoryConditionPoints = 2000;
        [SerializeField, Range(3, 10), Tooltip("endPerformanceTime")]
        private int m_endPerformanceTime_Seconds = 3;

        #endregion

        #region GetStatusFunction
        public int GetIntervalTime_Second() { return m_intervalTime_Second; }
        public int GetVictoryConditionPoints() { return m_victoryConditionPoints;}
        public int GetEndPerformanceTime_Seconds() { return m_endPerformanceTime_Seconds; }

        #endregion
    }
}