using Photon.Voice.PUN.UtilityScripts;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TakechiEngine.PUN.ServerConnect.Joined;

using Takechi.CharacterController.Parameters;
using Takechi.ScriptReference.CustomPropertyKey;

namespace Takechi.CharacterSelection
{
    public class CharacterSelectionManagement : TakechiJoinedPunCallbacks
    {
        [SerializeField] private PlayableCharacterParametersManager m_parametersManager;

        [SerializeField] private Text m_infometionText;
        [SerializeField] private Text m_gameStartTimeCuntText;
        [SerializeField] private float  m_gameStartTimeCunt_seconds = 30;

        private bool m_isSelectedTime => m_gameStartTimeCunt_seconds > 0 ? true : false;

        private void Start()
        {
            setInformationText(0);
            setLocalPlayerCustomProperties( CustomPropertyKeyReference.s_CharacterStatusSelectedCharacter, 0);

            StartCoroutine(nameof(TimeUpdate));
            RoomInfoAndJoinedPlayerInfoDisplay(CustomPropertyKeyReference.s_RoomStatusKeys, CustomPropertyKeyReference.s_CharacterStatusKeys);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            /// フェイド処理などを入れても良き
            
        }

        private IEnumerator TimeUpdate()
        {
            while ( this.gameObject.activeSelf)
            {
                if (m_isSelectedTime)
                {
                    m_gameStartTimeCunt_seconds -= Time.deltaTime;
                    m_gameStartTimeCuntText.text = Mathf.Ceil(m_gameStartTimeCunt_seconds).ToString();
                    yield return null;
                }
                else
                {
                    SceneSyncChange(3);
                    this.gameObject.gameObject.SetActive(false);
                }
            }
        }

        #region event system finction
        public void OnSelected(int num)
        {
            setInformationText(num);
            setLocalPlayerCustomProperties(CustomPropertyKeyReference.s_CharacterStatusSelectedCharacter, num);

           RoomInfoAndJoinedPlayerInfoDisplay(CustomPropertyKeyReference.s_RoomStatusKeys, CustomPropertyKeyReference.s_CharacterStatusKeys);
        }

        #endregion

        private void setInformationText(int num)
        {
            m_infometionText.text = m_parametersManager.GetParameters(num).GetInformation();
        }

        private void setInformationText(string name)
        {
            m_infometionText.text = m_parametersManager.GetParameters(name).GetInformation();
        }

    }
}
