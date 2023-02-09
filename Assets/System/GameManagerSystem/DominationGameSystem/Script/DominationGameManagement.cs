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
using Photon.Pun.UtilityScripts;

namespace Takechi.GameManagerSystem.Domination
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class DominationGameManagement : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== DominationGameManagerParameter ===")]
        [SerializeField] private DominationGameManagerParameters m_dominationGameManagerParameters;
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;
        [Header("=== Script Setting ===")]
        [SerializeField] private PhotonView m_thisPhtonView;

        #endregion

        #region private variable
        private DominationGameManagerParameters gameManagerParameters => m_dominationGameManagerParameters;
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private PhotonView thisPhtonView => m_thisPhtonView;
        private int    victoryConditionPoints     => gameManagerParameters.GetVictoryConditionPoints();
        private int    endPerformanceTime_Seconds => gameManagerParameters.GetEndPerformanceTime_Seconds();
        private int    intervalTime_Second        => gameManagerParameters.GetIntervalTime_Second();
        private int    areaLocationMaxPoints => gameManagerParameters.GetAreaLocationMaxPoints();
        private int    synchroTimeBeforeGameStart_Seconds => NetworkSyncSettings.synchroTimeBeforeGameStart_Seconds;
        private string judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;

        
        private Dictionary<string, Action> m_gameMain = new Dictionary<string, Action>();
        private float  m_gameTimeCunt_Seconds = 0;
        private bool   m_localGameEnd = false;

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
            if (!PhotonNetwork.InRoom) return;

            m_gameMain[roomStatusManagement.GetGameState()]();
        }

        #endregion

        #region set up finction
        private void setupOfStart()
        {
            roomStatusManagement.SetGameState(RoomStatusName.GameState.beforeStart);
            Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart)");
            roomStatusManagement.SetVictoryPoint(victoryConditionPoints);
            Debug.Log($" <color=yellow>SetVictoryPoint_domination</color>( {victoryConditionPoints})");
            roomStatusManagement.SetTeamAPoint_domination(0);
            Debug.Log(" <color=yellow>SetTeamAPoint_domination</color>(0)");
            roomStatusManagement.SetTeamBPoint_domination(0);
            Debug.Log(" <color=yellow>SetTeamBPoint_domination</color>(0)");
            roomStatusManagement.SetAreaLocationMaxPoint_domination(areaLocationMaxPoints);
            Debug.Log($" <color=yellow>SetAreaLocationMaxPoint_domination</color>( {areaLocationMaxPoints})");
            roomStatusManagement.SetAreaLocationAPoint_domination(areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationAPoint_domination</color>( {areaLocationMaxPoints / 2})");
            roomStatusManagement.SetAreaLocationBPoint_domination(areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationBPoint_domination</color>( {areaLocationMaxPoints / 2})");
            roomStatusManagement.SetAreaLocationCPoint_domination(areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>SetAreaLocationCPoint_domination</color>( {areaLocationMaxPoints / 2})");
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
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamAName);
                SceneSyncChange(SceneName.resultScene);
            };
            TeamBToVictory += () =>
            {
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamBName);
                SceneSyncChange(SceneName.resultScene);
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
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamAName);
                SceneSyncChange(SceneName.resultScene);
            };
            TeamBToVictory -= () =>
            {
                roomStatusManagement.SetVictoryingTeamName(CharacterTeamStatusName.teamBName);
                SceneSyncChange(SceneName.resultScene);
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

            if ( m_gameTimeCunt_Seconds >= intervalTime_Second)
            {
                m_gameTimeCunt_Seconds = 0;
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationAPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationBPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationCPoint_domination());
            }

            if (roomStatusManagement.GetTeamAPoint_domination() >= victoryConditionPoints)
            {
                roomStatusManagement.SetGameState(RoomStatusName.GameState.end);
                Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.end)");

                SetLocalGameEnd(true);

                StartCoroutine(DelayMethod( endPerformanceTime_Seconds, TeamAToVictory));
            };
            if (roomStatusManagement.GetTeamBPoint_domination() >= victoryConditionPoints)
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
