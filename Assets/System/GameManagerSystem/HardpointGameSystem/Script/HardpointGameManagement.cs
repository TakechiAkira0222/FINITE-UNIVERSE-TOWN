using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointGameManagement : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_areaLocationList = new List<GameObject>();
        [SerializeField] private float m_gameTime = 250;
        [SerializeField] private float m_victoryConditionPoints = 200;
        [SerializeField] private float m_gameTimeCunt = 0;

        public event Action ChangePointIocation = delegate { };

        private float m_teamAPoint = 0;
        private float m_teamBPoint = 0;

        private int   m_pointIocationindex = 0;
        private int   m_intervalTime = 0;

        private void OnEnable()
        {
            ChangePointIocation += () => { m_pointIocationindex += 1; };      
        }

        private void OnDisable()
        {
            ChangePointIocation -= () => { m_pointIocationindex += 1; };
        }

        private void Update()
        {
            m_gameTimeCunt += Time.deltaTime;

            if( m_gameTimeCunt % m_intervalTime == 0) ChangePointIocation();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }

        private void OnTriggerStay(Collider other)
        {
            
        }


        private void IncreaseInPoints(ref float point)
        {

        }
    }
}
