using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TakechiEngine.PUN;
using Takechi.CharacterController.RoomStatus;

using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.GameManagerSystem.Domination
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class DominationGameManagement : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;
        [SerializeField] private PhotonView m_thisPhtonView;
        [SerializeField] private int m_intervalTime_Second = 1;
        [SerializeField] private int m_victoryConditionPoints = 1000;
        [SerializeField] private int m_areaLocationMaxPoints  = 100;
        [SerializeField] private int m_endPerformanceTime_Seconds = 5;

        #endregion

        #region private variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private int    synchroTimeBeforeGameStart_Seconds => NetworkSyncSettings.synchroTimeBeforeGameStart_Seconds;
        private PhotonView thisPhtonView => m_thisPhtonView;
        private string judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;
        private int    endPerformanceTime_Seconds => m_endPerformanceTime_Seconds;

        private float  m_gameTimeCunt_Seconds = 0;

        private bool   m_localGameEnd = false;

        private Dictionary<string, Action> m_gameMain = new Dictionary<string, Action>();

        #endregion

        #region public event
        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #endregion

        #region setVariable
        public void SetLocalGameEnd(bool flag) 
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
        }

        private void Start()
        {
            setupOfStart();
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

            m_gameMain[roomStatusManagement.GetGameState()]();
        }

        #endregion

        #region set up finction
        private void setupOfStart()
        {
            roomStatusManagement.SetGameState(RoomStatusName.GameState.beforeStart);
            Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart)");
            roomStatusManagement.SetVictoryPoint(m_victoryConditionPoints);
            Debug.Log($" <color=yellow>SetVictoryPoint_domination</color>( {m_victoryConditionPoints})");
            roomStatusManagement.SetTeamAPoint_domination(0);
            Debug.Log(" <color=yellow>SetTeamAPoint_domination</color>(0)");
            roomStatusManagement.SetTeamBPoint_domination(0);
            Debug.Log(" <color=yellow>SetTeamBPoint_domination</color>(0)");
            roomStatusManagement.SetAreaLocationMaxPoint_domination(m_areaLocationMaxPoints);
            Debug.Log($" <color=yellow>SetAreaLocationMaxPoint_domination</color>( {m_areaLocationMaxPoints})");
            roomStatusManagement.SetAreaLocationAPoint_domination(m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationAPoint_domination</color>( {m_areaLocationMaxPoints / 2})");
            roomStatusManagement.SetAreaLocationBPoint_domination(m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationBPoint_domination</color>( {m_areaLocationMaxPoints / 2})");
            roomStatusManagement.SetAreaLocationCPoint_domination(m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationCPoint_domination</color>( {m_areaLocationMaxPoints / 2})");
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

            TeamAToVictory += () =>
            {
                SceneSyncChange(SceneName.resultScene);
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamAName);
            };
            TeamBToVictory += () =>
            {
                SceneSyncChange(SceneName.resultScene);
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamBName);
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

            TeamAToVictory -= () =>
            {
                SceneSyncChange(SceneName.resultScene);
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamAName);
            };
            TeamBToVictory -= () =>
            {
                SceneSyncChange(SceneName.resultScene);
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamBName);
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
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationAPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationBPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationCPoint_domination());
            }

            if (roomStatusManagement.GetTeamAPoint_domination() >= m_victoryConditionPoints)
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.end);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.end)");

                SetLocalGameEnd(true);

                StartCoroutine(DelayMethod( endPerformanceTime_Seconds, TeamAToVictory));
            };
            if (roomStatusManagement.GetTeamBPoint_domination() >= m_victoryConditionPoints)
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
        private void JudgmentToAcquirePoints(int areaLocationPoint)
        {
            if ( areaLocationPoint > roomStatusManagement.GetAreaLocationMaxPoint_domination() / 2) { roomStatusManagement.UpdateTeamAPoint_domination(1); }
            else if (areaLocationPoint < roomStatusManagement.GetAreaLocationMaxPoint_domination() / 2) { roomStatusManagement.UpdateTeamBPoint_domination(1); }
        }

        #endregion
    }
}
