using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TakechiEngine.PUN.Information;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;
using TakechiEngine.PUN.ServerConnect.Joined;
using UnityEngine.SceneManagement;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationGameManagement : TakechiJoinedPunCallbacks
    {
        #region serializeField
        [SerializeField] private int m_victoryConditionPoints = 1000;
        [SerializeField] private int m_areaLocationMaxPoints  = 100;
        [SerializeField] private int m_nextSceneNumber = 1;
        [SerializeField] private string m_judgmentTagName = "PlayerCharacter";
        #endregion

        #region private variable
        private bool m_isGameStart = false;
        private bool m_isGameStop  = false;
        private bool m_isGameEnd   = false;
        private int  m_gameFrameCunt = 0;
        private int  m_gameTimeCunt_Second => (int)Mathf.Ceil(m_gameFrameCunt / 1f / Time.deltaTime);

        #endregion

        public event Action TeamAToVictory = delegate { };
        public event Action TeamBToVictory = delegate { };

        #region setVariable
        public void SetTeamAPoint(int value) { setCurrentRoomCustomProperties( DominationStatusKey.teamAPoint, GetTeamAPoint() + value); }
        public void SetTeamBPoint(int value) { setCurrentRoomCustomProperties( DominationStatusKey.teamBPoint, GetTeamBPoint() + value); }
        public void SetAreaLocationAPoint(int value) { setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationAPoint, Mathf.Clamp( GetAreaLocationAPoint() + value, 0, GetAreaLocationMaxPoint())); }
        public void SetAreaLocationBPoint(int value) { setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationBPoint, Mathf.Clamp( GetAreaLocationBPoint() + value, 0, GetAreaLocationMaxPoint())); }
        public void SetAreaLocationCPoint(int value) { setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationCPoint, Mathf.Clamp( GetAreaLocationCPoint() + value, 0, GetAreaLocationMaxPoint())); }
        public void SetGameStart(bool flag) { m_isGameStart = flag; }
        public void SetGameStop(bool flag)  { m_isGameStop = flag; }
        public void SetGameEnd(bool flag)   { m_isGameEnd = flag; }

        #endregion

        #region getVariable

        public int    GetTeamAPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamAPoint]; }
        public int    GetTeamBPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamBPoint]; }
        public int    GetAreaLocationAPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationAPoint]; }
        public int    GetAreaLocationBPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationBPoint]; }
        public int    GetAreaLocationCPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationCPoint]; }
        public int    GetAreaLocationMaxPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationMaxPoint]; }
        public int    GetGameTimeCunt_Second() { return m_gameTimeCunt_Second; }
        public int    GetGameFrameCunt() { return m_gameFrameCunt; }
        public bool   GetGameStart() { return m_isGameStart; }
        public bool   GetGameStop()  { return m_isGameStop; }
        public bool   GetGameEnd()   { return m_isGameEnd; }
        public string GetJudgmentTagName() { return m_judgmentTagName; }

        #endregion

        private void Awake()
        {
            SetGameStart(true);

            setCurrentRoomCustomProperties( DominationStatusKey.teamAPoint, 0);
            Debug.Log(" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.teamAPoint, 0)");

            setCurrentRoomCustomProperties( DominationStatusKey.teamBPoint, 0);
            Debug.Log(" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.teamBPoint, 0)");

            setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationAPoint, m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.AreaLocationAPoint, {m_areaLocationMaxPoints / 2})");

            setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationBPoint, m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.AreaLocationBPoint, {m_areaLocationMaxPoints / 2})");

            setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationCPoint, m_areaLocationMaxPoints / 2);
            Debug.Log($" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.AreaLocationCPoint, {m_areaLocationMaxPoints / 2})");

            setCurrentRoomCustomProperties( DominationStatusKey.AreaLocationMaxPoint, m_areaLocationMaxPoints);
            Debug.Log($" <color=yellow>setCurrentRoomCustomProperties</color>( <color=green>RoomTeamStatusKey</color>.AreaLocationCPoint, { m_areaLocationMaxPoints})");

            setCurrentRoomCustomProperties( RoomStatusKey.victoryPointKey, m_victoryConditionPoints);
            Debug.Log($" <color=yellow>setCurrentRoomCustomProperties</color>( RoomStatusKey.victoryPointKey, { m_victoryConditionPoints})");
        }

        #region unity event
        public override void OnEnable()
        {
            TeamAToVictory += () =>
            {
                SetGameEnd(true);
                SceneManager.LoadScene(m_nextSceneNumber);
                LeaveRoom();
                Debug.Log("TeamAVictory");
            };
            TeamBToVictory += () =>
            {
                SetGameEnd(true);
                SceneManager.LoadScene(m_nextSceneNumber);
                LeaveRoom();
                Debug.Log("TeamBVictory");
            };
        }

        public override void OnDisable()
        {
            TeamAToVictory -= () =>
            {
                SetGameEnd(true);
                SceneManager.LoadScene(m_nextSceneNumber);
                LeaveRoom();
                Debug.Log("TeamAVictory");
            };
            TeamBToVictory -= () =>
            {
                SetGameEnd(true);
                SceneManager.LoadScene(m_nextSceneNumber);
                LeaveRoom();
                Debug.Log("TeamBVictory");
            };
        }

        private void FixedUpdate()
        {
            if ( !m_isGameStart || m_isGameStop || m_isGameEnd) return;

            float fps = 1f / Time.deltaTime;

            m_gameFrameCunt += 1;

            if ((m_gameFrameCunt / fps) % 1 == 0)
            {
                JudgmentToAcquirePoints( GetAreaLocationAPoint());
                JudgmentToAcquirePoints( GetAreaLocationBPoint());
                JudgmentToAcquirePoints( GetAreaLocationCPoint());
            }

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[ DominationStatusKey.teamAPoint] >= m_victoryConditionPoints) { TeamAToVictory(); };
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[ DominationStatusKey.teamBPoint] >= m_victoryConditionPoints) { TeamBToVictory(); };
        }

        #endregion

        #region recursive finction
        private void JudgmentToAcquirePoints(int areaLocationPoint)
        {
            if (areaLocationPoint > GetAreaLocationMaxPoint() / 2) { SetTeamAPoint(1); }
            else if (areaLocationPoint < GetAreaLocationMaxPoint() / 2) { SetTeamBPoint(1); }
        }

        #endregion
    }
}
