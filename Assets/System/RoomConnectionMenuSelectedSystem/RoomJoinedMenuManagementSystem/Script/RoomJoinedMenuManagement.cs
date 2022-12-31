using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TakechiEngine.PUN.ServerConnect.Joined;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Takechi.UI.RoomJoinedMenu
{
    public class RoomJoinedMenuManagement : TakechiJoinedPunCallbacks
    {
        [SerializeField] private Button m_gameStartButton;

        [SerializeField] private Text m_instansText;

        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;

        [SerializeField] private List<Player> m_playerInRoomList= new List<Player>(4);

        public override void OnEnable()
        {
            base.OnEnable();

            OnlyClientsCanInterfere( m_gameStartButton);

            PlayerListUpdate();
        }

        public void OnSceneSyncChange() { SceneSyncChange(2); }

        public void OnLeaveRoom() { LeaveRoom(); }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            OnlyClientsCanInterfere( m_gameStartButton);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            m_playerInRoomList.Add(newPlayer);

            PlayerListUpdate();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            m_playerInRoomList.Remove(otherPlayer);

            PlayerListUpdate();
        }

        private void PlayerListUpdate()
        {
            DestroyChildObjects( m_teamAContent);
            DestroyChildObjects( m_teamBContent);

            InstantiateTheText(m_instansText, m_playerInRoomList, m_teamAContent.transform);
        }

        private void DestroyChildObjects( GameObject parent)
        {
            foreach (Transform n in parent.transform)
            {
                GameObject.Destroy( n.gameObject);
            }
        }

        private void InstantiateTheText(Text instansText, List<Player> playerInRoomList, Transform parent)
        {
            foreach (Player n in playerInRoomList)
            {
                Debug.Log( n.NickName);
                Instantiate(instansText, parent).text = n.NickName;
            }
        }
    }
}
