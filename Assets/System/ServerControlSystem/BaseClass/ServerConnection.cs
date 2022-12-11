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
        /// �����̃J�X�^���v���p�e�B�[
        /// </summary>
        protected ExitGames.Client.Photon.Hashtable m_customRoomProperties =
            new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// �v���C���[�̃J�X�^���v���p�e�B�[
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
        /// prefab�̐���
        /// </summary>
        /// <remarks>�f�o�b�N��ɉ����ăC���X�^���X�����Ă��������B </remarks>>
        protected void GeneratingPrefabs()
        {
            Invoke( nameof( InstantiateWithSynchronizedTime), 0.1f);
        }

        /// <summary>
        /// Instantiate�̓������Ԃ̐ݒ�p�ɃR�[�����܂��B
        /// </summary>
        protected void InstantiateWithSynchronizedTime()
        {
               
        }
    }
}
