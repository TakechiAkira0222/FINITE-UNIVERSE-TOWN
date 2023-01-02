using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

using Photon.Pun;
using TakechiEngine.PUN.ServerConnect.Joined;
using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.ServerConnect.ToJoinRoom;

namespace Takechi.UI.RoomJoinedMenu
{
    /// <summary>
    /// ���[���ڑ���ɕ\������郁�j���[���A�Ǘ�����B
    /// </summary>
    public class RoomJoinedMenuManagement : TakechiJoinedPunCallbacks
    {
        #region SerializeField

        [SerializeField] private Button m_gameStartButton;
        [SerializeField] private Text m_instansText;
        [SerializeField] private Text m_roomInfometionText;
        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;
        #endregion

        private const string teamAName = "TeamA";
        private const string teamBName = "TeamB";

        private List<Player> m_teamA_memberList = new List<Player>();
        private List<Player> m_teamB_memberList = new List<Player>();

        #region update variable

        private void updateRoomInfometionText() { m_roomInfometionText.text = WhatTheTextDisplays(); }
        #endregion

        #region Unity Event

        public override void OnEnable()
        {
            base.OnEnable();

            StartCoroutine(nameof(ListUpdate));

            RandomTeamSetting();

            updateRoomInfometionText();

            OnlyClientsDisplay( m_gameStartButton.gameObject);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            RefreshTheListYouAreViewing();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            updateRoomInfometionText();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            updateRoomInfometionText();
        }

        #endregion

        #region PunCallbacks

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            OnlyClientsDisplay( m_gameStartButton.gameObject);
        }

        #endregion

        #region event system finction

        public void OnSceneSyncChange() { SceneSyncChange(2); }

        public void OnLeaveRoom() { LeaveRoom(); }

        public void OnChangeTeam(string teamName)
        {
            if (teamAName != teamName && teamBName != teamName)
            {
                Debug.LogError("The variable you are setting for the button is different from the expected value.");
                return;
            }

            if ( teamName == teamAName)
            {
                if (!MaximumOfMembers( m_teamA_memberList))
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamName);
                }
                else
                {
                    Debug.LogWarning(" The change has been canceled because the new team member is full. ");
                }
            }
            else if( teamName == teamBName)
            {
                if (!MaximumOfMembers( m_teamB_memberList))
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamName);
                }
                else
                {
                    Debug.LogWarning(" The change has been canceled because the new team member is full. ");
                }
            }

            PlayerInformationDisplay(PhotonNetwork.LocalPlayer, " ChangeTeamPlayerInfometion ", CustomPropertyKeyReference.s_CharacterStatusKeys);
        }

        #endregion

        #region Update IEnumerator 

        private IEnumerator ListUpdate()
        {
            while ( this.gameObject.activeSelf)
            {
                m_teamA_memberList = new List<Player>();
                m_teamB_memberList = new List<Player>();

                foreach ( Player player in PhotonNetwork.PlayerList)
                {
                    if (( player.CustomProperties[CustomPropertyKeyReference.s_CharacterStatusTeam] is string value ? value : "null") == teamAName)
                    {
                        m_teamA_memberList.Add(player);
                    }
                    else
                    {
                        m_teamB_memberList.Add(player);
                    }
                }

                RefreshTheListYouAreViewing();

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        #endregion

        #region private finction

        private string WhatTheTextDisplays()
        {
            string s
               = $" PlayerSlots : <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
                 $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
                 $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n";

            foreach (string propertie in CustomPropertyKeyReference.s_RoomStatusKeys)
            {
                s += $" CurrentRoom {propertie} : <color=green>{PhotonNetwork.CurrentRoom.CustomProperties[propertie]}</color> \n";
            }

            return s;
        }

        /// <summary>
        /// �\�����̃��X�g�̍X�V
        /// </summary>
        private void RefreshTheListYouAreViewing()
        {
            DestroyChildObjects(m_teamAContent);
            DestroyChildObjects(m_teamBContent);

            InstantiateTheText(m_instansText, m_teamA_memberList, m_teamAContent.transform);
            InstantiateTheText(m_instansText, m_teamB_memberList, m_teamBContent.transform);
        }

        /// <summary>
        /// �����_���ɓ������ĕ����̏󋵂����āA�ݒ肷��B
        /// </summary>
        private void RandomTeamSetting()
        {
            if (returnsTheProbabilityOf1In2())
            {
                if (!MaximumOfMembers(m_teamA_memberList))
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamAName);
                }
                else
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamBName);
                }
            }
            else
            {
                if (!MaximumOfMembers(m_teamB_memberList))
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamBName);
                }
                else
                {
                    setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusTeam, teamAName);
                }
            }

            PlayerInformationDisplay(PhotonNetwork.LocalPlayer, " RandomTeamSetting ", CustomPropertyKeyReference.s_CharacterStatusKeys);
        }

        /// <summary>
        /// �q�I�u�W�F�N�g�̑S�������܂��B
        /// </summary>
        /// <param name="parent"></param>
        private void DestroyChildObjects(GameObject parent)
        {
            foreach (Transform n in parent.transform) { GameObject.Destroy(n.gameObject); }
        }

        /// <summary>
        /// �e�L�X�g���A�C���X�^���X�����܂��B
        /// </summary>
        /// <param name="instansText"></param>
        /// <param name="players"></param>
        /// <param name="p"></param>
        private void InstantiateTheText(Text instansText, List<Player> players, Transform p)
        {
            foreach (Player n in players) { Instantiate(instansText, p).text = n.NickName; }
        }

        /// <summary>
        /// �񕪂̈�̊m����Ԃ��܂��B
        /// </summary>
        /// <returns></returns>
        private bool returnsTheProbabilityOf1In2() { return Random.Range(1, 10) % 2 == 0; }

        /// <summary>
        /// memberList�̍ő�ɒB���Ă��邩�ǂ����̊m�F���܂��B
        /// </summary>
        /// <param name="teamMemberList"> teamMemberList</param>
        /// <returns> max = true </returns>
        private bool MaximumOfMembers(List<Player> teamMemberList)
        {
            return teamMemberList.Count >= PhotonNetwork.CurrentRoom.MaxPlayers / 2;
        }

        #endregion
    }
}
