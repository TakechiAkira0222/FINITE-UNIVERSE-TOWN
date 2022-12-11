using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;

using TakechiEngine.PUN.CustomProperties;

namespace TakechiEngine.PUN.Information
{
    public class TakechiPunInformationDisclosure : TakechiPunCustomProperties
    {
        #region InformationTitle
        /// <summary>
        /// ���̕\��̃e���v���[�g �̐ݒ�
        /// </summary>
        /// <param name="titleName"> �f�o�b�N�̕\�薼 </param>
        /// <returns></returns>
        protected string InformationTitleTemplate(string titleName)
        {
            return $" { titleName} \n" + " <color=blue>Info</color> \n";
        }
        
        #endregion

        #region RoomInformation
        /// <summary>
        /// Room����\�����܂��B
        /// </summary>
        /// <remarks>
        /// Room���̃v���C���[�̐���N���C�A���g�̖��O�Ȃǂ�PhotnNetwork���Ȃ�ǂ�����ł��擾���\���ł��܂��B
        /// </remarks>
        public void RoomInformationDisplay()
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + WhatRoomInformationIsDisplayedInTheDebugLog());

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player {player.ActorNumber} Information");
                }
            }
        }

        /// <summary>
        /// Room����\�����܂��B���J�X�^���v���p�e�B�[�̌��̎Q�Ɛݒ肠��
        /// </summary>
        /// <remarks>
        /// Room���̃v���C���[�̐���N���C�A���g�̖��O�Ȃǂ�PhotnNetwork���Ȃ�ǂ�����ł��擾���\���ł��܂��B
        /// �܂��A�J�X�^���v���p�e�B�[�̖��O���擾���Ē��g��\�����܂��B
        /// </remarks>
        public void RoomInformationDisplay( string [] customRoomPropertieKeyNames , string[] customPlayerPropertieKeyNames)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + WhatRoomInformationIsDisplayedInTheDebugLog( customRoomPropertieKeyNames), this.gameObject);

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player { player.ActorNumber} Information" , customPlayerPropertieKeyNames);
                }
            }
        }

        /// <summary>
        /// Room�����f�o�b�N���O�ɕ\��������e�̐ݒ�
        /// </summary>
        /// <returns>
        /// PhotonNetwork �͈����ł̎Q�Ƃ��ł��܂���B
        /// ��PhotonNetwork���Q�Ɖ\�ȏꏊ�ł���΂ǂ�����ł��Ăяo�����Ƃ��ł��܂��B
        /// </returns>
        protected string WhatRoomInformationIsDisplayedInTheDebugLog()
        {
            return
               $" PlayerSlots :  <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
               $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
               $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n" +
               $" MyPlayerID : <color=green>{PhotonNetwork.LocalPlayer.ActorNumber}</color> \n"+
               $" AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene}</color> \n";
        }

        /// <summary>
        /// Room�����f�o�b�N���O�ɕ\��������e�̐ݒ� ���J�X�^���v���p�e�B�[�̌��̎Q�Ɛݒ肠��
        /// </summary>
        /// <returns>
        /// PhotonNetwork �͈����ł̎Q�Ƃ��ł��܂���B
        /// ��PhotonNetwork���Q�Ɖ\�ȏꏊ�ł���΂ǂ�����ł��Ăяo�����Ƃ��ł��܂�
        /// �܂��A�J�X�^���v���p�e�B�[�̖��O���擾���Ē��g��\�����܂��B
        /// </returns>
        protected string WhatRoomInformationIsDisplayedInTheDebugLog(string[] customPropertieKeyNames)
        {
            string s = $" PlayerSlots :  <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
               $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
               $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n" +
               $" MyPlayerID : <color=green>{PhotonNetwork.LocalPlayer.ActorNumber}</color> \n"+
               $" AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene}</color> \n";

            foreach (string propertie in customPropertieKeyNames)
            {
                s += $" CurrentRoom {propertie} : <color=green>{PhotonNetwork.CurrentRoom.CustomProperties[propertie]}</color> \n"; 
            }
            
            return s;
        }

        /// <summary>
        /// Room�����f�o�b�N���O�ɕ\��������e�̐ݒ�
        /// </summary>
        /// <param name="roomInfo"> ���[���̏�� </param>
        /// <returns>
        /// RoomInfo�@�ŎQ�Ƃ���ꍇ���̊֐����g�p���Ă��������B
        /// </returns>
        protected string WhatRoomInformationIsDisplayedInTheDebugLog(RoomInfo roomInfo)
        {
            return
                $" roomName : <color=green>{roomInfo.Name}</color> \n" +
                $" masterClientId : <color=green>{roomInfo.masterClientId}</color> \n" +
                $" PlayerCount : <color=green>{roomInfo.PlayerCount} / {roomInfo.MaxPlayers}</color> \n" +
                $" CustomProperties : <color=green>{roomInfo.CustomProperties}</color> \n" +
                $" Open : <color=green>{roomInfo.IsOpen}</color> \n" +
                $" Visible : <color=green>{roomInfo.IsVisible}</color> \n" +
                $" roomInfo : <color=green>{roomInfo}</color> \n";
        }
        #endregion

        #region PlayerInformation
  
        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����܂��B
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <param name="functionName"> �Ăяo�����̊֐����@</param>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName)
        {
            Debug.Log( InformationTitleTemplate( functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player) 
               , this);
        }
        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����܂��B
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <param name="functionName"> �Ăяo�����̊֐����@</param>
        /// <param name="playerState">�@�v���C���[�̏�� </param>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName, string playerState)
        {
            Debug.Log( InformationTitleTemplate( functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player) +
               $" {playerState} : <color=green>{ player}</color> \n"
               , this);
        }
        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����܂��B ���J�X�^���v���p�e�B�[�̌��̎Q�Ɛݒ肠��
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <param name="functionName"> �Ăяo�����̊֐����@</param>
        /// <param name="playerState">�@�v���C���[�̏�� </param>
        /// <returns>
        /// �J�X�^���v���p�e�B�[�̖��O���擾���Ē��g��\�����܂��B
        /// </returns>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName , string[] customPropertieKeyNames)
        {
            Debug.Log(InformationTitleTemplate(functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog( player , customPropertieKeyNames)
              , this);
        }
        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����܂��B ���J�X�^���v���p�e�B�[�̌��̎Q�Ɛݒ肠��
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <param name="functionName"> �Ăяo�����̊֐����@</param>
        /// <param name="playerState">�@�v���C���[�̏�� </param>
        /// <returns>
        /// �J�X�^���v���p�e�B�[�̖��O���擾���Ē��g��\�����܂��B
        /// </returns>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName, string playerState, string[] customPropertieKeyNames)
        {
            Debug.Log(InformationTitleTemplate(functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player, customPropertieKeyNames) +
               $" {playerState} : <color=green>{player}</color> \n"
               , this);
        }

        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\��������e�̐ݒ�
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <returns>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����������e�𕶎���Ƃ��ĕԂ��܂��B
        /// </returns>
        protected string WhatPlayerInformationIsDisplayedInTheDebugLog(Photon.Realtime.Player player)
        {
            return 
               $" ActorNumber : <color=green>{player.ActorNumber}</color> \n" +
               $" NickName : <color=green>{player.NickName}</color> \n" +
               $" IsMasterClient : <color=green>{player.IsMasterClient}</color> \n" +
               $" IsLocal : <color=green>{player.IsLocal}</color>\n" +
               $" TagObject : <color=green>{player.TagObject}</color>\n" ;
        }
        /// <summary>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\��������e�̐ݒ� ���J�X�^���v���p�e�B�[�̌��̎Q�Ɛݒ肠��
        /// </summary>
        /// <param name="player"> �v���C���[��� </param>
        /// <returns>
        /// �v���C���[�̏����f�o�b�N���O�ɕ\�����������e�𕶎���Ƃ��ĕԂ��܂��B
        /// �J�X�^���v���p�e�B�[�̖��O���擾���Ē��g��\�����܂��B
        /// </returns>
        protected string WhatPlayerInformationIsDisplayedInTheDebugLog(Photon.Realtime.Player player, string[] customPropertieKeyNames)
        {
            string s =   
               $" ActorNumber : <color=green>{player.ActorNumber}</color> \n" +
               $" NickName : <color=green>{player.NickName}</color> \n" +
               $" IsMasterClient : <color=green>{player.IsMasterClient}</color> \n" +
               $" IsLocal : <color=green>{player.IsLocal}</color>\n" +
               $" TagObject : <color=green>{player.TagObject}</color>\n";

            foreach (string propertie in customPropertieKeyNames)
            {
                s += $" Player {propertie} : <color=green>{player.CustomProperties[propertie]}</color> \n";
            }

            return s;
        }
        #endregion

    }
}
