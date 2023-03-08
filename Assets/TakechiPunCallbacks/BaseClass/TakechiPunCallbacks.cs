using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TakechiEngine.PUN
{
    public class TakechiPunCallbacks : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// 渡された処理を指定時間後に実行する
        /// </summary>
        /// <param name="waitTime">遅延時間[ミリ秒]</param>
        /// <param name="action">実行したい処理</param>
        /// <returns></returns>
        protected IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        #region SettingsClass

        /// <summary>
        /// ルーム基本設定の定義
        /// </summary>
        /// <param name="maxPlayers"> 最大人数 </param>
        /// <param name="isVisible"> 公開 or 非公開 </param>
        /// <param name="isOpen"> 入出許可　</param>
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

        #region SyncChange

        /// <summary>
        /// マスタークライアントと同じシーンに移動させる。
        /// </summary>
        /// <remarks> 
        /// この関数を使用するときには、PhotonNetwork.AutomaticallySyncScene = true にしてください。
        /// </remarks>
        /// <param name ="sceneNumber"> 移行したいシーンの番号 </param>
        protected void SceneSyncChange(int sceneNumber)
        {
            if ( !PhotonNetwork.IsMasterClient) return;

            PhotonNetwork.LoadLevel(sceneNumber);
            Debug.Log($"<color=green> SceneSyncChange SceneNumber</color> : {sceneNumber}");
        }
        /// <summary>
        /// マスタークライアントと同じシーンに移動させる。
        /// </summary>
        /// <remarks> 
        /// この関数を使用するときには、PhotonNetwork.AutomaticallySyncScene = true にしてください。
        /// </remarks>
        /// <param name ="sceneName"> 移行したいシーンの名前 </param>
        protected void SceneSyncChange(string sceneName)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            PhotonNetwork.LoadLevel(sceneName);
            Debug.Log($"<color=green> SceneSyncChange SceneNumber</color> : {sceneName}");
        }

        #endregion

        #region recursive function

        /// <summary>
        /// クライアントの場合表示する。
        /// </summary>
        /// <param name="obj"> 管理したいオブジェクト </param>
        protected void OnlyClientsDisplay(GameObject obj)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                obj.SetActive(true);
                Debug.Log($" OnlyClientsDisplay {obj.name}.SetActive :<color=blue> true </color>");
            }
            else
            {
                obj.SetActive(false);
                Debug.Log($" OnlyClientsDisplay {obj.name}.SetActive :<color=blue> false </color>");
            }
        }

        /// <summary>
        /// クライアントのみ干渉可能にする。
        /// </summary>
        /// <param name="button"> 管理したいButton </param>
        protected void OnlyClientsCanInterfere(Button button)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                button.interactable = true;
                Debug.Log($" OnlyClientsCanInterfere {button.name}.interactable :<color=blue> true </color>");
            }
            else
            {
                button.interactable = false;
                Debug.Log($" OnlyClientsCanInterfere {button.name}.interactable :<color=blue> false </color>");
            }
        }

        /// <summary>
        /// 入力された文字が適正であるかどうかの確認をし文字列を返す。
        /// </summary>
        /// <param name="inputText"> 入力されたテキスト </param>
        /// <returns></returns>
        protected string CheckForEnteredCharacters(string inputText)
        {
            Debug.Log($" CheckForEnteredCharacters input :<color=blue> {inputText}</color>");

            if (inputText == "")
            {
                inputText = UnityEngine.Random.Range(0, 10000).ToString();
                Debug.Log("<color=green> I defined a random name because it was not entered.</color>");
            }

            Debug.Log("CheckForEnteredCharacters :<color=green> clear </color>");

            return inputText;
        }

        /// <summary>
        /// ニックネームが同じ人をくべつする。
        /// </summary>
        /// <param name="nickName"> 確認したいニックネーム </param>
        /// <returns> 確認後のニックネーム </returns>
        protected string ConfirmationOfNicknames(string nickName)
        {
            if (PhotonNetwork.InRoom)
            {
                List<string> nickNameList = new List<string>(PhotonNetwork.PlayerList.Length);

                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    nickNameList.Add(player.NickName);
                }

                nickNameList.Remove(nickName);

                foreach (string nameList in nickNameList)
                {
                    if (nickName == nameList)
                    {
                        nickName = nickName + "(1)";
                        Debug.Log($" {nickName} = {nickName + "(1)"}<color=green> The same name existed, so I changed it.</color>");
                    }
                }
            }

            return nickName;
        }

        /// <summary>
        /// ニックネームが同じ人をくべつする。
        /// </summary>
        /// <remarks>
        /// 部屋の中にいるプレイヤーリストの中に自分と同じ名前があった場合自身の名前を変更します。
        /// </remarks>>
        /// <param name="photonView">　自身のニックネームが入ったPhotonView </param>
        protected void ConfirmationOfNicknames(PhotonView photonView)
        {
            if (PhotonNetwork.InRoom) 
            {
                List<string> nickNameList = new List<string>(PhotonNetwork.PlayerList.Length);

                foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                {
                    nickNameList.Add(player.NickName);
                }

                nickNameList.Remove(photonView.Owner.NickName);

                foreach (string nameList in nickNameList)
                {
                    if (photonView.Owner.NickName == nameList)
                    {
                        photonView.Owner.NickName = photonView.Owner.NickName + "(1)";
                        Debug.Log($" {photonView.Owner.NickName} = {photonView.Owner.NickName + "(1)"}<color=green> The same name existed, so I changed it.</color>");
                    }
                }

                Debug.Log("ConfirmationOfNicknames : <color=green>clear</color>");
            }
            else
            {
                Debug.LogError(" Please check your name in the room where your nickname is assigned. ");
            }
        }

        /// <summary>
        /// 自身が当たったオブジェクトの衝突方向を確認します。
        /// </summary>
        /// <returns> 接触方向を　string で返します。</returns>
        protected string ConfirmationOfCollisionDirection_string(Collision collision)
        {
            string direction = "";
            float standardValue = 0.05f;

            foreach (ContactPoint point in collision.contacts)
            {
                Vector3 relativePoint = transform.InverseTransformPoint(point.point);

                //if (relativePoint.y > 0.5)
                //{
                //    Debug.Log("Up");
                //    direction = Vector3.up;
                //}

                if (relativePoint.z >= standardValue)
                {
                    direction = "Forward";
                }
                else if (relativePoint.z <= -standardValue)
                {
                    direction = "Back";
                }

                if (relativePoint.x >= standardValue)
                {
                    direction = "Right";
                }
                else if (relativePoint.x <= -standardValue)
                {
                    direction = "Left";
                }
            }

            Debug.Log(direction);

            return direction;
        }


        /// <summary>
        /// 自身が当たったオブジェクトの衝突方向を確認します。
        /// </summary>
        /// <param name="collision"></param>
        /// <returns> 接触方向を　Vector3で返します。</returns>
        protected Vector3 ConfirmationOfCollisionDirection_vector3(Collision collision)
        {
            Vector3 direction = Vector3.zero;
            float standardValue = 0.05f;

            foreach ( ContactPoint point in collision.contacts)
            {
                Vector3 relativePoint = transform.InverseTransformPoint(point.point);

                //if (relativePoint.y > 0.5)
                //{
                //    Debug.Log("Up");
                //    direction = Vector3.up;
                //}

                if (relativePoint.z >= standardValue)
                {
                    direction = Vector3.forward;
                }
                else if (relativePoint.z <= -standardValue)
                {
                    direction = Vector3.back;
                }

                if (relativePoint.x >= standardValue)
                {
                    direction = Vector3.right;
                }
                else if (relativePoint.x <= -standardValue)
                {
                    direction = Vector3.left;
                }
            }

            return direction;
        }

        /// <summary>
        /// 二分の一の確率を返します。
        /// </summary>
        /// <returns></returns>
        protected bool ReturnsTheProbabilityOf1In2() { return UnityEngine.Random.Range(1, 10) % 2 == 0; }
        /// <summary>
        /// メニューの状態を変更します。
        /// </summary>
        /// <param name="menu"></param>
        protected void StateChangeOnMenu(GameObject menu) { menu.SetActive(!menu.activeSelf); }
        /// <summary>
        /// canvasの状態を変更します。
        /// </summary>
        /// <param name="canvas"></param>
        protected void StateChangeOnCanvas(Canvas canvas) { canvas.gameObject.SetActive(!canvas.gameObject.activeSelf); }

        #endregion

        #region PunCallbacks

        /// <summary>
        /// Photonから切断された時
        /// </summary>
        /// <param name="cause">　切断原因 </param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.Log($" OnDisconnected Cause :<color=green> {cause}</color>");
        }

        /// <summary>
        /// 地域リストを受け取った時
        /// </summary>
        /// <param name="regionHandler">
        /// 環境
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/v2/class_photon_1_1_realtime_1_1_region_handler.html"/>
        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            Debug.Log("OnRegionListReceived");
        }

        /// <summary>
        /// カスタム認証のレスポンスがあった時
        /// </summary>
        /// <param name="data">
        /// 
        /// </param>
        public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            Debug.Log("OnCustomAuthenticationResponse");
        }

        /// <summary>
        /// カスタム認証が失敗した時
        /// </summary>
        /// <param name="debugMessage">
        /// 認証が失敗した理由のデバッグ メッセージが含まれています。これは、開発中に修正する必要があります。
        /// </param>
        public override void OnCustomAuthenticationFailed(string debugMessage)
        {
            Debug.Log("OnCustomAuthenticationFailed");
            Debug.Log(debugMessage);
        }

        /// <summary>
        /// フレンドリストに更新があった時
        /// </summary>
        /// <param name="friendList">
        /// フレンドのオンライン状況と、どのRoomにいるのかという情報
        /// </param>
        /// <see cref="https://doc-api.photonengine.com/ja-jp/pun/current/class_friend_info.html"/>
        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            Debug.Log("OnFriendListUpdate");
        }

        /// <summary>
        /// フレンドのリストの部屋とオンライン ステータスをリクエストし、結果を PhotonNetwork.Friends に保存します。
        /// </summary>
        /// <param name="friendsUserIds">
        /// フレンドID
        /// </param>
        /// <returns></returns>
        public bool FindFriends(string[] friendsUserIds)
        {
            return PhotonNetwork.FindFriends(friendsUserIds);
        }

        /// <summary>
        /// フレンドリストの更新
        /// </summary>
        /// <param name="friendsInfo">
        /// フレンドのオンライン状況と、どのRoomにいるのかという情報
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
