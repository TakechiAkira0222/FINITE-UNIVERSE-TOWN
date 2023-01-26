using Photon.Pun;

using System;
using System.Collections.Generic;

using UnityEngine;

using TakechiEngine.PUN;
using Takechi.CharacterController.RoomStatus;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
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
        private int      synchroTimeBeforeGameStart_Seconds => NetworkSyncSettings.synchroTimeBeforeGameStart_Seconds;
        private string   judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;
        private float    m_gameTimeCunt_Seconds = 0;
        private int      m_pointIocationindex = 0;

        #endregion
        public event Action ChangePointIocation = delegate { };
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable

        #endregion

        #region getVariable
        public RoomStatusManagement GetMyRoomStatusManagement() { return roomStatusManagement; }
        public float    GetGameTimeCunt_Seconds() { return m_gameTimeCunt_Seconds; }
        public string   GetJudgmentTagName() { return  judgmentTagName; }


        #endregion

        private void Reset()
        {
            m_roomStatusManagement = this.transform.GetComponent<RoomStatusManagement>();
        }

        private void Start()
        {
            roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
            Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
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
                roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamAVictory"); 
            };
            TeamBToVictory += () => 
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
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
                roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamAVictory");
            };

            TeamBToVictory -= () => 
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamBVictory");
            };
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient) return;

            if (roomStatusManagement.GetGameRunning())
            {
                m_gameTimeCunt_Seconds += Time.deltaTime;

                if (m_gameTimeCunt_Seconds >= m_intervalTime_Second)
                {
                    m_gameTimeCunt_Seconds = 0;
                    ChangePointIocation();
                }

                if (roomStatusManagement.GetTeamAPoint_hardPoint() >= m_victoryConditionPoints) { TeamAToVictory(); };
                if (roomStatusManagement.GetTeamBPoint_hardPoint() >= m_victoryConditionPoints) { TeamBToVictory(); };
            }
            else
            {
                m_gameTimeCunt_Seconds += Time.deltaTime;

                if ( m_gameTimeCunt_Seconds >= synchroTimeBeforeGameStart_Seconds)
                {
                    m_gameTimeCunt_Seconds = 0;
                    ChangePointIocation();
                    roomStatusManagement.SetGameState(RoomStatusName.GameState.running);
                    Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.running)");
                }
            }
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
