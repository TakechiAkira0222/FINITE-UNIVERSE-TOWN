using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using TakechiEngine.PUN.ServerConnect;
using UnityEngine;

namespace Takechi.ServerAccess
{
    public class ServerConnection : TakechiServerConnectPunCallbacks, ConnectTheServer
    {
        /// <summary>
        /// 部屋のカスタムプロパティー
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_customRoomProperties =
            new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// プレイヤーのカスタムプロパティー
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_customPlayerProperties =
            new ExitGames.Client.Photon.Hashtable();

        protected virtual void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = false;

            Debug.Log($" PhotonNetwork.AutomaticallySyncScene : <color=green>{ PhotonNetwork.AutomaticallySyncScene} </color>");

            Connect("");
        }

        public override void OnConnected()
        {
            base.OnConnected();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // Finction /////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// prefabの生成
        /// </summary>
        /// <remarks>デバック情況に応じてインスタンス化してください。 </remarks>>
        protected void GeneratingPrefabs()
        {
            Invoke( nameof( InstantiateWithSynchronizedTime), 0.1f);
        }

        /// <summary>
        /// Instantiateの同期時間の設定用にコールします。
        /// </summary>
        protected void InstantiateWithSynchronizedTime()
        {
               
        }
    }
}
