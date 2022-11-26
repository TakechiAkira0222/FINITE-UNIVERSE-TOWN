using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TakechiEngine.PUN.ServerAccess;
using Takechi.UI.Mune.Tracking;
using UnityEngine.SceneManagement;

namespace Takechi.ServerAccess
{
    public class RoomConnectionAndRoomCreationFromWithinTheList : TakechiServerAccessPunCallbacks, AccessTheServer
    {
        [Header(" Settings")]
        #region SerializeField
        [SerializeField] private Text m_informTheStatusText;

        #endregion

        #region CustomProperties
        /// <summary>
        /// 部屋のカスタムプロパティー
        /// </summary>
        private ExitGames.Client.Photon.Hashtable m_customRoomProperties =
            new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// プレイヤーのカスタムプロパティー
        /// </summary>
        private ExitGames.Client.Photon.Hashtable m_customPlayerProperties =
           new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// ルームリスト
        /// </summary>
        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();

        #endregion

        #region PublicAction
        /// <summary>
        /// Photonに接続した時の処理。
        /// </summary>
        public event Action OnConnectedAction = delegate { };
        /// <summary>
        /// マスターサーバーに接続した時の処理。
        /// </summary>
        public event Action OnConnectedToMasterAction = delegate { };
        /// <summary>
        /// ロビーに接続した時の処理。
        /// </summary>
        public event Action OnJoinedLobbyAction = delegate { };
        /// <summary>
        /// 部屋に入室した時の処理。
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };
        /// <summary>
        /// 部屋を作成するときの処理。
        /// </summary>
        public event Action < string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };
        #endregion

        #region UintyEvent
        private void Awake()
        {
            OnConnectedToMasterAction += JoinLobby;
            OnRoomCreationAction += JoinOrCreateRoom;
            Debug.Log("<color=yellow>ServerAccess.Awake</color> : Add JoinLobby, CreateAndJoinRoom");
        }

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log($" PhotonNetwork.AutomaticallySyncScene : <color=green>{ PhotonNetwork.AutomaticallySyncScene} </color>");

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

            Connect("");
        }
        #endregion

        #region MonoBehaviourPunCallbacks

        public override void OnConnected()
        {
            base.OnConnected();

            m_informTheStatusText.text = "OnConnected";

            OnConnectedAction();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            m_informTheStatusText.text = "OnConnectedToMaster";

            OnConnectedToMasterAction();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            m_informTheStatusText.text = "OnJoinedLobby";

            OnJoinedLobbyAction();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();

            m_informTheStatusText.text = "OnCreatedRoom";
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom( newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            m_informTheStatusText.text = 
                ($"{PhotonNetwork.CurrentRoom.Name} : {PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
           
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched( newMasterClient);
        }
        #endregion

        #region EventSystemButton
        /// <summary>
        /// ルームの設定が終わった後、部屋を作成して入室する。
        /// </summary>
        public void OnRoomCreation()
        {

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
        public void OnOwnNameReferenceJoinRoom(Button button)
        {
            JoinRoom( button.gameObject.name);
            Debug.Log($"OnOwnNameReferenceJoinRoom :<color=green> clear </color>:<color=blue> RoomName : { button.gameObject.name} </color>");
        }
        /// <summary>
        /// 部屋から退出する。
        /// </summary>
        public void OnLeaveRoom()
        {
            Debug.Log(" Base.OnLeaveRoom :<color=green> clear </color>");
            LeaveRoom();
        }

        #endregion

        #region Error
      
        /// <summary>
        /// ランダムな部屋への入室に失敗した時
        /// </summary>
        /// <remarks>接続に失敗した場合マスターサーバーの呼び出しまで戻ります。</remarks>>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);

            m_informTheStatusText.text = message;

            ReturnToFirstSelectionMenuDueToConnectionError();
        }

        /// <summary>
        /// 特定の部屋への入室に失敗した時
        /// </summary>
        /// <remarks>接続に失敗した場合マスターサーバーの呼び出しまで戻ります。</remarks>>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);

            m_informTheStatusText.text = message;

            ReturnToFirstSelectionMenuDueToConnectionError();
        }

        /// <summary>
        /// サーバーにルームを作成できなかった時
        /// </summary>
        /// <remarks>
        /// The most common cause to fail creating a room, is when a title relies on fixed room-names and the room already exists.
        /// </remarks>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
        　　base.OnCreateRoomFailed(returnCode, message);

            m_informTheStatusText.text = message;

            ReturnToFirstSelectionMenuDueToConnectionError();
        }

        /// <summary>
        /// Photonから切断された時
        /// </summary>
        /// <param name="cause">　切断原因 </param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            m_informTheStatusText.text = "接続情況が不安定なため切断しました。";

            base.OnDisconnected(cause);
        }
        #endregion

        #region PrivateFinction
        /// <summary>
        /// 接続エラー時、一定時間経過後に始めの選択メニューに戻ります。
        /// </summary>
        /// <param name="_waitTime"></param>
        /// <returns> Return To First Selection Menu Due To Connection Error </returns>
        private void ReturnToFirstSelectionMenuDueToConnectionError()
        {
            Debug.Log(" _joinRoomMenu.SetActive : <color=blue> false </color>", this.gameObject);
            Debug.Log(" _titileMenu.SetActive : <color=blue> true </color>", this.gameObject);

            OnConnectedToMasterAction();
            Debug.Log(" Return To First Selection Menu Due To Connection Error :<color=green> clear </color>");
        }

        /// <summary>
        /// リストの中にあるオブジェクトを消去とリストの初期化をする。
        /// </summary>
        /// <typeparam name="T"> 消去したいリストの型 </typeparam>
        /// <param name="_values">　消去したい オブジェクトリスト </param>
        private void ErasingWithinAnElementOfList<T>(List<T> _values) where T : MonoBehaviour
        {
            foreach (var _v in _values)
            {
                Destroy(_v.gameObject);
            }

            _values.Clear();
        }
        #endregion

    }
}
