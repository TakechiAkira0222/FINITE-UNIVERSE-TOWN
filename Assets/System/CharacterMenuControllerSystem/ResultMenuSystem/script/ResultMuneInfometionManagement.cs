using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Takechi.CharacterController.Parameters;
using Takechi.PlayableCharacter.FadingCanvas;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.RoomStatus;

namespace Takechi.UI.ResultMune
{
    public class ResultMuneInfometionManagement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== Script Setting ===")]
        [SerializeField, NamedArrayAttribute(new string[] { "TeamA", "TeamB" })] private Text[] m_scoreDisplayTextArray = new Text[2];
        [SerializeField] private Text m_victoryOrLoseText;

        #endregion


        #region private variable
        /// <summary>
        /// character addressManagement
        /// </summary>
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private RoomStatusManagement roomStatusManagement => addressManagement.GetMyRoomStatusManagement();
        private Text  victoryOrLoseText => m_victoryOrLoseText;
        private float victory_f => roomStatusManagement.GetVictoryPoint();

        #endregion

        #region unity event 
        private void Start()
        {
            if (!statusManagement.GetIsLocal()) return;

            setScoreDisplayText();
            setVictoryOrLoseText();
        }

        #endregion

        private void setVictoryOrLoseText()
        {
            if (statusManagement.GetCustomPropertiesTeamName() == roomStatusManagement.GetVictoryingTeamName())
            {
                victoryOrLoseText.text = "Victory";
            }
            else { victoryOrLoseText.text = "Lose"; }
        }

        private void setScoreDisplayText()
        {
            float aPoint_f, bPoint_f;

            if (roomStatusManagement.GetIsHardPoint())
            {
                aPoint_f = roomStatusManagement.GetTeamAPoint_hardPoint();
                bPoint_f = roomStatusManagement.GetTeamBPoint_hardPoint();
            }
            else if (roomStatusManagement.GetIsDomination())
            {
                aPoint_f = roomStatusManagement.GetTeamAPoint_domination();
                bPoint_f = roomStatusManagement.GetTeamBPoint_domination();
            }
            else
            {
                aPoint_f = -1;
                bPoint_f = -1;
            }

            float percentageOfTeamAPoints = Mathf.Ceil((aPoint_f / victory_f) * 1000);
            float percentageOfTeamBPoints = Mathf.Ceil((bPoint_f / victory_f) * 1000);

            m_scoreDisplayTextArray[0].text = Mathf.Clamp((percentageOfTeamAPoints / 10), 0, 100).ToString();
            m_scoreDisplayTextArray[1].text = Mathf.Clamp((percentageOfTeamBPoints / 10), 0, 100).ToString();
        }
    }
}