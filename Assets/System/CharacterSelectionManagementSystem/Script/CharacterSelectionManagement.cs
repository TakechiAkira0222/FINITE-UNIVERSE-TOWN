using Photon.Voice.PUN.UtilityScripts;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TakechiEngine.PUN.ServerConnect.Joined;
using Takechi.CharacterController.Parameters;

namespace Takechi.CharacterSelection
{
    public class CharacterSelectionManagement : TakechiJoinedPunCallbacks
    {
        [SerializeField] private PlayableCharacterParametersManager m_parametersManager;
        [SerializeField] private Text text;

        private int m_selectNum = 0;

        private void Start()
        {
            setInformationText(m_selectNum);
        }

        public int GetSelectNum() { return m_selectNum; }

        public void OnSelected(int num)
        {
            setInformationText(num);
        }

        private void setInformationText(int num)
        {
            text.text = m_parametersManager.GetParameters(num).GetInformation();
        }

        private void setInformationText(string name)
        {
            text.text = m_parametersManager.GetParameters(name).GetInformation();
        }
    }
}
