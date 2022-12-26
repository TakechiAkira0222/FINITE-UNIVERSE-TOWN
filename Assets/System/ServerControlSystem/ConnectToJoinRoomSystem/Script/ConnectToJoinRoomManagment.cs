using System;
using System.Collections;
using System.Collections.Generic;

using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TakechiEngine.PUN.ServerConnect;
using UnityEngine.SceneManagement;


using Takechi.UI.GameTypeSelection;
using Takechi.UI.RoomPropertySetting;
using Takechi.UI.MapSelection;
using Takechi.ScriptReference.CustomPropertyKey;

namespace Takechi.ServerConnect.ConnectToJoinRoom
{
    public class ConnectToJoinRoomManagment : TakechiServerConnectPunCallbacks
    {
        [SerializeField] private GameTypeSelectionManagement   m_gameTypeSelection;
        [SerializeField] private RoomPropertySettingManagement m_roomPropertySetting;
        [SerializeField] private MapSelectionManagement        m_mapSelection;

        #region CustomProperties
       

        /// <summary>
        /// ���[�����X�g
        /// </summary>
        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();

        #endregion

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

            roomOptions.MaxPlayers = (byte)((m_gameTypeSelection.GetGameTypeSelectionIndex() + 1) * 2);

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

        #endregion

        #region private finction

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
