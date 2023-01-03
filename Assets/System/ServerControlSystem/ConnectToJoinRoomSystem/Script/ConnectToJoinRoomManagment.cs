using System;
using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

using Takechi.UI.GameTypeSelection;
using Takechi.UI.RoomPropertySetting;
using Takechi.UI.MapSelection;
using Takechi.UI.RoomJoinedMenu;
using Takechi.UI.UserNickname;
using TakechiEngine.PUN.ServerConnect.ToJoinRoom;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.ServerConnect.ToJoinRoom 
{
    public class ConnectToJoinRoomManagment : TakechiConnectToJoinRoomPunCallbaks
    {
        [SerializeField] private GameTypeSelectionManagement   m_gameTypeSelection;
        [SerializeField] private RoomPropertySettingManagement m_roomPropertySetting;
        [SerializeField] private MapSelectionManagement        m_mapSelection;
        [SerializeField] private RoomJoinedMenuManagement      m_roomJoinedMune;
        [SerializeField] private UserNickNameManagement        m_userNickNameManagement;

        #region Event Action

        /// <summary>
        /// 部屋を作成するときの処理。
        /// </summary>
        public event Action< string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };

        /// <summary>
        /// 部屋のリスト更新された時の処理
        /// </summary>
        public event Action<List<RoomInfo>>  OnRoomListUpdateAction = delegate { };

        /// <summary>
        /// 部屋に入室した時の処理。
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };

        #endregion

        /// <summary>
        /// 存在している部屋 リスト
        /// </summary>
        private List<RoomInfo> m_roomInfoList = 
            new List<RoomInfo>();

        /// <summary>
        /// 存在している部屋 辞書
        /// </summary>
        private Dictionary<string, RoomInfo> m_roomInfoDictionary =
            new Dictionary<string, RoomInfo>();

        #region Unity Event

        void Awake()
        {
            OnRoomCreationAction += ( name, roomOption, customRoomProperties) =>
            {
                CreateRoom( name, roomOption, customRoomProperties);
                Debug.Log("<color=green> OnRoomCreationAction </color>");
            };

            OnRoomListUpdateAction += ( changedRoomList) =>
            {
                UpdateRoomInfo( changedRoomList);
                Debug.Log("<color=green> OnJoinedRoomAction </color>");
            };

            OnJoinedRoomAction += () =>
            {
                SetMyNickName(ConfirmationOfNicknames(m_userNickNameManagement.GetNickNameData().nickName));

                RoomInfoAndJoinedPlayerInfoDisplay(RoomStatusKey.allKeys, CharacterStatusKey.allKeys);

                m_roomJoinedMune.gameObject.SetActive(true);

                Debug.Log("<color=green> OnJoinedRoomAction </color>");
            };

            Debug.Log("<color=yellow>ServerAccess.Awake </color> : " +
              " OnRoomCreationAction, OnJoinedRoomAction, OnRoomListUpdateAction : <color=green>to set</color>");
        }

        #endregion

        #region EventSystemButton

        /// <summary>
        /// ルームの設定が終わった後、部屋を作成して入室する。
        /// </summary>
        public void OnRoomCreation()
        {
            var roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = (byte)(( m_roomPropertySetting.GetParticipantsIndex() + 1) * 2);

              roomOptions.IsOpen  = true;
            roomOptions.IsVisible = true;

            var customRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { RoomStatusKey.gameTypeKey, m_gameTypeSelection.GetGameTypeSelectionIndex()},
                { RoomStatusKey.mapKey, m_mapSelection.GetMapSelectionIndex()},
            };

            OnRoomCreationAction( CheckForEnteredCharacters( m_roomPropertySetting.GetRoomName()), roomOptions, customRoomProperties);
        }

        /// <summary>
        /// ランダムな部屋に入る。(ボタン)
        /// </summary>
        public void OnJoinRandomRoom()
        {
            JoinRandomRoom();
            Debug.Log("OnJoinRandomRoom :<color=green> clear </color>");
        }

        /// <summary>
        /// 名前で指定した特定の部屋に入る。(ボタン)
        /// </summary>
        public void OnNameSearchJoinRoom(Text roomNameText)
        {
            JoinRoom( roomNameText.text);
            Debug.Log("OnNameSearchJoinRoom :<color=green> clear </color>");
        }

        /// <summary>
        /// 自身の名前を参照して部屋に入る。
        /// </summary>
        /// <remarks> インスタンス化されたボタンの名前を参照してその部屋に入室します。 </remarks>
        public void OnNameReferenceJoinRoom(Button button)
        {
            JoinRoom(button.gameObject.name);
            Debug.Log($" OnNameReferenceJoinRoom :<color=green> clear </color>:<color=blue> RoomName : {button.gameObject.name} </color>");
        }

        #endregion

        #region MonoBehaviourPunCallbacks

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            OnJoinedRoomAction();
        }

        public override void OnRoomListUpdate( List<RoomInfo> changedRoomList)
        {
            base.OnRoomListUpdate(changedRoomList);
            OnRoomListUpdateAction(changedRoomList);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);

            PhotonNetwork.JoinLobby();
        }

        #endregion

        public List<RoomInfo> GetRoomInfoList() { return m_roomInfoList; }

        public Dictionary< string, RoomInfo> GetRoomInfoDictionary() { return m_roomInfoDictionary; }

        private void UpdateRoomInfo( List<RoomInfo> changedRoomList)
        {
            foreach ( RoomInfo info in changedRoomList)
            {
                if ( !m_roomInfoList.Contains(info))
                {
                    m_roomInfoList.Add(info);
                    m_roomInfoDictionary.Add(info.Name, info);
                    Debug.Log($"{info} : add");
                }
                
                if( m_roomInfoList.Contains(info))
                {
                    if ( info.PlayerCount <= 0)
                    {
                        m_roomInfoList.Remove(info);
                        m_roomInfoDictionary.Remove(info.Name);
                        Debug.Log($"{info} : remove");
                    }
                }
            }
        }
    }
}
