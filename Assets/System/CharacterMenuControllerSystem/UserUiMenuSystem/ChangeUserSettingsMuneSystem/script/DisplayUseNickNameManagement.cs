using System.Collections;
using System.Collections.Generic;
using Takechi.ExternalData;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CryptoRedPath.ReferenceCryptoRedPath;

namespace Takechi.UI.UserNickname
{
    public class DisplayUseNickNameManagement : ExternalDataManagement
    {
        [SerializeField] private Text m_displayUserNameText;

        private string m_cleanpath => Cleanpath.nickNameDataPath;
        private NickNameData m_nickNameData = new NickNameData();

        private void OnEnable()
        {
            GetNickNameData();

            m_displayUserNameText.text = m_nickNameData.nickName;

            Debug.Log(" displayUserNameText <color=green>to set</color>.");
            Debug.Log(" userNameInputField <color=green>to set</color>.");
        }

        public NickNameData GetNickNameData()
        {
            OnDataLoad();
            Debug.Log(" UserNickNameManagement.<color=yellow>OnDataLoad</color>()");

            OnDataSave();
            return m_nickNameData;
        }

        public void OnDataSave() { DataSave(m_nickNameData, m_cleanpath); }

        private void OnDataLoad() { m_nickNameData = LoadData(m_nickNameData, m_cleanpath); }
    }
}
