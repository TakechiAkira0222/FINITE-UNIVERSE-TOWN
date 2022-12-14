using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TakechiEngine.PUN.CustomProperties
{
    public class TakechiPunCustomProperties : TakechiPunCallbacks
    {
        #region setPropertiesFunction
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

        #endregion

        #region UpdateCustomRoomProperties
        /// <summary>
        /// CurrentRoomCustomProperties to set.
        /// </summary>
        /// <param name="customRoomProperties"> �X�V��������Ԃ̃v���p�e�B�[ </param>
        protected void setCurrentRoomCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
                Debug.Log($" setCurrentRoomCustomProperties : <color=green> current room custom properties to set.</color>");
            }
        }
        /// <summary>
        /// CurrentRoomCustomProperties to set.
        /// </summary>
        /// <remarks>�@�����̃J�X�^���v���p�e�B�[�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
        protected void setCurrentRoomCustomProperties(string key, object o)
        {
            if ( PhotonNetwork.InRoom)
            {
                object temp = null;
                ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable();

                if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(key, out temp))
                {
                    hastable[key] = o;
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    hastable.Add(key, o);
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.CurrentRoom.SetCustomProperties(hastable);
            }
        }
        /// <summary>
        /// CurrentRoomCustomProperties to set.
        /// </summary>
        /// <remarks>�@�����̃J�X�^���v���p�e�B�[�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="customRoomProperties">�@���݂̃v���p�e�B�[ </param>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
        protected void setCurrentRoomCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties, string key, object o)
        {
            if (PhotonNetwork.InRoom)
            {
                object temp = null;
                if (customRoomProperties.TryGetValue(key, out temp))
                {
                    customRoomProperties[key] = o;
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    customRoomProperties.Add(key, o);
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.CurrentRoom.SetCustomProperties(customRoomProperties);
            }
        }

        #endregion

        #region setLocalPlayerCustomProperties

        /// <summary>
        /// LocalPlayerCustomProperties to set.
        /// </summary>
        /// <param name="customRoomProperties"> �X�V��������Ԃ̃v���p�e�B�[ </param>
        protected void setLocalPlayerCustomProperties(ExitGames.Client.Photon.Hashtable customRoomProperties)
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(customRoomProperties);
                Debug.Log($" setLocalPlayerCustomProperties : <color=green> local player custom properties to set.</color>");
            }
        }
        /// <summary>
        /// LocalPlayerCustomProperties to set.
        /// </summary>
        /// <remarks>�@���g�̃J�X�^���v���p�e�B�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
        protected void setLocalPlayerCustomProperties(string key, object o)
        {
            if (PhotonNetwork.IsConnected)
            {
                object temp = null;
                ExitGames.Client.Photon.Hashtable hastable = new ExitGames.Client.Photon.Hashtable();

                if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(key, out temp))
                {
                    hastable[key] = o;
                    Debug.Log($" setLocalPlayerCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    hastable.Add(key, o);
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.LocalPlayer.SetCustomProperties(hastable);
            }
        }
        /// <summary>
        /// LocalPlayerCustomProperties to set.
        /// </summary>
        /// <remarks>�@���g�̃J�X�^���v���p�e�B�ɑ��݂��邩�ǂ��������ĒǋL�������͏����������s���܂��B�@</remarks>>
        /// <param name="customPlayerProperties">�@���݂̃v���p�e�B </param>
        /// <param name="key"> �v���p�e�B�[�̌��@</param>
        /// <param name="o">�@�ύX���e�̃I�u�W�F�N�g�^ </param>
        protected void setLocalPlayerCustomProperties(ExitGames.Client.Photon.Hashtable customPlayerProperties, string key, object o)
        {
            if (PhotonNetwork.InRoom)
            {
                object temp = null;
                if (customPlayerProperties.TryGetValue(key, out temp))
                {
                    customPlayerProperties[key] = o;
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to set.</color>");
                }
                else
                {
                    customPlayerProperties.Add(key, o);
                    Debug.Log($" setCurrentRoomCustomProperties[<color=orange>{key}</color>] : {o} <color=green>to add.</color>");
                }

                PhotonNetwork.LocalPlayer.SetCustomProperties(customPlayerProperties);
            }
        }

        #endregion

    }
}
