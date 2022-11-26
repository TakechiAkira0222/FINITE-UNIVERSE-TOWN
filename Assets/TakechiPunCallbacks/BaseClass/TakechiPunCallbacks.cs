using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TakechiEngine.PUN
{
    public class TakechiPunCallbacks : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// クライアントの場合表示する。
        /// </summary>
        /// <param name="obj"> 管理したいオブジェクト </param>
        protected void ShowClientsOnly(GameObject obj)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                obj.SetActive(true);
                Debug.Log($" ShowClientsOnly { obj.name}.SetActive :<color=blue> true </color>");
            }
            else
            {
                obj.SetActive(false);
                Debug.Log($" ShowClientsOnly { obj.name}.SetActive :<color=blue> false </color>");
            }
        }

        #region networkPropertyChecker

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
                inputText = UnityEngine.Random.Range( 0, 10000).ToString();
                Debug.Log("<color=green> I defined a random name because it was not entered.</color>");
            }

            Debug.Log("CheckForEnteredCharacters :<color=green> clear </color>");

            return inputText;
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

        #endregion

        /// <summary>
        /// 自身が当たったオブジェクトの衝突方向を確認します。
        /// </summary>
        /// <returns> 接触方向を　string で返します。</returns>
        protected string ConfirmationOfCollisionDirection_string( Collision collision)
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

            Debug.Log( direction);

            return direction;
        }

        /// <summary>
        /// 自身が当たったオブジェクトの衝突方向を確認します。
        /// </summary>
        /// <param name="collision"></param>
        /// <returns> 接触方向を　Vector3で返します。</returns>
        protected Vector3 ConfirmationOfCollisionDirection_vector3( Collision collision)
        {
            Vector3 direction = Vector3.zero;
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
    }
}
