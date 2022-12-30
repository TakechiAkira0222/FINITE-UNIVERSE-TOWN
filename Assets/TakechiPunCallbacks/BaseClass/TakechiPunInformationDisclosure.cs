using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;

using TakechiEngine.PUN.CustomProperties;
using System.Net.NetworkInformation;
using System.Security.AccessControl;

namespace TakechiEngine.PUN.Information
{
    public class TakechiPunInformationDisclosure : TakechiPunCustomProperties
    {
        #region RoomInformation

        /// <summary>
        /// Room情報を表示します。
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数やクライアントの名前などをPhotnNetwork内ならどこからでも取得し表示できます。
        /// </remarks>
        public void RoomInformationDisplay()
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents());
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        /// <summary>
        /// Room情報を表示します。※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数や、クライアントの名前などを、部屋の中ならどこからでも取得し表示できます。
        /// </remarks>
        /// <param name="customRoomPropertieKeyNames"> 部屋のカスタムプロパティーKeyを取得して中身を表示します。</param>
        public void RoomInformationDisplay(string[] customRoomPropertieKeyNames)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents(customRoomPropertieKeyNames), this.gameObject);
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        /// <summary>
        /// Room情報を表示します。
        /// </summary>
        /// <remarks>
        /// RoomInfoが取得可能の場合、使うことができます。
        /// </remarks>
        public void RoomInformationDisplay( RoomInfo roomInfo)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents(roomInfo), this);
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        /// <summary>
        /// Room情報を表示します。※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <remarks>
        /// RoomInfoが取得可能の場合、使うことができます。
        /// </remarks>
        /// <param name="roomInfo"> 部屋の情報 </param>
        /// <param name="customRoomPropertieKeyNames"> 部屋のカスタムプロパティーKeyを取得して中身を表示します。</param>
        public void RoomInformationDisplay(RoomInfo roomInfo, string[] customRoomPropertieKeyNames)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents(roomInfo, customRoomPropertieKeyNames), this);
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        #endregion

        #region PlayerInformation

        /// <summary>
        /// プレイヤーの情報をデバックログに表示します。
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="playerState"> 呼び出し元の関数名　</param>
        protected void PlayerInformationDisplay( Player player, string playerState)
        {
            Debug.Log( InformationTitleTemplate(playerState) + WhatPlayerInformationIsDisplayedInTheDebugLog(player) , this);
        }

        /// <summary>
        /// プレイヤーの情報をデバックログに表示します。 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="playerState">　プレイヤーの状態 </param>
        /// <param name="functionName"> 呼び出し元の関数名　</param>
        /// <returns>
        /// カスタムプロパティーの名前を取得して中身を表示します。
        /// </returns>
        protected void PlayerInformationDisplay( Player player, string playerState, string[] customPropertieKeyNames)
        {
            Debug.Log( InformationTitleTemplate(playerState) + WhatPlayerInformationIsDisplayedInTheDebugLog( player , customPropertieKeyNames), this);
        }

        #endregion

        #region RoomInfoAndPlayerInfoDisplay

        /// <summary>
        /// 部屋の情報と部屋に接続しているプレイヤーの情報を表示します。
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数や、クライアントの名前などを、部屋の中ならどこからでも取得し表示できます。
        /// </remarks>
        public void RoomInfoAndJoinedPlayerInfoDisplay()
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents());

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player <color=blue>{player.ActorNumber}</color> Information");
                }
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        /// <summary>
        /// 部屋の情報と部屋に接続しているプレイヤーの情報を表示します。
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数や、クライアントの名前などを、部屋の中ならどこからでも取得し表示できます。
        /// </remarks>
        public void RoomInfoAndJoinedPlayerInfoDisplay(string[] customRoomPropertieKeyNames)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents(customRoomPropertieKeyNames));

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player <color=blue>{player.ActorNumber}</color> Information");
                }
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        /// <summary>
        /// 部屋の情報と、部屋に接続しているプレイヤーの情報を表示します。※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数や、クライアントの名前などを、部屋の中ならどこからでも取得し表示できます。
        /// </remarks>
        /// <param name="customRoomPropertieKeyNames"> 部屋の RoomCustomPropetieKey を取得して中身を表示します。</param>
        /// <param name="customPlayerPropertieKeyNames"> 部屋のにいるcharacterの　PlayerCustomPropetieKeyを、取得して中身を表示します。</param>
        public void RoomInfoAndJoinedPlayerInfoDisplay(string[] customRoomPropertieKeyNames, string[] customPlayerPropertieKeyNames)
        {
            if (PhotonNetwork.InRoom)
            {
                Debug.Log(InformationTitleTemplate("RoomInformation") + RoomInformationDisplayContents(customRoomPropertieKeyNames), this.gameObject);

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player <color=blue>{player.ActorNumber}</color> Information", customPlayerPropertieKeyNames);
                }
            }
            else
            {
                Debug.LogWarning(" Debug is not shown because it is not connected to the room.");
            }
        }

        #endregion
        
        #region InformationTitle

        /// <summary>
        /// 情報の表題のテンプレート の設定
        /// </summary>
        /// <param name="titleName"> デバックの表題名 </param>
        /// <returns></returns>
        protected string InformationTitleTemplate(string titleName)
        {
            return $" { titleName} \n" + " <color=blue>Info</color> \n";
        }

        #endregion

        #region private roomInfo function

        /// <summary>
        /// Room情報をデバックログに表示する内容の設定
        /// </summary>
        /// <param name="roomInfo"> ルームの情報 </param>
        /// <returns>
        /// RoomInfo　で参照する場合この関数を使用してください。
        /// </returns>
        private string RoomInformationDisplayContents(RoomInfo roomInfo)
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

        /// <summary>
        /// Room情報をデバックログに表示する内容の設定 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="roomInfo"> ルームの情報 </param>
        /// <returns>
        /// RoomInfo　で参照する場合この関数を使用してください。
        /// </returns>
        private string RoomInformationDisplayContents(RoomInfo roomInfo, string[] customPropertieKeyNames)
        {
            string s =
                $" roomName : <color=green>{roomInfo.Name}</color> \n" +
                $" masterClientId : <color=green>{roomInfo.masterClientId}</color> \n" +
                $" PlayerCount : <color=green>{roomInfo.PlayerCount} / {roomInfo.MaxPlayers}</color> \n" +
                $" CustomProperties : <color=green>{roomInfo.CustomProperties}</color> \n" +
                $" Open : <color=green>{roomInfo.IsOpen}</color> \n" +
                $" Visible : <color=green>{roomInfo.IsVisible}</color> \n" +
                $" roomInfo : <color=green>{roomInfo}</color> \n";

            foreach (string propertie in customPropertieKeyNames)
            {
                s += $" CurrentRoom {propertie} : <color=green>{PhotonNetwork.CurrentRoom.CustomProperties[propertie]}</color> \n";
            }

            return s;
        }

        /// <summary>
        /// Room情報をデバックログに表示する内容の設定
        /// </summary>
        /// <returns>
        /// PhotonNetwork は引数での参照ができません。
        /// ※PhotonNetworkが参照可能な場所であればどこからでも呼び出すことができます。
        /// </returns>
        private string RoomInformationDisplayContents()
        {
            return
               $" PlayerSlots : <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
               $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
               $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n" +
               $" MyPlayerID : <color=green>{PhotonNetwork.LocalPlayer.ActorNumber}</color> \n" +
               $" AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene}</color> \n";
        }

        /// <summary>
        /// Room情報をデバックログに表示する内容の設定 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <returns>
        /// PhotonNetwork は引数での参照ができません。
        /// ※PhotonNetworkが参照可能な場所であればどこからでも呼び出すことができます
        /// また、カスタムプロパティーの名前を取得して中身を表示します。
        /// </returns>
        private string RoomInformationDisplayContents(string[] customPropertieKeyNames)
        {
            string s = 
               $" PlayerSlots : <color=green>{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}</color> \n" +
               $" RoomName : <color=green>{PhotonNetwork.CurrentRoom.Name}</color> \n" +
               $" HostName : <color=green>{PhotonNetwork.MasterClient.NickName}</color> \n" +
               $" MyPlayerID : <color=green>{PhotonNetwork.LocalPlayer.ActorNumber}</color> \n" +
               $" AutomaticallySyncScene : <color=green>{PhotonNetwork.AutomaticallySyncScene}</color> \n";

            foreach (string propertie in customPropertieKeyNames)
            {
                s += $" CurrentRoom {propertie} : <color=green>{PhotonNetwork.CurrentRoom.CustomProperties[propertie]}</color> \n";
            }

            return s;
        }

        #endregion

        #region private roominfo finction

        /// <summary>
        /// プレイヤーの情報をデバックログに表示する内容の設定
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <returns>
        /// プレイヤーの情報をデバックログに表示したい内容を文字列として返します。
        /// </returns>
        private string WhatPlayerInformationIsDisplayedInTheDebugLog(Photon.Realtime.Player player)
        {
            return
               $" ActorNumber : <color=green>{player.ActorNumber}</color> \n" +
               $" NickName : <color=green>{player.NickName}</color> \n" +
               $" IsMasterClient : <color=green>{player.IsMasterClient}</color> \n" +
               $" IsLocal : <color=green>{player.IsLocal}</color>\n" +
               $" TagObject : <color=green>{player.TagObject}</color>\n";
        }

        /// <summary>
        /// プレイヤーの情報をデバックログに表示する内容の設定 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <returns>
        /// プレイヤーの情報をデバックログに表示したい内容を文字列として返します。
        /// カスタムプロパティーの名前を取得して中身を表示します。
        /// </returns>
        private string WhatPlayerInformationIsDisplayedInTheDebugLog(Photon.Realtime.Player player, string[] customPropertieKeyNames)
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
