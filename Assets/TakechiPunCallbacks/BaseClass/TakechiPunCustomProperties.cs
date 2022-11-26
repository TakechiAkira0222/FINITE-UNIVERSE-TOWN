using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TakechiEngine.PUN.CustomProperties
{
    public class TakechiPunCustomProperties : TakechiPunCallbacks
    {
        /// <summary>
        /// ニックネームを付ける
        /// </summary>
        /// <param name="nickName"> 設定したい自分の名前 </param>
        protected void SetMyNickName(string nickName)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = nickName;

                Debug.Log($" LocalPlayer.NickName : <color=blue>{nickName}</color> <color=green> to set.</color>");
            }
        }

        #region UpdateCustomRoomProperties
        /// <summary>
        /// 部屋のカスタムプロパティを更新する
        /// </summary>
        /// <param name="customRoomProperties"> 更新したい状態のプロパティー </param>
        protected void UpdateRoomCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
                Debug.Log($" customRoomProperties : <color=green>setCustomProperties.</color>");
            }
        }
        /// <summary>
        /// 部屋のカスタムプロパティを更新する
        /// </summary>
        /// <remarks>　部屋のカスタムプロパティーに存在するかどうかを見て追記もしくは書き換えを行います。　</remarks>>
        /// <param name="key"> プロパティーの鍵　</param>
        /// <param name="o">　変更内容のオブジェクト型 </param>
        protected void UpdateRoomCustomProperties(string key, object o)
        {
            if ( PhotonNetwork.InRoom)
            {
                object temp = null;
                ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable();

                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(key, out temp))
                {
                    hastable[key] = o;
                    Debug.Log($" customRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    hastable.Add(key, o);
                    Debug.Log($" customRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.CurrentRoom.SetCustomProperties(hastable);
            }
        }
        /// <summary>
        /// 部屋のカスタムプロパティを更新する
        /// </summary>
        /// <remarks>　部屋のカスタムプロパティーに存在するかどうかを見て追記もしくは書き換えを行います。　</remarks>>
        /// <param name="customRoomProperties">　現在のプロパティー </param>
        /// <param name="key"> プロパティーの鍵　</param>
        /// <param name="o">　変更内容のオブジェクト型 </param>
        protected void UpdateRoomCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties, string key, object o)
        {
            if (PhotonNetwork.InRoom)
            {
                object temp = null;
                if (customRoomProperties.TryGetValue(key, out temp))
                {
                    customRoomProperties[key] = o;
                    Debug.Log($" customRoomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    customRoomProperties.Add(key, o);
                    Debug.Log($" customRoomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
            }
        }

        #endregion

        #region UpdateCustomPlayerProperties
        /// <summary>
        /// 自身のカスタムプロパティを更新する
        /// </summary>
        /// <param name="customRoomProperties"> 更新したい状態のプロパティー </param>
        protected void UpdateLocalPlayerProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(customRoomProperties);
                Debug.Log($" customPlayerProperties : <color=green>setCustomProperties.</color>");
            }
        }
        /// <summary>
        /// 自身のカスタムプロパティを更新する
        /// </summary>
        /// <remarks>　自身のカスタムプロパティに存在するかどうかを見て追記もしくは書き換えを行います。　</remarks>>
        /// <param name="key"> プロパティーの鍵　</param>
        /// <param name="o">　変更内容のオブジェクト型 </param>
        protected void UpdateLocalPlayerProperties(string key, object o)
        {
            if (PhotonNetwork.IsConnected)
            {
                object temp = null;
                ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable();

                if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(key, out temp))
                {
                    hastable[key] = o;
                    Debug.Log($" customPlayerProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    hastable.Add(key, o);
                    Debug.Log($" customPlayerProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.LocalPlayer.SetCustomProperties(hastable);
            }
        }
        /// <summary>
        /// 自身のカスタムプロパティを更新する
        /// </summary>
        /// <remarks>　自身のカスタムプロパティに存在するかどうかを見て追記もしくは書き換えを行います。　</remarks>>
        /// <param name="customPlayerProperties">　現在のプロパティ </param>
        /// <param name="key"> プロパティーの鍵　</param>
        /// <param name="o">　変更内容のオブジェクト型 </param>
        protected void UpdateLocalPlayerProperties(ExitGames.Client.Photon.Hashtable customPlayerProperties, string key, object o)
        {
            if (PhotonNetwork.InRoom)
            {
                object temp = null;
                if (customPlayerProperties.TryGetValue(key, out temp))
                {
                    customPlayerProperties[key] = o;
                    Debug.Log($" customPlayerProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    customPlayerProperties.Add(key, o);
                    Debug.Log($" customPlayerProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.LocalPlayer.SetCustomProperties(customPlayerProperties);
            }
        }

        #endregion

    }
}
