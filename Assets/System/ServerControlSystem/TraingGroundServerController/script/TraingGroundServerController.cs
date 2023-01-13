using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Threading.Tasks;

using System.Collections.Generic;
using UnityEngine;

using TakechiEngine.PUN.Information;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.ServerConnect.TraingGroundServer
{
    public class TraingGroundServerController : TakechiPunInformationDisclosure
    {
        [Header("DefaultRoomSettings")]
        [SerializeField] private int  m_selectedCharacterIndex = 0;
        [SerializeField] private int  maxPlayers = 1;
        [SerializeField] private bool isVisible = true;
        [SerializeField] private bool isOpen = true;

        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private Quaternion m_quaternion = Quaternion.identity;

        [SerializeField] private List<string> m_folderName = new List<string>();
        [SerializeField] private List<GameObject> m_networkInstancePrefab = new List<GameObject>();

        private void Start()
        {
            if (PhotonNetwork.InLobby)
            {
                CreateAndJoinRoom();
                return;
            }

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.GameVersion = "0.0f";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void CreateAndJoinRoom()
        {
            var roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = (byte)maxPlayers;

            roomOptions.IsOpen = isOpen;
            roomOptions.IsVisible = isVisible;

            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { RoomStatusKey.gameTypeKey,  RoomStatusName.hardpoint},
                { RoomStatusKey.mapKey, "255"},
                { RoomTeamStatusKey.HardPointStatusKey.teamAPoint, 0 },
                { RoomTeamStatusKey.HardPointStatusKey.teamBPoint, 0 },
                { RoomStatusKey.victoryPointKey, 0 },
            };

            if (PhotonNetwork.InLobby)
            {
                roomOptions.CustomRoomProperties = customRoomProperties;
                PhotonNetwork.JoinOrCreateRoom("TraingRoom", roomOptions, TypedLobby.Default);
            }
        }

        #region Pun Callbacks

        public override void OnConnected()
        {
            SetMyNickName("Player");
            setLocalPlayerCustomProperties(CharacterStatusKey.teamKey, "NullTeam");

            Debug.Log("OnConnected");
        }

        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.IsConnected) { PhotonNetwork.JoinLobby(); }

            Debug.Log("OnConnectedToMaster");
        }

        public override void OnJoinedLobby()
        {
            CreateAndJoinRoom();

            Debug.Log("OnJoinedLobby");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");

            RoomInfoAndJoinedPlayerInfoDisplay( RoomStatusKey.allKeys ,CharacterStatusKey.allKeys);

            InstantiationSetting();
        }

        private async void InstantiationSetting()
        {
            await Task.Delay(NetworkSyncSettings.connectionSynchronizationTime);

            string path = "";

            foreach (string s in m_folderName)
            {
                path += s + "/";
            }

            PhotonNetwork.Instantiate( path + m_networkInstancePrefab[ m_selectedCharacterIndex].name, this.transform.position, m_quaternion);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log("OnDisconnected");
        }

        #endregion
    }
}