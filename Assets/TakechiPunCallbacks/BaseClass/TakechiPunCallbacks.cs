using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace TakechiEngine.PUN
{
    public class TakechiPunCallbacks : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// �N���C�A���g�̏ꍇ�\������B
        /// </summary>
        /// <param name="obj"> �Ǘ��������I�u�W�F�N�g </param>
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
        /// ���͂��ꂽ�������K���ł��邩�ǂ����̊m�F�����������Ԃ��B
        /// </summary>
        /// <param name="inputText"> ���͂��ꂽ�e�L�X�g </param>
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
        /// �j�b�N�l�[���������l�����ׂ���B
        /// </summary>
        /// <remarks>
        /// �����̒��ɂ���v���C���[���X�g�̒��Ɏ����Ɠ������O���������ꍇ���g�̖��O��ύX���܂��B
        /// </remarks>>
        /// <param name="photonView">�@���g�̃j�b�N�l�[����������PhotonView </param>
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
        /// ���g�����������I�u�W�F�N�g�̏Փ˕������m�F���܂��B
        /// </summary>
        /// <returns> �ڐG�������@string �ŕԂ��܂��B</returns>
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
        /// ���g�����������I�u�W�F�N�g�̏Փ˕������m�F���܂��B
        /// </summary>
        /// <param name="collision"></param>
        /// <returns> �ڐG�������@Vector3�ŕԂ��܂��B</returns>
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
