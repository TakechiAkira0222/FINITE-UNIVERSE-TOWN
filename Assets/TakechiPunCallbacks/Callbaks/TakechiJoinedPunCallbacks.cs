using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Realtime;
using TakechiEngine.PUN.Information;

namespace TakechiEngine.PUN.ServerConnect.Joined
{
    public class TakechiJoinedPunCallbacks : TakechiPunInformationDisclosure
    {
        /// <summary>
        /// 参考資料
        /// </summary>
        /// 
        /// 用語集
        /// https://doc.photonengine.com/ja-jp/pun/current/reference/glossary
        ////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// マスタークライアントが変わった時
        /// </summary>
        /// <remarks>
        /// このクライアントがルームに入るとき、これは呼び出されません。 
        /// このメソッドが呼び出されたとき、以前のマスター クライアントはプレイヤー リストに残っています。
        /// </remarks>
        /// <param name="newMasterClient"></param>
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            PlayerInformationDisplay(newMasterClient, "OnMasterClientSwitched", "newMasterClient");
        }

        /// <summary>
        /// ルームプロパティが更新された時
        /// </summary>
        /// <param name="propertiesThatChanged">
        ///　ルームプロパティ
        /// </param>
        /// <remarks>　プロパティーに更新が入った時、更新されたKeyの変更内容を返します。</remarks>>
        public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
        {
            // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
            foreach (var prop in propertiesThatChanged)
            {
                Debug.Log($" Room property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>", this.gameObject);
        }

        /// <summary>
        /// プレイヤープロパティが更新された時
        /// </summary>
        /// <param name="target">
        /// 
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        /// <param name="changedProps">
        /// ルームプロパティ
        /// </param>
        public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
        {
            // 更新されたプレイヤーのカスタムプロパティのペアをコンソールに出力する
            foreach (var prop in changedProps)
            {
                Debug.Log($" Player property <color=orange>'{prop.Key}'</color> : <color=blue>{prop.Value}</color> <color=green>changed.</color>");
            }

            Debug.Log($"OnRoomPropertiesUpdate :<color=green> clear </color>");
        }

        /// <summary>
        /// 他のプレイヤーが入室してきた時
        /// </summary>
        /// <param name="newPlayer">
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            PlayerInformationDisplay(newPlayer, "OnPlayerEnteredRoom", "newPlayer");
        }

        /// <summary>
        /// 他のプレイヤーが退室した時
        /// </summary>
        /// <param name="otherPlayer">
        /// プレイヤーのデータ
        /// 参考
        /// https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_player.html
        /// </param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerInformationDisplay(otherPlayer, "OnPlayerLeftRoom", "otherPlayerr");
        }
    }
}
