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

        #endregion

        #region GetStatusFunction
        public int GetIntervalTime_Second() { return m_intervalTime_Second; }
        public int GetVictoryConditionPoints() { return m_victoryConditionPoints; }
        public int GetEndPerformanceTime_Seconds() { return m_endPerformanceTime_Seconds; }
        public int GetAreaLocationMaxPoints() { return m_areaLocationMaxPoints; }

        #endregion
    }
}