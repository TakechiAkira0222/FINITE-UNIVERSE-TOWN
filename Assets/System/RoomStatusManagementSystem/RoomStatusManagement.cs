using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TakechiEngine.PUN.CustomProperties;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;
using UnityEngine.Experimental.GlobalIllumination;

namespace Takechi.CharacterController.RoomStatus
{
    public class RoomStatusManagement : TakechiPunCustomProperties
    {
        #region SerializeField


        #endregion

        #region set function
        /// <summary>
        /// RoomStatusName.GameState Çê›íËÇµÇ‹Ç∑ÅB
        /// </summary>
        /// <param name="state"> RoomStatusName.GameState </param>
        public void SetGameState(string state) { setCurrentRoomCustomProperties(RoomStatusKey.gameStateKey, state); }
        public void SetVictoryPoint(int value) { setCurrentRoomCustomProperties(RoomStatusKey.victoryPointKey, value); }

        // HardPoint
        public void SetTeamAPoint_hardPoint(int value) { setCurrentRoomCustomProperties(HardPointStatusKey.teamAPoint, value); }
        public void SetTeamBPoint_hardPoint(int value) { setCurrentRoomCustomProperties(HardPointStatusKey.teamBPoint, value); }

        // Domination
        public void SetTeamAPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.teamAPoint, value); }
        public void SetTeamBPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.teamBPoint, value); }
        public void SetAreaLocationAPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationAPoint, Mathf.Clamp(value, 0, GetAreaLocationMaxPoint_domination())); }
        public void SetAreaLocationBPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationBPoint, Mathf.Clamp(value, 0, GetAreaLocationMaxPoint_domination())); }
        public void SetAreaLocationCPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationCPoint, Mathf.Clamp(value, 0, GetAreaLocationMaxPoint_domination())); }
        public void SetAreaLocationMaxPoint_domination(int valus) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationMaxPoint, valus); }

        #endregion

        #region get function

        public int   GetVictoryPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]; }
        public bool  GetGameRunning()
        {
            if (!PhotonNetwork.InRoom) return true;

            if (GetGameState() == null) { return true; }
            else if (GetGameState() == RoomStatusName.GameState.running) { return true; }
            else return false;
        }
        public string GetGameState() { return (string)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.gameStateKey]; }

        // HardPoint
        public int GetTeamAPoint_hardPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamAPoint]; }
        public int GetTeamBPoint_hardPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamBPoint]; }

        // Domination
        public int GetTeamAPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamAPoint]; }
        public int GetTeamBPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamBPoint]; }
        public int GetAreaLocationAPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationAPoint]; }
        public int GetAreaLocationBPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationBPoint]; }
        public int GetAreaLocationCPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationCPoint]; }
        public int GetAreaLocationMaxPoint_domination() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationMaxPoint]; }

        #endregion

        #region update function
        // HardPoint
        public void UpdateTeamAPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.teamAPoint, GetTeamAPoint_domination() + value); }
        public void UpdateTeamBPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.teamBPoint, GetTeamBPoint_domination() + value); }

        // Domination
        public void UpdateAreaLocationAPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationAPoint, Mathf.Clamp(GetAreaLocationAPoint_domination() + value, 0, GetAreaLocationMaxPoint_domination())); }
        public void UpdateAreaLocationBPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationBPoint, Mathf.Clamp(GetAreaLocationBPoint_domination() + value, 0, GetAreaLocationMaxPoint_domination())); }
        public void UpdateAreaLocationCPoint_domination(int value) { setCurrentRoomCustomProperties(DominationStatusKey.AreaLocationCPoint, Mathf.Clamp(GetAreaLocationCPoint_domination() + value, 0, GetAreaLocationMaxPoint_domination())); }
        public void UpdateTeamAPoint_hardPoint(int value) { setCurrentRoomCustomProperties(HardPointStatusKey.teamAPoint, GetTeamAPoint_hardPoint() + value); }
        public void UpdateTeamBPoint_hardPoint(int value) { setCurrentRoomCustomProperties(HardPointStatusKey.teamBPoint, GetTeamBPoint_hardPoint() + value); }

        #endregion
    }
}