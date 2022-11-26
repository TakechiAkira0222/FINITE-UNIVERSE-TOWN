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
    /// �T�[�o�[�A�N�Z�X���̃C���^�[�t�F�[�X
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
        /// �Q�l����
        /// </summary>
        /// 
        /// �p��W
        /// https://doc.photonengine.com/ja-jp/pun/current/reference/glossary
        ////////////////////////////////////////////////////////////////////
        # region SettingsClass

        /// <summary>
        /// ���[����{�ݒ�̒�`
        /// </summary>
        /// <param name="maxPlayers"> �ő�l�� </param>
        /// <param name="isVisible"> ���J or ����J </param>
        /// <param name="isOpen"> ���o���@</param>
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
        /// Photon�ɐڑ�����
        /// </summary>
        /// <param name ="gameVersion"> �o�[�W���� </param>
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
        /// ���r�[�ɓ���
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
        /// �������쐬���ē������� ���J�X�^���v���p�e�B�̐ݒ�\
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="customRoomProperties"> �v���p�e�B�[�̕ۊǂ���Ă���@ExitGames.Client.Photon.Hashtable </param>
        protected void CreateAndJoinRoom(string roomName, RoomOptions roomOptions, ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            roomOptions.CustomRoomProperties = customRoomProperties;

            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.CreateRoom(roomName, roomOptions);
            }
        }

        /// <summary>
        /// �������쐬���ē������� ���J�X�^���v���p�e�B�̐ݒ�\
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
        /// ����̕����ɓ������� ���J�X�^���v���p�e�B�̐ݒ�\
        /// </summary>
        /// <remarks> 
        /// ���݂��Ȃ���΍쐬���ē�������
        /// </remarks>>
        /// <param name="roomName"></param>
        /// <param name="roomOptions"></param>
        /// <param name="customRoomProperties"> �v���p�e�B�[�̕ۊǂ���Ă���@ExitGames.Client.Photon.Hashtable </param>
        protected void JoinOrCreateRoom( string roomName, RoomOptions roomOptions ,ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            roomOptions.CustomRoomProperties = customRoomProperties;

            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinOrCreateRoom( roomName, roomOptions, TypedLobby.Default);
            }
        }
        /// <summary>
        /// ����̕����ɓ�������
        /// </summary>
        /// <remarks> 
        /// ���݂��Ȃ���΍쐬���ē�������
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
        /// ����̕����ɓ�������
        /// </summary>
        /// <param name ="targetRoomName"> �����̖��O </param>
        protected void JoinRoom(string targetRoomName)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinRoom(targetRoomName);
            }
        }
        /// <summary>
        /// �����_���ȕ����ɓ�������
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
        /// ��������ގ�����
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
        /// �}�X�^�[�N���C�A���g�Ɠ����V�[���Ɉړ�������B
        /// </summary>
        /// <remarks> 
        /// ���̊֐����g�p����Ƃ��ɂ́APhotonNetwork.AutomaticallySyncScene = true �ɂ��Ă��������B
        /// </remarks>
        /// <param name ="sceneNumber"> �ڍs�������V�[���̔ԍ� </param>
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
        /// Photon�ɐڑ�������
        /// </summary>
        public override void OnConnected()
        {
            Debug.Log(" OnConnected :<color=green> clear</color>");
        }
        /// <summary>
        /// Photon����ؒf���ꂽ��
        /// </summary>
        /// <param name="cause">�@�ؒf���� </param>
        public override void OnDisconnected( DisconnectCause cause)
        {
            Debug.Log($" OnDisconnected Cause :<color=green> {cause}</color>");
        }
        /// <summary>
        /// �}�X�^�[�T�[�o�[�ɐڑ�������
        /// </summary>
        public override void OnConnectedToMaster()
        {
            Debug.Log(" OnConnectedToMaster :<color=green> clear</color>");
        }
        /// <summary>
        /// ���r�[�ɓ�������
        /// </summary>
        public override void OnJoinedLobby()
        {
            Debug.Log(" OnJoinedLobby :<color=green> clear</color>");
        }
        /// <summary>
        /// ���r�[����o����
        /// </summary>
        public override void OnLeftLobby()
        {
            Debug.Log(" OnLeftLobby :<color=green> clear</color>");
        }
        /// <summary>
        /// �������쐬������
        /// </summary>
        public override void OnCreatedRoom()
        {
            Debug.Log(" OnCreatedRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// �T�[�o�[�Ƀ��[�����쐬�ł��Ȃ������ꍇ 
        /// </summary>
        /// <remarks>
        /// The most common cause to fail creating a room, is when a title relies on fixed room-names and the room already exists.
        /// </remarks>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError(InformationTitleTemplate(" OnCreateRoomFailed ") +
               $" Massage : <color=red>{message}</color> CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// �����ɓ���������
        /// </summary>
        /// <remarks>
        /// ���[���ɓ��������Ƃ��ɁA���̃N���C�A���g�����[�����쐬�������A�P�ɎQ���������Ɋ֌W�Ȃ��Ăяo����܂��B
        /// </remarks> 
        public override void OnJoinedRoom()
        {
            Debug.Log(" OnJoinedRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// ����̕����ւ̓����Ɏ��s������
        /// </summary>
        /// <remarks>�ڑ��Ɏ��s�����ꍇ�}�X�^�[�T�[�o�[�̌Ăяo���܂Ŗ߂�܂��B</remarks>>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.LogError(InformationTitleTemplate(" OnJoinRoomFailed ") +
                $" Massage : <color=red>{message}</color> CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// �����_���ȕ����ւ̓����Ɏ��s������
        /// </summary>
        /// <remarks>�ڑ��Ɏ��s�����ꍇ�}�X�^�[�T�[�o�[�̌Ăяo���܂Ŗ߂�܂��B</remarks>>
        /// <param name="returnCode"> �T�[�o�[����̑���߂�R�[�h </param>
        /// <param name="message"> �G���[���b�Z�[�W </param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.LogError( InformationTitleTemplate(" OnJoinRandomFailed ") +
                $" Massage : <color=red>{message}</color>  CodeNumber : <color=red>{returnCode}</color>");
        }
        /// <summary>
        /// ��������ގ�������
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.Log(" OnLeftRoom :<color=green> clear</color>");
        }
        /// <summary>
        /// ���̃v���C���[���������Ă�����
        /// </summary>
        /// <param name="newPlayer">
        /// �v���C���[�̃f�[�^
        /// �Q�l
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            PlayerInformationDisplay( newPlayer, "OnPlayerEnteredRoom", "newPlayer");
        }
        /// <summary>
        /// ���̃v���C���[���ގ�������
        /// </summary>
        /// <param name="otherPlayer">
        /// �v���C���[�̃f�[�^
        /// �Q�l
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerInformationDisplay( otherPlayer, "OnPlayerLeftRoom", "otherPlayerr");
        }
        /// <summary>
        /// �}�X�^�[�N���C�A���g���ς������
        /// </summary>
        /// <remarks>
        /// ���̃N���C�A���g�����[���ɓ���Ƃ��A����͌Ăяo����܂���B 
        /// ���̃��\�b�h���Ăяo���ꂽ�Ƃ��A�ȑO�̃}�X�^�[ �N���C�A���g�̓v���C���[ ���X�g�Ɏc���Ă��܂��B
        /// </remarks>
        /// <param name="newMasterClient"></param>
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            PlayerInformationDisplay( newMasterClient, "OnMasterClientSwitched", "newMasterClient");
        }
        /// <summary>
        /// ���r�[�ɍX�V����������
        /// </summary>
        /// <param name="lobbyStatistics">
        /// ���r�[�̏��
        /// 
        /// �Q�l
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_typed_lobby_info.html
        /// </param>
        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            Debug.Log("OnLobbyStatisticsUpdate");
        }
        /// <summary>
        /// ���[���ɓ��������������́A���X�g�ɍX�V����������
        /// </summary>
        /// <param name="roomList">
        /// 
        /// ���[���̏��
        /// �Q�l
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
        /// ���[���v���p�e�B���X�V���ꂽ��
        /// </summary>
        /// <param name="propertiesThatChanged">
        ///�@���[���v���p�e�B
        /// </param>
        /// <remarks>�@�v���p�e�B�[�ɍX�V�����������A�X�V���ꂽKey�̕ύX���e��Ԃ��܂��B</remarks>>
        public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            // �X�V���ꂽ�v���C���[�̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
            foreach (var prop in propertiesThatChanged)
            {
                Debug.Log($" Room property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>", this.gameObject);
        }

        /// <summary>
        /// �v���C���[�v���p�e�B���X�V���ꂽ��
        /// </summary>
        /// <param name="target">
        /// 
        /// �v���C���[�̃f�[�^
        /// �Q�l
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        /// <param name="changedProps">
        /// ���[���v���p�e�B
        /// </param>
        public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {
            // �X�V���ꂽ�v���C���[�̃J�X�^���v���p�e�B�̃y�A���R���\�[���ɏo�͂���
            foreach (var prop in changedProps)
            {
                Debug.Log($" Player property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>");
        }
        /// <summary>
        /// �n�惊�X�g���󂯎������
        /// </summary>
        /// <param name="regionHandler">
        /// ��
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_region_handler.html"/>
        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("OnRegionListReceived");
        }
        /// <summary>
        /// �J�X�^���F�؂̃��X�|���X����������
        /// </summary>
        /// <param name="data">
        /// 
        /// </param>
        public override void OnCustomAuthenticationResponse( Dictionary<string, object> data)
        {
            Debug.Log("OnCustomAuthenticationResponse");
        }
        /// <summary>
        /// �J�X�^���F�؂����s������
        /// </summary>
        /// <param name="debugMessage">
        /// �F�؂����s�������R�̃f�o�b�O ���b�Z�[�W���܂܂�Ă��܂��B����́A�J�����ɏC������K�v������܂��B
        /// </param>
        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("OnCustomAuthenticationFailed");
            Debug.Log(debugMessage);
        }
        /// <summary>
        /// �t�����h���X�g�ɍX�V����������
        /// </summary>
        /// <param name="friendList">
        /// �t�����h�̃I�����C���󋵂ƁA�ǂ�Room�ɂ���̂��Ƃ������
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/current/class_friend_info.html"/>
        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("OnFriendListUpdate");
        }
        /// <summary>
        /// �t�����h�̃��X�g�̕����ƃI�����C�� �X�e�[�^�X�����N�G�X�g���A���ʂ� PhotonNetwork.Friends �ɕۑ����܂��B
        /// </summary>
        /// <param name="friendsUserIds">
        /// �t�����hID
        /// </param>
        /// <returns></returns>
        public bool FindFriends(string[] friendsUserIds)
        {
            return PhotonNetwork.FindFriends(friendsUserIds);
        }
        /// <summary>
        /// �t�����h���X�g�̍X�V
        /// </summary>
        /// <param name="friendsInfo">
        /// �t�����h�̃I�����C���󋵂ƁA�ǂ�Room�ɂ���̂��Ƃ������
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