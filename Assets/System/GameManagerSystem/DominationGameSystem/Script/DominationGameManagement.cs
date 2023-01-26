using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Takechi.CharacterController.RoomStatus;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using TakechiEngine.PUN;
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
        [SerializeField] private int m_intervalTime_Second = 1;
        [SerializeField] private int m_victoryConditionPoints = 1000;
        [SerializeField] private int m_areaLocationMaxPoints  = 100;

        #endregion

        #region private variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private int    synchroTimeBeforeGameStart_Seconds => NetworkSyncSettings.synchroTimeBeforeGameStart_Seconds;
        private string judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;

        private float  m_gameTimeCunt_Seconds = 0;

        #endregion

        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable

        #endregion

        #region getVariable
        public RoomStatusManagement GetMyRoomStatusManagement() { return roomStatusManagement; }
        public float  GetGameTimeCunt_Seconds() { return m_gameTimeCunt_Seconds; }
        public string GetJudgmentTagName() { return judgmentTagName; }

        #endregion

        private void Reset()
        {
            m_roomStatusManagement = this.transform.GetComponent<RoomStatusManagement>();
        }

        private void Start()
        {
            roomStatusManagement.SetGameState(RoomStatusName.GameState.stopped);
            Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped)");
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

        #region unity event
        public override void OnEnable()
        {
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

                if ( m_gameTimeCunt_Seconds >= m_intervalTime_Second)
                {
                    m_gameTimeCunt_Seconds = 0;
                    JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationAPoint_domination());
                    JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationBPoint_domination());
                    JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationCPoint_domination());
                }

                if (roomStatusManagement.GetTeamAPoint_domination() >= m_victoryConditionPoints) { TeamAToVictory(); };
                if (roomStatusManagement.GetTeamBPoint_domination() >= m_victoryConditionPoints) { TeamBToVictory(); };
            }
            else
            {
                m_gameTimeCunt_Seconds += Time.deltaTime;

                if (m_gameTimeCunt_Seconds >= synchroTimeBeforeGameStart_Seconds)
                {
                    m_gameTimeCunt_Seconds = 0;
                    roomStatusManagement.SetGameState( RoomStatusName.GameState.running);
                    Debug.Log($" <color=yellow>SetGameState</color>(<color=green>RoomStatusName</color>.<color=green>GameState</color>.running)");
                }
            }
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
