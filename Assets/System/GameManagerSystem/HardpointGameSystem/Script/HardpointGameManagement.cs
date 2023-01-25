using Photon.Pun;

using System;
using System.Collections.Generic;

using UnityEngine;

using TakechiEngine.PUN;
using Takechi.CharacterController.RoomStatus;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;

namespace Takechi.GameManagerSystem.Hardpoint
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class HardpointGameManagement : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;
        [SerializeField] private List<GameObject> m_areaLocationList = new List<GameObject>();
        [SerializeField] private int    m_intervalTime_Second = 0;
        [SerializeField] private int    m_victoryConditionPoints = 5000;

        #endregion

        #region private Variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private bool   m_isGameStart = false;
        private bool   m_isGameStop = false;
        private bool   m_isGameEnd = false;
        private int    m_pointIocationindex = 0;
        private int    m_gameFrameCunt = 0; 
        private int    m_gameTimeCunt_Second => (int)Mathf.Ceil( m_gameFrameCunt / 1f / Time.deltaTime);
        private string m_judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;

        #endregion
        public event Action ChangePointIocation = delegate { };
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable
        public void SetGameStart(bool flag) { m_isGameStart = flag;}
        public void SetGameStop(bool flag) { m_isGameStop = flag;}
        public void SetGameEnd(bool flag) { m_isGameEnd = flag;}

        #endregion

        #region getVariable
        public RoomStatusManagement GetMyRoomStatusManagement() { return roomStatusManagement; }

        public bool   GetGameStart() { return m_isGameStart; }
        public bool   GetGameStop() { return m_isGameStop; }
        public bool   GetGameEnd() { return m_isGameEnd; }

        public int    GetGameTimeCunt_Second() { return m_gameTimeCunt_Second; }
        public string GetJudgmentTagName() { return m_judgmentTagName; }
        public int    GetGameFrameCunt() {return m_gameFrameCunt; }


        #endregion

        private void Reset()
        {
            m_roomStatusManagement = this.transform.GetComponent<RoomStatusManagement>();
        }

        private void Awake()
        {
            SetGameStart(true);
        }

        private void Start()
        {
            //roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
            //Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
            roomStatusManagement.SetTeamAPoint_hardPoint(0);
            Debug.Log($" <color=yellow>SetTeamAPoint_hardPoint</color>(0)");
            roomStatusManagement.SetTeamBPoint_hardPoint(0);
            Debug.Log($" <color=yellow>SetTeamBPoint_hardPoint</color>(0)");
            roomStatusManagement.SetVictoryPoint(m_victoryConditionPoints);
            Debug.Log($" <color=yellow>SetVictoryPoint_domination</color>({ m_victoryConditionPoints})");
        }

        #region unity event
        public override void OnEnable()  
        {
            ChangePointIocation += LocationChange;
            Debug.Log("ChangePointIocation += <color=yellow>LocationChange</color>");

            TeamAToVictory += () => 
            {
                SetGameEnd(true);
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamAVictory"); 
            };
            TeamBToVictory += () => 
            {
                SetGameEnd(true);
                SceneSyncChange(SceneName.resultScene);
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
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamAVictory");
            };

            TeamBToVictory -= () => 
            {
                SetGameEnd(true);
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamBVictory");
            };
        }

        private void FixedUpdate()
        {
            if ( !m_isGameStart || m_isGameStop || m_isGameEnd) return;

            float fps = 1f / Time.deltaTime;

            if (( m_gameFrameCunt / fps) % m_intervalTime_Second == 0) { ChangePointIocation(); }

            m_gameFrameCunt += 1;

            if (roomStatusManagement.GetTeamAPoint_hardPoint() >= m_victoryConditionPoints) { TeamAToVictory(); };
            if (roomStatusManagement.GetTeamBPoint_hardPoint() >= m_victoryConditionPoints) { TeamBToVictory(); };
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
