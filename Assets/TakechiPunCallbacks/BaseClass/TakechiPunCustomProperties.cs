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
        /// �j�b�N�l�[����t����
        /// </summary>
        /// <param name="nickName"> �ݒ肵���������̖��O </param>
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
        /// �����̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <param name="customRoomProperties"> �X�V��������Ԃ̃v���p�e�B�[ </param>
        protected void UpdateRoomCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
                Debug.Log($" customRoomProperties : <color=green>setCustomProperties.</color>");
            }
        }
        /// <summary>
        /// �����̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <remarks>�@�����̃J�X�^���v���p�e�B�[�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
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
        /// �����̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <remarks>�@�����̃J�X�^���v���p�e�B�[�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="customRoomProperties">�@���݂̃v���p�e�B�[ </param>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
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
        /// ���g�̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <param name="customRoomProperties"> �X�V��������Ԃ̃v���p�e�B�[ </param>
        protected void UpdateLocalPlayerProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(customRoomProperties);
                Debug.Log($" customPlayerProperties : <color=green>setCustomProperties.</color>");
            }
        }
        /// <summary>
        /// ���g�̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <remarks>�@���g�̃J�X�^���v���p�e�B�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
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
        /// ���g�̃J�X�^���v���p�e�B���X�V����
        /// </summary>
        /// <remarks>�@���g�̃J�X�^���v���p�e�B�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="customPlayerProperties">�@���݂̃v���p�e�B </param>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
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
