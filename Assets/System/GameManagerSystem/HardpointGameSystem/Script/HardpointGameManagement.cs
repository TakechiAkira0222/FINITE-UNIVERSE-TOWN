using Photon.Pun;
using Photon.Realtime;

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
    [RequireComponent(typeof(PhotonView))]
    public class HardpointGameManagement : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;
        [SerializeField] private List<GameObject> m_areaLocationList = new List<GameObject>();
        [SerializeField] private PhotonView m_thisPhtonView;
        [SerializeField] private int m_intervalTime_Second = 60;
        [SerializeField] private int m_victoryConditionPoints = 5000;
        [SerializeField] private int m_endPerformanceTime_Seconds = 5;

        #endregion

        #region private Variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private PhotonView thisPhtonView => m_thisPhtonView;
        private int synchroTimeBeforeGameStart_Seconds => NetworkSyncSettings.synchroTimeBeforeGameStart_Seconds;
        private int endPerformanceTime_Seconds => m_endPerformanceTime_Seconds;
        private string judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;
       
        private Dictionary<string, Action> m_gameMain = new Dictionary<string, Action>();
        private int   m_pointIocationindex = 0;
        private bool  m_localGameEnd = false;
        private float m_gameTimeCunt_Seconds = 0;

        #endregion

        #region public event
        public event Action ChangePointIocation = delegate { };
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #endregion


        #region setVariable
        public void   SetLocalGameEnd(bool flag)
        {
            m_localGameEnd = flag;
            Debug.Log($" <color=yellow>SetLocalGameEnd</color>({flag}) to set.");
        }

        #endregion

        #region getVariable
        public RoomStatusManagement GetMyRoomStatusManagement() { return roomStatusManagement; }
        public float  GetGameTimeCunt_Seconds() { return m_gameTimeCunt_Seconds; }
        public string GetJudgmentTagName() { return judgmentTagName; }
        public bool   GetLocalGameEnd() { return m_localGameEnd; }

        #endregion

        #region unity event
        private void Reset()
        {
            m_roomStatusManagement = this.transform.GetComponent<RoomStatusManagement>();
            m_thisPhtonView = this.transform.GetComponent<PhotonView>(); 
        }
        private void Awake()
        {
            setupOfAwake();
        }

        public override void OnEnable()
        {
            setupOfOnEnable();
        }

        public override void OnDisable()
        {
            setupOfOnDisable();
        }

        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            if (GetLocalGameEnd()) return;
            if (!PhotonNetwork.InRoom) return;

            m_gameMain[roomStatusManagement.GetGameState()]();
        }

        #endregion

        #region set up finction

        private void setupOfAwake()
        {
            roomStatusManagement.SetGameState(RoomStatusName.GameState.beforeStart);
            Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart)");
            roomStatusManagement.SetTeamAPoint_hardPoint(0);
            Debug.Log($" <color=yellow>SetTeamAPoint_hardPoint</color>(0)");
            roomStatusManagement.SetTeamBPoint_hardPoint(0);
            Debug.Log($" <color=yellow>SetTeamBPoint_hardPoint</color>(0)");
            roomStatusManagement.SetVictoryPoint(m_victoryConditionPoints);
            Debug.Log($" <color=yellow>SetVictoryPoint_domination</color>({m_victoryConditionPoints})");
        }
        private void setupOfOnEnable()
        {
            m_gameMain.Add(RoomStatusName.GameState.beforeStart, GameState_BeforeStart);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart, <color=yellow>GameState_BeforeStart</color>)");
            m_gameMain.Add(RoomStatusName.GameState.running, GameState_Running);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.running, <color=yellow>GameState_Running</color>)");
            m_gameMain.Add(RoomStatusName.GameState.end, GameState_End);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.end, <color=yellow>GameState_End</color>)");
            m_gameMain.Add(RoomStatusName.GameState.stopped, GameState_Stopped);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped, <color=yellow>GameState_Stopped</color>)");
            m_gameMain.Add(RoomStatusName.GameState.NULL, GameState_NULL);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.NULL, <color=yellow>GameState_NULL</color>)");

            ChangePointIocation += LocationChange;
            Debug.Log("ChangePointIocation += <color=yellow>LocationChange</color>");

            TeamAToVictory += () =>
            {
                SceneSyncChange(SceneName.resultScene);

                Debug.Log("TeamAVictory");

                //foreach(Player player in PhotonNetwork.PlayerList)
                //{

                //}

            };
            TeamBToVictory += () =>
            {
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamBVictory");
            };
        }
        private void setupOfOnDisable()
        {
            m_gameMain.Remove(RoomStatusName.GameState.beforeStart);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart");
            m_gameMain.Remove(RoomStatusName.GameState.running);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.running");
            m_gameMain.Remove(RoomStatusName.GameState.end);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.end");
            m_gameMain.Remove(RoomStatusName.GameState.stopped);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped");
            m_gameMain.Remove(RoomStatusName.GameState.NULL);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.NULL");

            ChangePointIocation -= LocationChange;
            Debug.Log("ChangePointIocation -= <color=yellow>LocationChange</color>");

            TeamAToVictory -= () =>
            {
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamAVictory");
            };

            TeamBToVictory -= () =>
            {
                SceneSyncChange(SceneName.resultScene);
                Debug.Log("TeamBVictory");
            };
        }

        #endregion

        #region main game finction
        private void GameState_BeforeStart()
        {
            m_gameTimeCunt_Seconds += Time.deltaTime;

            if (m_gameTimeCunt_Seconds >= synchroTimeBeforeGameStart_Seconds)
            {
                m_gameTimeCunt_Seconds = 0;
                ChangePointIocation();
                roomStatusManagement.SetGameState(RoomStatusName.GameState.running);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.running)");
            }
        }

        private void GameState_Running()
        {
            m_gameTimeCunt_Seconds += Time.deltaTime;

            if (m_gameTimeCunt_Seconds >= m_intervalTime_Second)
            {
                m_gameTimeCunt_Seconds = 0;
                ChangePointIocation();
            }

            if (roomStatusManagement.GetTeamAPoint_hardPoint() >= m_victoryConditionPoints)
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.end);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.end)");

                SetLocalGameEnd(true);

                StartCoroutine(DelayMethod(endPerformanceTime_Seconds, TeamAToVictory));
            };

            if (roomStatusManagement.GetTeamBPoint_hardPoint() >= m_victoryConditionPoints)
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.end);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.end)");

                SetLocalGameEnd(true);

                StartCoroutine(DelayMethod(endPerformanceTime_Seconds, TeamBToVictory));
            };
        }

        private void GameState_End()
        {

        }

        private void GameState_Stopped()
        {

        }

        private void GameState_NULL()
        {
          
            Debug.LogError(" Game state not set");
        }

        #endregion

        #region recursive finction
        private void LocationChange()
        {
            thisPhtonView.RPC(nameof(RPC_LocationChange), RpcTarget.AllBufferedViaServer);
        }

        #endregion

        [PunRPC]
        private void RPC_LocationChange()
        {
            Debug.Log("<color=green> LocationChange </color> to call.");
            foreach (GameObject o in m_areaLocationList) { o.SetActive(false); }

            m_areaLocationList[m_pointIocationindex % m_areaLocationList.Count].SetActive(true);

            m_pointIocationindex += 1;
        }
    }
}
