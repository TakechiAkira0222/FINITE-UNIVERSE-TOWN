using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

using Photon.Pun;
using Photon.Realtime;

using TakechiEngine.PUN.ServerConnect.Joined;

using Takechi.ServerConnect.ToJoinRoom;
using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.UI.CanvasMune.DisplayListUpdate;

using System.Threading.Tasks;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using System.Diagnostics.SymbolStore;

namespace Takechi.UI.RoomJoinedMenu
{
    /// <summary>
    /// ルーム接続後に表示されるメニューを、管理する。
    /// </summary>
    public class RoomJoinedMenuManagement : DisplayListUpdateManagement
    {
        #region SerializeField

        [Header(" ui setting ")]
        [SerializeField] private Text   m_roomInfometionText;
        [SerializeField] private Button m_gameStartButton;

        [Header(" memberList setting ")]
        [SerializeField] private Text       m_instansText;
        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;

        #endregion

        private List<Player> m_teamA_memberList = new List<Player>(4);
        private List<Player> m_teamB_memberList = new List<Player>(4);

        #region update variable
        private void updateRoomInfometionText() { m_roomInfometionText.text = WhatTheTextDisplays(); }

        #endregion

        #region reset variable
        private void resetRoomInfometionText() { m_roomInfometionText.text = "\r\n\r\nconnecting..."; }

        #endregion

        #region private async

        /// <summary>
        /// 同期時間経過後に、実行されます。
        /// </summary>
        /// <param name="s"></param>
        private async void StartAfterSync(int s)
        {
            await Task.Delay(s);

            StartCoroutine(nameof(ListUpdate));

            updateRoomInfometionText();
        }

        #endregion

        #region Unity Event

        public override void OnEnable()
        {
            base.OnEnable();

            RandomlyJoinTeam();

            StartAfterSync( NetworkSyncSettings.connectionSynchronizationTime);

            m_gameStartButton.interactable = false;
            OnlyClientsDisplay( m_gameStartButton.gameObject);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            DestroyChildObjects( m_teamAContent);
            DestroyChildObjects( m_teamBContent);
            resetRoomInfometionText();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            updateRoomInfometionText();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            updateRoomInfometionText();
        }

        #endregion

        #region PunCallbacks

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            OnlyClientsDisplay( m_gameStartButton.gameObject);
        }

        #endregion

        #region event system finction

        public void OnSceneSyncChange() { SceneSyncChange(2); }

        public void OnLeaveRoom() { LeaveRoom(); }

        public void OnChangeTeam(string teamName)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            if (CharacterTeamStatusName.teamAName != teamName && CharacterTeamStatusName.teamBName != teamName)
            {
                Debug.LogError("The variable you are setting for the button is different from the expected value.");
                return;
            }

            if ( teamName == CharacterTeamStatusName.teamAName)
            {
                if (!MaximumOfMembers( m_teamA_memberList))
                {
                    setLocalPlayerCustomProperties(CharacterStatusKey.teamKey, teamName);
                }
                else
                {
                    Debug.LogWarning(" The change has been canceled because the new team member is full. ");
                }
            }
            else if( teamName == CharacterTeamStatusName.teamBName)
            {
                if (!MaximumOfMembers( m_teamB_memberList))
                {
                    setLocalPlayerCustomProperties(CharacterStatusKey.teamKey, teamName);
                }
                else
                {
                    Debug.LogWarning(" The change has been canceled because the new team member is full. ");
                }
            }

            PlayerInformationDisplay(PhotonNetwork.LocalPlayer, " ChangeTeamPlayerInfometion ", CharacterStatusKey.allKeys);
        }

        #endregion

        #region IEnumerator Update 

        protected override IEnumerator ListUpdate()
        {
            base.ListUpdate();
            
            while ( this.gameObject.activeSelf)
            {
                m_teamA_memberList = new List<Player>();
                m_teamB_memberList = new List<Player>();

                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if ((player.CustomProperties[CharacterStatusKey.teamKey] is string value ? value : "null") == CharacterTeamStatusName.teamAName)
                    {
                        m_teamA_memberList.Add(player);
                    }
                    else
                    {
                        m_teamB_memberList.Add(player);
                    }
                }

                RefreshTheListViewing();
                m_gameStartButton.interactable = AppropriateConfirmationOfTheNumberOfPeople();

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        protected override void RefreshTheListViewing()
        {
            DestroyChildObjects(m_teamAContent);
            DestroyChildObjects(m_teamBContent);

            InstantiateTheContentsOfListAndChangeText<Text, Player>( m_instansText, m_teamA_memberList, m_teamAContent.transform);
            InstantiateTheContentsOfListAndChangeText<Text, Player>( m_instansText, m_teamB_memberList, m_teamBContent.transform);
        }

        #endregion

        #region private finction

        /// <summary>
        /// ランダムにチームに入室する
        /// </summary>
        private void RandomlyJoinTeam()
        {
            if (ReturnsTheProbabilityOf1In2()) { setLocalPlayerCustomProperties(CharacterStatusKey.teamKey, CharacterTeamStatusName.teamAName); }
            else { setLocalPlayerCustomProperties(CharacterStatusKey.teamKey, CharacterTeamStatusName.teamBName); }

            PlayerInformationDisplay(PhotonNetwork.LocalPlayer, " RandomTeamSetting ", CharacterStatusKey.allKeys);
        }

        private bool AppropriateConfirmationOfTheNumberOfPeople()
        {
            return true;
            //return m_teamA_memberList.Count == m_teamB_memberList.Count ? true : false;
        }

        private string WhatTheTextDisplays()
        {
            string s
               = $" PlayerSlots : <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
                 $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
                 $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n";

            foreach (string propertie in RoomStatusKey.allKeys)
            {
                s += $" CurrentRoom {propertie} : <color=green>{PhotonNetwork.CurrentRoom.CustomProperties[propertie]}</color> \n";
            }

            return s;
        }

        #endregion
    }
}
