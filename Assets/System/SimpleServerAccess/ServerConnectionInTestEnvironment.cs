using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;


namespace Takechi.ServerAccess
{
    public class ServerConnectionInTestEnvironment : ServerConnection
    {
        [Header("DefaultRoomSettings")]
        #region SerializeField
        [SerializeField] private int maxPlayers = 4;
        [SerializeField] private string roomName = "TestRoom";
        [SerializeField] private bool m_isOpen = false;
        [SerializeField] private bool m_isVisible = false;
        #endregion

        #region PublicAction
        public event Action OnConnectedAction = delegate { };
        public event Action OnConnectedToMasterAction = delegate { };
        public event Action OnJoinedRoomAction = delegate { };
        public event Action< string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnJoinedLobbyAction = delegate { };
        #endregion

        private void Reset()
        {
            maxPlayers = 4;
            m_isOpen = false;
            m_isVisible = false;
            roomName = "TestRoom";
        }

        private void Awake()
        {
            OnConnectedToMasterAction += JoinLobby;
            OnJoinedLobbyAction += JoinOrCreateRoom;
            OnJoinedRoomAction += GeneratingPrefabs;
            Debug.Log("<color=yellow> AccessingTheTestEnvironment.Awake </color>: Add JoinLobby, JoinOrCreateRoom, GeneratingPrefabs");
        }

        protected override void Start()
        {
            m_customRoomProperties =
                new ExitGames.Client.Photon.Hashtable
                {

                };

            Debug.Log(" CustomRoomProperties : <color=green>Initial setting completed</color>");

            m_customPlayerProperties =
               new ExitGames.Client.Photon.Hashtable
               {

               };

            Debug.Log(" CustomPlayerProperties : <color=green>Initial setting completed</color>");

            base.Start();
        }
        public override void OnConnected()
        {
            base.OnConnected();

            OnConnectedAction();
        }
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            OnConnectedToMasterAction();
        }
        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            SetMyNickName("akira");

            UpdateLocalPlayerProperties( m_customPlayerProperties);

            OnJoinedLobbyAction
                ( roomName, SetRoomOptions( maxPlayers, m_isVisible, m_isOpen), m_customRoomProperties);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
        }

        private void OnDestroy()
        {
            Debug.Log(" <color=green>It was deleted because the construction of the test environment was completed.</color>");
        }
    }
}

