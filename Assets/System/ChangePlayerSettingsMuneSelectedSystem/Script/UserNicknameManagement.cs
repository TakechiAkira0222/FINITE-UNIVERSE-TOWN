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

namespace Takechi.UI.UserNickname
{
    public class SaveData
    {
        public string nickname;
    }

    public class UserNicknameManagement : ExternalDataManagement
    {
        private SaveData m_saveData = new SaveData();
        private string m_nickname = "NoName";
        private string m_cleanpath => Application.persistentDataPath + "/saveData.bytes";

        public string GetNickName() { return m_nickname; }
        public void SetNickname(string setName) { m_nickname = setName; }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.K))
            {
                m_saveData.nickname = GetNickName();
                OnDataSave();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log(OnDataLoad().nickname);
            }
        }

        public void OnDataSave() { DataSave( m_saveData, m_cleanpath); }

        private SaveData OnDataLoad() { return SetAndLoadData(m_saveData, m_cleanpath); }
    }
}
