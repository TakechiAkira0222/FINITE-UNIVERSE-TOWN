using System;
using System.Collections;
using System.Collections.Generic;

using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TakechiEngine.PUN.ServerAccess;
using UnityEngine.SceneManagement;

namespace Takechi.ServerAccess.ConnectToJoinRoom
{
    public class ConnectToJoinRoomManagment : TakechiServerAccessPunCallbacks
    {

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

        /// <summary>
        /// �����ɓ����������̏����B
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };
        /// <summary>
        /// �������쐬����Ƃ��̏����B
        /// </summary>
        public event Action<string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };

        void Awake()
        {
            OnRoomCreationAction += (name, roomOption, customRoomProperties) =>
            {
                JoinOrCreateRoom(name, roomOption);
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

        #region EventSystemButton

        /// <summary>
        /// ���[���̐ݒ肪�I�������A�������쐬���ē�������B
        /// </summary>
        public void OnRoomCreation()
        {
            var roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = 2;
            roomOptions.IsOpen = true; 
            roomOptions.IsVisible = true; 

            OnRoomCreationAction("aaa", roomOptions, m_customRoomProperties);
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
            JoinRoom(roomNameText.text);
            Debug.Log("OnNameSearchJoinRoom :<color=green> clear </color>");
        }

        /// <summary>
        /// ���g�̖��O���Q�Ƃ��ĕ����ɓ���B
        /// </summary>
        /// <remarks> �C���X�^���X�����ꂽ�{�^���̖��O���Q�Ƃ��Ă��̕����ɓ������܂��B </remarks>
        public void OnNameReferenceJoinRoom(Button button)
        {
            JoinRoom(button.gameObject.name);
            Debug.Log($"OnOwnNameReferenceJoinRoom :<color=green> clear </color>:<color=blue> RoomName : {button.gameObject.name} </color>");
        }

        /// <summary>
        /// ��������ޏo����B
        /// </summary>
        public void OnLeaveRoom()
        {
            LeaveRoom();
            Debug.Log(" Base.OnLeaveRoom :<color=green> clear </color>");
        }

        #endregion

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

        #region Error

        /// <summary>
        /// �����_���ȕ����ւ̓����Ɏ��s������
        /// </summary>
        /// <remarks>�ڑ��Ɏ��s�����ꍇ�}�X�^�[�T�[�o�[�̌Ăяo���܂Ŗ߂�܂��B</remarks>>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
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
            base.OnCreateRoomFailed(returnCode, message);
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
