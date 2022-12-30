using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using System.IO;
using System.Security.Cryptography;
using System.Text;

using System.Data;
using UnityEditor;
using UnityEngine.Playables;
using Takechi.ExternalData;
using UnityEngine.UI;
using ExitGames.Client.Photon;

namespace Takechi.UI.UserNickname
{
    public class NickNameData
    {
        public string nickName;
    }

    public class UserNickNameManagement : ExternalDataManagement
    {
        [SerializeField] private InputField m_userNameInputField;
        [SerializeField] private Text       m_displayUserNameText;

        private string m_cleanpath => Application.persistentDataPath + "/nickNameData.bytes";

        private NickNameData m_nickNameData = new NickNameData();

        private void OnEnable()
        {
            GetNickNameData();

            m_displayUserNameText.text = m_nickNameData.nickName;
            m_userNameInputField.text  = m_nickNameData.nickName;

            Debug.Log(" displayUserNameText <color=green>to set</color>.");
            Debug.Log(" userNameInputField <color=green>to set</color>.");
        }

        public NickNameData GetNickNameData()
        {
            OnDataLoad();
            Debug.Log(" UserNickNameManagement.<color=yellow>OnDataLoad</color>()");

            if ( m_nickNameData == null)
            {
                m_nickNameData.nickName = "Player";

                OnDataSave();

                Debug.Log("I <color=green>created the username</color> file because it doesn't exist.");
            }

            return m_nickNameData;
        }

        public void OnChangeDisplayText()
        {
            m_displayUserNameText.text = m_userNameInputField.text;
            m_nickNameData.nickName = m_userNameInputField.text;

            Debug.Log(" displayUserNameText <color=green>to set</color>.");
            Debug.Log(" nickNameData.nickName <color=green>to set</color>.");
        }

        public void OnDataSave() { DataSave( m_nickNameData, m_cleanpath); }

        private void OnDataLoad() { m_nickNameData = LoadData( m_nickNameData, m_cleanpath); }
    }
}
