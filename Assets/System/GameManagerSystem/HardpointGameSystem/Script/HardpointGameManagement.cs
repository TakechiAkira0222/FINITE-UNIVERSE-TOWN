using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TakechiEngine.PUN;
using TakechiEngine.PUN.Information;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointGameManagement : TakechiPunInformationDisclosure
    {
        #region serializeField

        [SerializeField] private List<GameObject> m_areaLocationList = new List<GameObject>();
        [SerializeField] private int    m_intervalTime_Second = 0;
        [SerializeField] private int    m_victoryConditionPoints = 200;
        [SerializeField] private string m_judgmentTagName = "PlayerCharacter";

        #endregion

        #region private Variable
        private bool m_isGameStart = false;
        private bool m_isGameStop = false;
        private bool m_isGameEnd = false;

        private int m_pointIocationindex = 0;
        private int m_gameFrameCunt = 0; 
        private int m_gameTimeCunt_Second => (int)Mathf.Ceil( m_gameFrameCunt / 1f / Time.deltaTime);
        private int m_teamAPoint = 0;
        private int m_teamBPoint = 0;
        private int teamAPoint => m_teamAPoint;
        private int teamBPoint => m_teamBPoint;
        #endregion

        public event Action ChangePointIocation = delegate { };
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable
        public void SetTeamAPoint( int value) 
        { 
            m_teamAPoint += value;
            if (!PhotonNetwork.IsMasterClient) return;
            setCurrentRoomCustomProperties(RoomTeamStatusKey.teamAPoint, m_teamAPoint);
        }

        public void SetTeamBPoint( int value) 
        { 
            m_teamBPoint += value;
            if (!PhotonNetwork.IsMasterClient) return;
            setCurrentRoomCustomProperties(RoomTeamStatusKey.teamBPoint, m_teamBPoint);
        }

        public void SetGameStart(bool flag) { m_isGameStart = flag; }
        public void SetGameStop(bool flag) { m_isGameStop = flag; }
        public void SetGameEnd(bool flag) { m_isGameEnd = flag; }

        #endregion

        #region getVariable

        public int    GetTeamAPoint() { return teamAPoint; }
        public int    GetTeamBPoint() { return teamBPoint; }
        public int    GetGameTimeCunt_Second() { return m_gameTimeCunt_Second; }
        public int    GetGameFrameCunt() {return m_gameFrameCunt; }
        public bool   GetGameStart() { return m_isGameStart; }
        public bool   GetGameStop() { return m_isGameStop; }
        public bool   GetGameEnd() { return m_isGameEnd; }
        public string GetJudgmentTagName() { return m_judgmentTagName; }

        #endregion

        private void Start()
        {
            setCurrentRoomCustomProperties( RoomTeamStatusKey.teamAPoint, 0);
            setCurrentRoomCustomProperties( RoomTeamStatusKey.teamBPoint, 0);
        }

        #region unity event
        public override void OnEnable()  
        {
            ChangePointIocation += LocationChange;
            TeamAToVictory += () => { Debug.Log("TeamAVictory"); };
            TeamBToVictory += () => { Debug.Log("TeamBVictory"); };
        }

        public override void OnDisable() 
        { 
            ChangePointIocation -= LocationChange;
            TeamAToVictory -= () => { Debug.Log("TeamAVictory"); };
            TeamBToVictory -= () => { Debug.Log("TeamBVictory"); };
        }

        private void FixedUpdate()
        {
            if ( m_isGameStart || m_isGameStop || m_isGameEnd) return;

            float fps = 1f / Time.deltaTime;

            if (( m_gameFrameCunt / fps) % m_intervalTime_Second == 0) { ChangePointIocation(); }

            m_gameFrameCunt += 1;

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint] >= m_victoryConditionPoints) { TeamAToVictory(); };
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint] >= m_victoryConditionPoints) { TeamBToVictory(); };
        }

        #endregion

        #region recursive finction
        private void LocationChange()
        {
            Debug.Log("ChangePoint");

            foreach ( GameObject o in m_areaLocationList) { o.SetActive(false); }
            m_areaLocationList[ m_pointIocationindex % m_areaLocationList.Count].SetActive(true);

            m_pointIocationindex += 1;
        }

        #endregion
    }
}
