using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TakechiEngine.PUN.ServerConnect.ToLobby;
using Takechi.ScriptReference.CustomPropertyKey;

namespace Takechi.ServerConnect.ToLobby
{
    public class ConnectToLobbyManagment : TakechiConnectToLobbyPunCallbacks
    {
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
       
        #endregion

        #region UintyEvent
        private void Awake()
        {
            OnConnectedAction += () => { Debug.Log("<color=green> OnConnectedAction </color>");};

            OnConnectedToMasterAction += () => 
            {
                JoinLobby();
                Debug.Log("<color=green> OnConnectedToMasterAction </color>");
            };

            OnJoinedLobbyAction += () => 
            {
                Debug.Log("<color=green> OnJoinedLobbyAction </color>"); 
            };
            
            Debug.Log("<color=yellow>ServerAccess.Awake </color> : " +
                "OnConnectedAction, OnConnectedToMasterAction, OnJoinedLobbyAction, OnRoomCreationAction, OnJoinedRoomAction : " +
                "<color=green>to set</color>");
        }

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log($" PhotonNetwork.AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene} </color>");

            Connect("prototype");
        }

        #endregion

        #region MonoBehaviourPunCallbacks

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

            OnJoinedLobbyAction();
        }

        #endregion
    }
}
