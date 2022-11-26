using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;

using TakechiEngine.PUN.Information;

namespace TakechiEngine.PUN.ServerAccess
{
    /// <summary>
    /// サーバーアクセス時のインターフェース
    /// </summary>
    public interface AccessTheServer
    {
        void Connect(string v);
        void OnConnected();
        void OnConnectedToMaster();
        void OnJoinedLobby();
        void OnJoinedRoom();
    }

    public class TakechiServerAccessPunCallbacks : TakechiPunInformationDisclosure
    {
        /// <summary>
        /// 参考資料
        /// </summary>
        /// 
        /// 用語集
        /// https://doc.photonengine.com/ja-jp/pun/current/reference/glossary
        ////////////////////////////////////////////////////////////////////
        # region SettingsClass

        /// <summary>
        /// ルーム基本設定の定義
        /// </summary>
        /// <param name="maxPlayers"> 最大人数 </param>
        /// <param name="isVisible"> 公開 or 非公開 </param>
        /// <param name="isOpen"> 入出許可　</param>
        /// <returns></returns>
        protected RoomOptions SetRoomOptions(int maxPlayers, bool isVisible, bool isOpen)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                MaxPlayers = (byte)maxPlayers,
                IsVisible = isVisible,
                IsOpen = isOpen
            };

