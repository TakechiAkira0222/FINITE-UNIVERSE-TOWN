using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TakechiEngine.PUN.ServerAccess;

namespace Takechi.ServerAccess
{
    public class ServerConnectionInLoby : TakechiServerAccessPunCallbacks, AccessTheServer
    {
        #region SerializeField

        #endregion

        #region CustomProperties
        /// <summary>
        /// �����̃J�X�^���v���p�e�B�[
        /// </summary>
        private ExitGames.Client.Photon.Hashtable m_customRoomProperties =
            new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// �v���C���[�̃J�X�^���v���p�e�B�[
        /// </summary>
        private ExitGames.Client.Photon.Hashtable m_customPlayerProperties =
           new ExitGames.Client.Photon.Hashtable();

        /// <summary>
        /// ���[�����X�g
        /// </summary>
        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();

        #endregion

        #region PublicAction
        /// <summary>
        /// Photon�ɐڑ��������̏����B
        /// </summary>
        public event Action OnConnectedAction = delegate { };
        /// <summary>
        /// �}�X�^�[�T�[�o�[�ɐڑ��������̏����B
        /// </summary>
        public event Action OnConnectedToMasterAction = delegate { };
        /// <summary>
        /// ���r�[�ɐڑ��������̏����B
        /// </summary>
        public event Action OnJoinedLobbyAction = delegate { };
        /// <summary>
        /// �����ɓ����������̏����B
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };
        /// <summary>
        /// �������쐬����Ƃ��̏����B
        /// </summary>
        public event Action < string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };
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
            
            OnRoomCreationAction += ( name , roomOption , customRoomProperties) => 
            {
                JoinOrCreateRoom( name, roomOption, customRoomProperties);
                Debug.Log("<color=green> OnJoinedLobbyAction </color>");
            };

            OnJoinedRoomAction += () => 
            {
                Debug.Log("<color=green> OnJoinedRoomAction </color>");
                SceneSyncChange(2);
            };

            Debug.Log("<color=yellow>ServerAccess.Awake </color> : " +
                "OnConnectedAction, OnConnectedToMasterAction, OnJoinedLobbyAction, OnRoomCreationAction, OnJoinedRoomAction : " +
                "<color=green>to set</color>");
        }

        private void Start()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log($" PhotonNetwork.AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene} </color>");

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
            base.OnPlayerEnteredRoom( newPlayer);
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
            base.OnMasterClientSwitched( newMasterClient);
        }
        #endregion

        #region EventSystemButton
        /// <summary>
        /// ���[���̐ݒ肪�I�������A�������쐬���ē�������B
        /// </summary>
        public void OnRoomCreation()
        {
            OnRoomCreationAction("aaa", new RoomOptions(), m_customRoomProperties);
        }

        /// <summary>
        /// �����_���ȕ����ɓ���B(�{�^��)
        /// </summary>
        public void OnJoinRandomRoom()
        {
            JoinRandomRoom();
            Debug.Log("OnJoinRandomRoom :<color=green> clear </color>");
        }

        /// <summary>
        /// ���O�Ŏw�肵������̕����ɓ���B(�{�^��)
        /// </summary>
        public void OnNameSearchJoinRoom(Text roomNameText)
        {
            JoinRoom( roomNameText.text);
            Debug.Log("OnNameSearchJoinRoom :<color=green> clear </color>");
        }

        /// <summary>
        /// ���g�̖��O���Q�Ƃ��ĕ����ɓ���B
        /// </summary>
        /// <remarks> �C���X�^���X�����ꂽ�{�^���̖��O���Q�Ƃ��Ă��̕����ɓ������܂��B </remarks>
        public void OnNameReferenceJoinRoom(Button button)
        {
            JoinRoom( button.gameObject.name);
            Debug.Log($"OnOwnNameReferenceJoinRoom :<color=green> clear </color>:<color=blue> RoomName : { button.gameObject.name} </color>");
        }
        /// <summary>
        /// ��������ޏo����B
        /// </summary>
        public void OnLeaveRoom()
        {
            LeaveRoom();
            Debug.Log(" Base.OnLeaveRoom :<color=green> clear </color>");
        }

        public void OnGameStart()
        {
           
        }

        #endregion

        #region Error
      
        /// <summary>
        /// �����_���ȕ����ւ̓����Ɏ��s������
        /// </summary>
        /// <remarks>�ڑ��Ɏ��s�����ꍇ�}�X�^�[�T�[�o�[�̌Ăяo���܂Ŗ߂�܂��B</remarks>>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed( returnCode, message);
        }

        /// <summary>
        /// ����̕����ւ̓����Ɏ��s������
        /// </summary>
        /// <remarks>�ڑ��Ɏ��s�����ꍇ�}�X�^�[�T�[�o�[�̌Ăяo���܂Ŗ߂�܂��B</remarks>>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
        }

        /// <summary>
        /// �T�[�o�[�Ƀ��[�����쐬�ł��Ȃ�������
        /// </summary>
        /// <remarks>
        /// The most common cause to fail creating a room, is when a title relies on fixed room-names and the room already exists.
        /// </remarks>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
        �@�@base.OnCreateRoomFailed(returnCode, message);
        }

        /// <summary>
        /// Photon����ؒf���ꂽ��
        /// </summary>
        /// <param name="cause">�@�ؒf���� </param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
        }
        #endregion

        #region PrivateFinction

        /// <summary>
        /// ���X�g�̒��ɂ���I�u�W�F�N�g�������ƃ��X�g�̏�����������B
        /// </summary>
        /// <typeparam name="T"> �������������X�g�̌^ </typeparam>
        /// <param name="values">�@���������� �I�u�W�F�N�g���X�g </param>
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
