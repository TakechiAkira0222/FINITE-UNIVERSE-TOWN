using System;
using System.Collections;
using System.Collections.Generic;

using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TakechiEngine.PUN.ServerConnect;
using UnityEngine.SceneManagement;


using Takechi.UI.GameTypeSelection;
using Takechi.UI.RoomPropertySetting;
using Takechi.UI.MapSelection;
using Takechi.ScriptReference.CustomPropertyKey;

namespace Takechi.ServerConnect.ConnectToJoinRoom
{
    public class ConnectToJoinRoomManagment : TakechiServerConnectPunCallbacks
    {
        [SerializeField] private GameTypeSelectionManagement   m_gameTypeSelection;
        [SerializeField] private RoomPropertySettingManagement m_roomPropertySetting;
        [SerializeField] private MapSelectionManagement        m_mapSelection;

        #region CustomProperties
       

        /// <summary>
        /// ルームリスト
        /// </summary>
        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();

        #endregion

        #region Event Action

        /// <summary>
        /// 部屋に入室した時の処理。
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };
        /// <summary>
        /// 部屋を作成するときの処理。
        /// </summary>
        public event Action<string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };

        #endregion

        #region Unity Event

        void Awake()
        {
            OnRoomCreationAction += (name, roomOption, customRoomProperties) =>
            {
                CreateRoom(name, roomOption);
                Debug.Log("<color=green> OnRoomCreationAction </color>");
            };

            OnJoinedRoomAction += () =>
            {
                SceneManager.LoadScene(3);
                Debug.Log("<color=green> OnJoinedRoomAction </color>");
            };

            Debug.Log("<color=yellow>ServerAccess.Awake </color> : " +
              " OnRoomCreationAction, OnJoinedRoomAction : <color=green>to set</color>");
        }

        #endregion

        #region EventSystemButton

        /// <summary>
        /// ルームの設定が終わった後、部屋を作成して入室する。
        /// </summary>
        public void OnRoomCreation()
        {
            var roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = (byte)((m_gameTypeSelection.GetGameTypeSelectionIndex() + 1) * 2);

            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;

            /// <summary>
            /// 部屋のカスタムプロパティー
            /// </summary>
            var customRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { CustomPropertyKeyReference.s_RoomStatusGameType, m_gameTypeSelection.GetGameTypeSelectionIndex()},
                { CustomPropertyKeyReference.s_RoomSatusMap, m_mapSelection.GetMapSelectionIndex() },
            };

            OnRoomCreationAction( CheckForEnteredCharacters(m_roomPropertySetting.GetRoomName()), roomOptions, customRoomProperties); ;
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
            Debug.Log($"OnOwnNameReferenceJoinRoom :<color=green> clear </color>:<color=blue> RoomName : {button.gameObject.name} </color>");
        }

        /// <summary>
        /// 部屋から退出する。
        /// </summary>
        public void OnLeaveRoom()
        {
            LeaveRoom();
            Debug.Log(" Base.OnLeaveRoom :<color=green> clear </color>");
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

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);
        }

        #endregion

        #region private finction

        /// <summary>
        /// リストの中にあるオブジェクトを消去とリストの初期化をする。
        /// </summary>
        /// <typeparam name="T"> 消去したいリストの型 </typeparam>
        /// <param name="values">　消去したい オブジェクトリスト </param>
        private void ErasingWithinAnElementOfList<T>(List<T> values) where T : MonoBehaviour
        {
            foreach (var v in values)
            {
                Destroy(v.gameObject);
            }

            values.Clear();
        }
        #endregion
    }
}
