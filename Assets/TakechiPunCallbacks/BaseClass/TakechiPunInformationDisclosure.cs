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
        /// 情報の表題のテンプレート の設定
        /// </summary>
        /// <param name="titleName"> デバックの表題名 </param>
        /// <returns></returns>
        protected string InformationTitleTemplate(string titleName)
        {
            return $" { titleName} \n" + " <color=blue>Info</color> \n";
        }
        
        #endregion

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
                Debug.Log(InformationTitleTemplate("RoomInformation") + WhatRoomInformationIsDisplayedInTheDebugLog());

                foreach (var player in PhotonNetwork.PlayerList)
                {
                    PlayerInformationDisplay(player, $"Player {player.ActorNumber} Information");
                }
            }
        }

        /// <summary>
        /// Room情報を表示します。※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <remarks>
        /// Room内のプレイヤーの数やクライアントの名前などをPhotnNetwork内ならどこからでも取得し表示できます。
        /// また、カスタムプロパティーの名前を取得して中身を表示します。
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
        /// Room情報をデバックログに表示する内容の設定
        /// </summary>
        /// <returns>
        /// PhotonNetwork は引数での参照ができません。
        /// ※PhotonNetworkが参照可能な場所であればどこからでも呼び出すことができます。
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
        /// Room情報をデバックログに表示する内容の設定 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <returns>
        /// PhotonNetwork は引数での参照ができません。
        /// ※PhotonNetworkが参照可能な場所であればどこからでも呼び出すことができます
        /// また、カスタムプロパティーの名前を取得して中身を表示します。
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
        /// Room情報をデバックログに表示する内容の設定
        /// </summary>
        /// <param name="roomInfo"> ルームの情報 </param>
        /// <returns>
        /// RoomInfo　で参照する場合この関数を使用してください。
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
        /// プレイヤーの情報をデバックログに表示します。
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="functionName"> 呼び出し元の関数名　</param>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName)
        {
            Debug.Log( InformationTitleTemplate( functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player) 
               , this);
        }
        /// <summary>
        /// プレイヤーの情報をデバックログに表示します。
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="functionName"> 呼び出し元の関数名　</param>
        /// <param name="playerState">　プレイヤーの状態 </param>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName, string playerState)
        {
            Debug.Log( InformationTitleTemplate( functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player) +
               $" {playerState} : <color=green>{ player}</color> \n"
               , this);
        }
        /// <summary>
        /// プレイヤーの情報をデバックログに表示します。 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="functionName"> 呼び出し元の関数名　</param>
        /// <param name="playerState">　プレイヤーの状態 </param>
        /// <returns>
        /// カスタムプロパティーの名前を取得して中身を表示します。
        /// </returns>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName , string[] customPropertieKeyNames)
        {
            Debug.Log(InformationTitleTemplate(functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog( player , customPropertieKeyNames)
              , this);
        }
        /// <summary>
        /// プレイヤーの情報をデバックログに表示します。 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <param name="functionName"> 呼び出し元の関数名　</param>
        /// <param name="playerState">　プレイヤーの状態 </param>
        /// <returns>
        /// カスタムプロパティーの名前を取得して中身を表示します。
        /// </returns>
        protected void PlayerInformationDisplay(Photon.Realtime.Player player, string functionName, string playerState, string[] customPropertieKeyNames)
        {
            Debug.Log(InformationTitleTemplate(functionName) + WhatPlayerInformationIsDisplayedInTheDebugLog(player, customPropertieKeyNames) +
               $" {playerState} : <color=green>{player}</color> \n"
               , this);
        }

        /// <summary>
        /// プレイヤーの情報をデバックログに表示する内容の設定
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <returns>
        /// プレイヤーの情報をデバックログに表示したい内容を文字列として返します。
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
        /// プレイヤーの情報をデバックログに表示する内容の設定 ※カスタムプロパティーの鍵の参照設定あり
        /// </summary>
        /// <param name="player"> プレイヤー情報 </param>
        /// <returns>
        /// プレイヤーの情報をデバックログに表示したい内容を文字列として返します。
        /// カスタムプロパティーの名前を取得して中身を表示します。
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
