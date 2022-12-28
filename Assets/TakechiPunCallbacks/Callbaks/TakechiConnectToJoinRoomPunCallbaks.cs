using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using TakechiEngine.PUN.Information;

namespace TakechiEngine.PUN.ServerConnect.ToJoinRoom
{
    public class TakechiConnectToJoinRoomPunCallbaks : TakechiPunInformationDisclosure
    {
        /// <summary>
        /// 参考資料
        /// </summary>
        /// 
        /// 用語集
        /// https://doc.photonengine.com/ja-jp/pun/current/reference/glossary
        ////////////////////////////////////////////////////////////////////
        
        #region RoomCreate

        /// <summary>
        /// 部屋を作成して入室する ※カスタムプロパティの設定可能
        /// </summary>
        /// <remarks> 部屋の作成　主は、そのままその部屋の入室します。</remarks>>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="customRoomProperties"> プロパティーの保管されている　ExitGames.Client.Photon.Hashtable </param>
        protected void CreateRoom(string roomName, RoomOptions roomOptions, ExitGames.Client.Photon.Hashtable customRoomProperties)
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
        /// <remarks> 部屋の作成　主は、そのままその部屋の入室します。</remarks>>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        protected void CreateRoom( string roomName, RoomOptions roomOptions)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }

        #endregion

        #region RoomJoin

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

        #region RoomCreateOrJoin

        /// <summary>
        /// 存在しなければ作成して入室する 
        /// </summary>
        /// <remarks> 
        /// ※カスタムプロパティの設定可能
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
        /// 存在しなければ作成して入室する
        /// </summary>
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

        #region active caollbaks

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
        /// 部屋を作成した時
        /// </summary>
        public override void OnCreatedRoom()
        {
            Debug.Log(" OnCreatedRoom :<color=green> clear</color>");
        }

        /// <summary>
        /// 部屋から退室した時
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.Log(" OnLeftRoom :<color=green> clear</color>");
        }

        #endregion

        #region error callbaks

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
            Debug.LogError(InformationTitleTemplate(" OnJoinRandomFailed ") +
                $" Massage : <color=red>{message}</color>  CodeNumber : <color=red>{returnCode}</color>");
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

        #endregion
    }
}
