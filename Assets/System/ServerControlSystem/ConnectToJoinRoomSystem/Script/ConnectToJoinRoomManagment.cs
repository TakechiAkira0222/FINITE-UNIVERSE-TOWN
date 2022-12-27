using System;
using System.Collections;
using System.Collections.Generic;

using Photon.Realtime;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Takechi.UI.GameTypeSelection;
using Takechi.UI.RoomPropertySetting;
using Takechi.UI.MapSelection;
using Takechi.ScriptReference.CustomPropertyKey;

using TakechiEngine.PUN.ServerConnect.ToJoinRoom;

namespace Takechi.ServerConnect.ToJoinRoom 
{
    public class ConnectToJoinRoomManagment : TakechiConnectToJoinRoomPunCallbaks
    {
        [SerializeField] private GameTypeSelectionManagement   m_gameTypeSelection;
        [SerializeField] private RoomPropertySettingManagement m_roomPropertySetting;
        [SerializeField] private MapSelectionManagement        m_mapSelection;

        #region Event Action

        /// <summary>
        /// �����ɓ����������̏����B
        /// </summary>
        public event Action OnJoinedRoomAction = delegate { };
        /// <summary>
        /// �������쐬����Ƃ��̏����B
        /// </summary>
        public event Action<string, RoomOptions, ExitGames.Client.Photon.Hashtable> OnRoomCreationAction = delegate { };

        #endregion

        #region Unity Event

        void Awake()
        {
            OnRoomCreationAction += (name, roomOption, customRoomProperties) =>
            {
                CreateRoom(name, roomOption);
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

        #endregion

        #region EventSystemButton

        /// <summary>
        /// ���[���̐ݒ肪�I�������A�������쐬���ē�������B
        /// </summary>
        public void OnRoomCreation()
        {
            var roomOptions = new RoomOptions();

            roomOptions.MaxPlayers = (byte)(( m_gameTypeSelection.GetGameTypeSelectionIndex() + 1) * 2);

            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;

            /// <summary>
            /// �����̃J�X�^���v���p�e�B�[
            /// </summary>
            var customRoomProperties = new ExitGames.Client.Photon.Hashtable
            {
                { CustomPropertyKeyReference.s_RoomStatusGameType, m_gameTypeSelection.GetGameTypeSelectionIndex()},
                { CustomPropertyKeyReference.s_RoomSatusMap, m_mapSelection.GetMapSelectionIndex() },
            };

            OnRoomCreationAction( CheckForEnteredCharacters(m_roomPropertySetting.GetRoomName()), roomOptions, customRoomProperties); ;
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

        #region MonoBehaviourPunCallbacks

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            OnJoinedRoomAction();
        }

        #endregion
    }
}
