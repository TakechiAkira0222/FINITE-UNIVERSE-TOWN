using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TakechiEngine.PUN;
using TakechiEngine.PUN.Information;
using TakechiEngine.PUN.ServerConnect.Joined;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointGameManagement : TakechiJoinedPunCallbacks
    {
        #region serializeField

        [SerializeField] private List<GameObject> m_areaLocationList = new List<GameObject>();
        [SerializeField] private int    m_intervalTime_Second = 0;
        [SerializeField] private int    m_victoryConditionPoints = 5000;


        #endregion

        #region private Variable
        private bool m_isGameStart = false;
        private bool m_isGameStop = false;
        private bool m_isGameEnd = false;
        private int m_pointIocationindex = 0;
        private int m_gameFrameCunt = 0; 
        private int m_gameTimeCunt_Second => (int)Mathf.Ceil( m_gameFrameCunt / 1f / Time.deltaTime);
        private string m_judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;

        #endregion
        public event Action ChangePointIocation = delegate { };
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable
        public void SetTeamAPoint( int value)
        {
            setCurrentRoomCustomProperties( HardPointStatusKey.teamAPoint, GetTeamAPoint() + value);
        }

        public void SetTeamBPoint( int value) 
        {
            setCurrentRoomCustomProperties( HardPointStatusKey.teamBPoint, GetTeamBPoint() + value);
        }

        public void SetGameStart(bool flag) { m_isGameStart = flag; }
        public void SetGameStop(bool flag) { m_isGameStop = flag; }
        public void SetGameEnd(bool flag) { m_isGameEnd = flag; }

        #endregion

        #region getVariable

        public int    GetTeamAPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamAPoint]; }
        public int    GetTeamBPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamBPoint]; }
        public int    GetGameTimeCunt_Second() { return m_gameTimeCunt_Second; }
        public int    GetGameFrameCunt() {return m_gameFrameCunt; }
        public bool   GetGameStart() { return m_isGameStart; }
        public bool   GetGameStop() { return m_isGameStop; }
        public bool   GetGameEnd() { return m_isGameEnd; }
        public string GetJudgmentTagName() { return m_judgmentTagName; }

        #endregion

        private void Awake()
        {
            SetGameStart(true);

            setCurrentRoomCustomProperties(HardPointStatusKey.teamAPoint, 0);
            Debug.Log(" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.teamAPoint, 0)");

            setCurrentRoomCustomProperties(HardPointStatusKey.teamBPoint, 0);
            Debug.Log(" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.teamBPoint, 0)");

            setCurrentRoomCustomProperties(RoomStatusKey.victoryPointKey, m_victoryConditionPoints);
            Debug.Log(" <color=yellow>setCurrentRoomCustomProperties</color>( RoomStatusKey.victoryPointKey, m_victoryConditionPoints)");
        }

        #region unity event
        public override void OnEnable()  
        {
            ChangePointIocation += LocationChange;
            Debug.Log("ChangePointIocation += <color=yellow>LocationChange</color>");

            TeamAToVictory += () => 
            {
                SetGameEnd(true);
                SceneManager.LoadScene(1);
                LeaveRoom();
                Debug.Log("TeamAVictory"); 
            };
            TeamBToVictory += () => 
            {
                SetGameEnd(true);
                SceneManager.LoadScene(1);
                LeaveRoom();
                Debug.Log("TeamBVictory"); 
            };
        }

        public override void OnDisable() 
        { 
            ChangePointIocation -= LocationChange;
            Debug.Log("ChangePointIocation -= <color=yellow>LocationChange</color>");

            TeamAToVictory -= () =>
            {
                SetGameEnd(true);
                SceneManager.LoadScene(1);
                LeaveRoom();
                Debug.Log("TeamAVictory");
            };

            TeamBToVictory -= () => 
            {
                SetGameEnd(true);
                SceneManager.LoadScene(1);
                LeaveRoom();
                Debug.Log("TeamBVictory");
            };
        }

        private void FixedUpdate()
        {
            if ( !m_isGameStart || m_isGameStop || m_isGameEnd) return;

            float fps = 1f / Time.deltaTime;

            if (( m_gameFrameCunt / fps) % m_intervalTime_Second == 0) { ChangePointIocation(); }

            m_gameFrameCunt += 1;

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamAPoint] >= m_victoryConditionPoints) { TeamAToVictory(); };
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamBPoint] >= m_victoryConditionPoints) { TeamBToVictory(); };
        }

        #endregion

        #region recursive finction
        private void LocationChange()
        {
            Debug.Log("<color=green> LocationChange </color> to call.");
            foreach ( GameObject o in m_areaLocationList) { o.SetActive(false); }
            m_areaLocationList[ m_pointIocationindex % m_areaLocationList.Count].SetActive(true);

            m_pointIocationindex += 1;
        }

        #endregion
    }
}
