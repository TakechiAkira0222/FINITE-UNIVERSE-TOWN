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

namespace Takechi.GameManagerSystem.Domination
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class DominationGameManagement : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;
        [SerializeField] private int m_victoryConditionPoints = 1000;
        [SerializeField] private int m_areaLocationMaxPoints  = 100;
        [SerializeField] private int m_nextSceneNumber = 1;

        #endregion

        #region private variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;
        private bool 　m_isGameStart = false;
        private bool 　m_isGameStop  = false;
        private bool 　m_isGameEnd   = false;
        private int  　m_gameFrameCunt = 0;
        private int    m_gameTimeCunt_Second => (int)Mathf.Ceil(m_gameFrameCunt / 1f / Time.deltaTime);
        private string m_judgmentTagName => SearchForPrefabTag.playerCharacterPrefabTag;

        #endregion

        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable
        public void   SetGameStart(bool flag) { m_isGameStart = flag;}
        public void   SetGameStop(bool flag)  { m_isGameStop = flag;}
        public void   SetGameEnd(bool flag)   { m_isGameEnd = flag;}

        #endregion

        #region getVariable
        public RoomStatusManagement GetMyRoomStatusManagement() { return roomStatusManagement; }

        public bool   GetGameStart() { return m_isGameStart; }
        public bool   GetGameStop()  { return m_isGameStop; }
        public bool   GetGameEnd()   { return m_isGameEnd; }

        public int    GetGameTimeCunt_Second() { return m_gameTimeCunt_Second; }
        public int    GetGameFrameCunt() { return m_gameFrameCunt; }
        public string GetJudgmentTagName() { return m_judgmentTagName; }

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
            if (!m_isGameStart || m_isGameStop || m_isGameEnd) return;

            float fps = 1f / Time.deltaTime;

            m_gameFrameCunt += 1;

            if ((m_gameFrameCunt / fps) % 1 == 0)
            {
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationAPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationBPoint_domination());
                JudgmentToAcquirePoints(roomStatusManagement.GetAreaLocationCPoint_domination());
            }

            if (roomStatusManagement.GetTeamAPoint_domination() >= m_victoryConditionPoints) { TeamAToVictory(); };
            if (roomStatusManagement.GetTeamBPoint_domination() >= m_victoryConditionPoints) { TeamBToVictory(); };
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