            return roomOptions;
        }
        #endregion

        #region Connect

        /// <summary>
        /// Photonに接続する
        /// </summary>
        /// <param name ="gameVersion"> バージョン </param>
        public void Connect(string gameVersion)
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                Debug.LogWarning("Connection will not be performed because it was in a connected state.");
            }
        }
        #endregion

        #region JoinLobby
        /// <summary>
        /// ロビーに入る
        /// </summary>
        protected void JoinLobby()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinLobby();
            }
        }

        #endregion 

        #region RoomCreateAndJoin
        /// <summary>
        /// 部屋を作成して入室する ※カスタムプロパティの設定可能
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="customRoomProperties"> プロパティーの保管されている　ExitGames.Client.Photon.Hashtable </param>
        protected void CreateAndJoinRoom(string roomName, RoomOptions roomOptions, ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            roomOptions.CustomRoomProperties = customRoomProperties;

            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }

        /// <summary>
        /// 部屋を作成して入室する ※カスタムプロパティの設定可能
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        protected void CreateAndJoinRoom(string roomName, RoomOptions roomOptions)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }

        #endregion

        #region RoomCreateOrJoin
        /// <summary>
        /// 特定の部屋に入室する ※カスタムプロパティの設定可能
        /// </summary>
        /// <remarks> 
        /// 存在しなければ作成して入室する
        /// </remarks>>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="customRoomProperties"> プロパティーの保管されている　ExitGames.Client.Photon.Hashtable </param>
        protected void JoinOrCreateRoom( string roomName, RoomOptions roomOptions ,ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            roomOptions.CustomRoomProperties = customRoomProperties;

            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinOrCreateRoom( roomName, roomOptions, TypedLobby.Default);
            }
        }
        /// <summary>
        /// 特定の部屋に入室する
        /// </summary>
        /// <remarks> 
        /// 存在しなければ作成して入室する
        /// </remarks>>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        protected void JoinOrCreateRoom( string roomName, RoomOptions roomOptions)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinOrCreateRoom( roomName, roomOptions, TypedLobby.Default);
            }
        }

        #endregion

        #region RoomJoinOnly
        /// <summary>
        /// 特定の部屋に入室する
        /// </summary>
        /// <param name ="targetRoomName"> 部屋の名前 </param>
        protected void JoinRoom(string targetRoomName)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinRoom(targetRoomName);
            }
        }
        /// <summary>
        /// ランダムな部屋に入室する
        /// </summary>
        protected void JoinRandomRoom()
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        #endregion

        #region LeaveRoom
 
        /// <summary>
        /// 部屋から退室する
        /// </summary>
        protected void LeaveRoom()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log(" OnLeaveRoom :<color=green> clear </color>",this.gameObject);
            }
        }
        #endregion

        # region SyncChange

        /// <summary>
        /// マスタークライアントと同じシーンに移動させる。
        /// </summary>
        /// <remarks> 
        /// この関数を使用するときには、PhotonNetwork.AutomaticallySyncScene = true にしてください。
        /// </remarks>
        /// <param name ="sceneNumber"> 移行したいシーンの番号 </param>
        protected void SceneSyncChange(int sceneNumber)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(sceneNumber);

                Debug.Log($"<color=green> SceneSyncChange SceneNumber</color> : {sceneNumber}");
            }
        }

        #endregion

        #region PunCallbacks
        /// <summary>
        /// Photonに接続した時
        /// </summary>
        public override void OnConnected()
        {
            Debug.Log(" OnConnected :<color=green> clear</color>");
        }
        /// <summary>
        /// Photonから切断された時
        /// </summary>
        /// <param name="cause">　切断原因 </param>
        public override void OnDisconnected( DisconnectCause cause)
        {
            Debug.Log($" OnDisconnected Cause :<color=green> {cause}</color>");
        }
        /// <summary>
        /// マスターサーバーに接続した時
        /// </summary>
        public override void OnConnectedToMaster()
        {
            Debug.Log(" OnConnectedToMaster :<color=green> clear</color>");
        }
        /// <summary>
        /// ロビーに入った時
        /// </summary>
        public override void OnJoinedLobby()
        {
            Debug.Log(" OnJoinedLobby :<color=green> clear</color>");
        }
        /// <summary>
        /// ロビーから出た時
        /// </summary>
        public override void OnLeftLobby()
        {
            Debug.Log(" OnLeftLobby :<color=green> clear</color>");
        }
        /// <summary>
        /// 部屋を作成した時
        /// </summary>
        public override void OnCreatedRoom()
        {
            Debug.Log(" OnCreatedRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// サーバーにルームを作成できなかった場合 
        /// </summary>
        /// <remarks>
        /// The most common cause to fail creating a room, is when a title relies on fixed room-names and the room already exists.
        /// </remarks>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError(InformationTitleTemplate(" OnCreateRoomFailed ") +
               $" Massage : <color=red>{message}</color> CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// 部屋に入室した時
        /// </summary>
        /// <remarks>
        /// ルームに入室したときに、このクライアントがルームを作成したか、単に参加したかに関係なく呼び出されます。
        /// </remarks> 
        public override void OnJoinedRoom()
        {
            Debug.Log(" OnJoinedRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// 特定の部屋への入室に失敗した時
        /// </summary>
        /// <remarks>接続に失敗した場合マスターサーバーの呼び出しまで戻ります。</remarks>>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError(InformationTitleTemplate(" OnJoinRoomFailed ") +
                $" Massage : <color=red>{message}</color> CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// ランダムな部屋への入室に失敗した時
        /// </summary>
        /// <remarks>接続に失敗した場合マスターサーバーの呼び出しまで戻ります。</remarks>>
        /// <param name="returnCode"> サーバーからの操作戻りコード </param>
        /// <param name="message"> エラーメッセージ </param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError( InformationTitleTemplate(" OnJoinRandomFailed ") +
                $" Massage : <color=red>{message}</color>  CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// 部屋から退室した時
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.Log(" OnLeftRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// 他のプレイヤーが入室してきた時
        /// </summary>
        /// <param name="newPlayer">
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            PlayerInformationDisplay( newPlayer, "OnPlayerEnteredRoom", "newPlayer");
        }
        /// <summary>
        /// 他のプレイヤーが退室した時
        /// </summary>
        /// <param name="otherPlayer">
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerInformationDisplay( otherPlayer, "OnPlayerLeftRoom", "otherPlayerr");
        }
        /// <summary>
        /// マスタークライアントが変わった時
        /// </summary>
        /// <remarks>
        /// このクライアントがルームに入るとき、これは呼び出されません。 
        /// このメソッドが呼び出されたとき、以前のマスター クライアントはプレイヤー リストに残っています。
        /// </remarks>
        /// <param name="newMasterClient"></param>
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            PlayerInformationDisplay( newMasterClient, "OnMasterClientSwitched", "newMasterClient");
        }
        /// <summary>
        /// ロビーに更新があった時
        /// </summary>
        /// <param name="lobbyStatistics">
        /// ロビーの情報
        /// 
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_typed_lobby_info.html
        /// </param>
        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log("OnLobbyStatisticsUpdate");
        }
        /// <summary>
        /// ルームに入った時もしくは、リストに更新があった時
        /// </summary>
        /// <param name="roomList">
        /// 
        /// ルームの情報
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_room_info.html
        /// </param>
        public override void OnRoomListUpdate( List<RoomInfo> roomList)
        {
            Debug.Log(" OnRoomListUpdate :<color=green> clear </color>");

            foreach( RoomInfo roomInfo in roomList)
            {
                Debug.Log( InformationTitleTemplate("OnRoomListUpdate") + WhatRoomInformationIsDisplayedInTheDebugLog(roomInfo), this);
            }
        }
        /// <summary>
        /// ルームプロパティが更新された時
        /// </summary>
        /// <param name="propertiesThatChanged">
        ///　ルームプロパティ
        /// </param>
        /// <remarks>　プロパティーに更新が入った時、更新されたKeyの変更内容を返します。</remarks>>
        public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
            foreach (var prop in propertiesThatChanged)
            {
                Debug.Log($" Room property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>", this.gameObject);
        }

        /// <summary>
        /// プレイヤープロパティが更新された時
        /// </summary>
        /// <param name="target">
        /// 
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        /// <param name="changedProps">
        /// ルームプロパティ
        /// </param>
        public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {
            // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
            foreach (var prop in changedProps)
            {
                Debug.Log($" Player property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>");
        }
        /// <summary>
        /// 地域リストを受け取った時
        /// </summary>
        /// <param name="regionHandler">
        /// 環境
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_region_handler.html"/>
        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("OnRegionListReceived");
        }
        /// <summary>
        /// カスタム認証のレスポンスがあった時
        /// </summary>
        /// <param name="data">
        /// 
        /// </param>
        public override void OnCustomAuthenticationResponse( Dictionary<string, object> data)
        {
            Debug.Log("OnCustomAuthenticationResponse");
        }
        /// <summary>
        /// カスタム認証が失敗した時
        /// </summary>
        /// <param name="debugMessage">
        /// 認証が失敗した理由のデバッグ メッセージが含まれています。これは、開発中に修正する必要があります。
        /// </param>
        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("OnCustomAuthenticationFailed");
            Debug.Log(debugMessage);
        }
        /// <summary>
        /// フレンドリストに更新があった時
        /// </summary>
        /// <param name="friendList">
        /// フレンドのオンライン状況と、どのRoomにいるのかという情報
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/current/class_friend_info.html"/>
        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("OnFriendListUpdate");
        }
        /// <summary>
        /// フレンドのリストの部屋とオンライン ステータスをリクエストし、結果を PhotonNetwork.Friends に保存します。
        /// </summary>
        /// <param name="friendsUserIds">
        /// フレンドID
        /// </param>
        /// <returns></returns>
        public bool FindFriends(string[] friendsUserIds)
        {
            return PhotonNetwork.FindFriends(friendsUserIds);
        }
        /// <summary>
        /// フレンドリストの更新
        /// </summary>
        /// <param name="friendsInfo">
        /// フレンドのオンライン状況と、どのRoomにいるのかという情報
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/current/class_friend_info.html"/>
        protected void OnUpdatedFriendList(List<FriendInfo> friendsInfo)
        {
            for (int i = 0; i < friendsInfo.Count; i++)
            {
                FriendInfo friend = friendsInfo[i];
                Debug.LogFormat("{0}", friend);
            }
        }
        #endregion
    }
}