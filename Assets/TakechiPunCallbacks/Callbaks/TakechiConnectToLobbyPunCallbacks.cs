using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TakechiEngine.PUN.Information;
using UnityEngine;

namespace TakechiEngine.PUN.ServerConnect.ToLobby
{
    public class TakechiConnectToLobbyPunCallbacks : TakechiPunInformationDisclosure
    {
        /// <summary>
        /// 参考資料
        /// </summary>
        /// 
        /// 用語集
        /// https://doc.photonengine.com/ja-jp/pun/current/reference/glossary
        ////////////////////////////////////////////////////////////////////
        
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

        #region active callbacks

        /// <summary>
        /// Photonに接続した時
        /// </summary>
        public override void OnConnected()
        {
            Debug.Log(" OnConnected :<color=green> clear</color>");
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
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log(" OnRoomListUpdate :<color=green> clear </color>");

            foreach (RoomInfo roomInfo in roomList)
            {
                
            }
        }

        #endregion
    }
}
