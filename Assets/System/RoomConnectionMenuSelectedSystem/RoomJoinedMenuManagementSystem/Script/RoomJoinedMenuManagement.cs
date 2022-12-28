using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TakechiEngine.PUN.ServerConnect.Joined;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Takechi.UI.RoomJoinedMenu
{
    public class RoomJoinedMenuManagement : TakechiJoinedPunCallbacks
    {
        [SerializeField] private Button m_gameStartButton;
        [SerializeField] private List<Player> m_playerInRoomList= new List<Player>(4);

        public override void OnEnable()
        {
            base.OnEnable();

            OnlyClientsCanInterfere(m_gameStartButton);
        }

        public void OnSceneSyncChange()
        {
            SceneSyncChange(2);
        }

        public void OnLeaveRoom() 
        {
            LeaveRoom();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            OnlyClientsCanInterfere(m_gameStartButton);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            m_playerInRoomList.Add(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            m_playerInRoomList.Remove(otherPlayer);
        }
    }
}
