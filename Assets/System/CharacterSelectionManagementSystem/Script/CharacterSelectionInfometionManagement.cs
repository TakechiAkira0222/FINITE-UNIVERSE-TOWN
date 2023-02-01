using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Information;
using Takechi.CharacterSelection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.CharacterSelection
{
    public class InformationData
    {
        public string name = "null";
        public Sprite icon = null;
        public string explanation = "null";
    }

    public class CharacterSelectionInfometionManagement : MonoBehaviour
    {

        [Header("=== PlayableCharacterInformationManager ===")]
        [SerializeField] private PlayableCharacterInformationManager m_playableCharacterInformationManager;
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3"})]  private Text[]  m_nameTextArray = new Text[5];
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3" })] private Text[]  m_explanationTextArray = new Text[5];
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3" })] private Image[] m_iconArray = new Image[5];
      
        private PlayableCharacterInformationManager informationManager => m_playableCharacterInformationManager;

        #region set variable
        private void setCharacterInformation(int num)
        {
            m_nameTextArray[0].text = informationManager.GetInformation(num).GetCharacterName();
            m_iconArray[0].sprite = informationManager.GetInformation(num).GetCharacterIcon();
            m_explanationTextArray[0].text = informationManager.GetInformation(num).GetCharacterExplanation();
        }
        private void setdeathblowInfomation(int num)
        {
            m_nameTextArray[1].text = informationManager.GetInformation(num).GetdeathblowName();
            m_iconArray[1].sprite = informationManager.GetInformation(num).GetdeathblowIcon();
            m_explanationTextArray[1].text = informationManager.GetInformation(num).GetdeathblowExplanation();
        }
        private void setAbility1Information(int num)
        {
            m_nameTextArray[2].text = informationManager.GetInformation(num).GetAbility1Name();
            m_iconArray[2].sprite = informationManager.GetInformation(num).GetAbility1Icon();
            m_explanationTextArray[2].text = informationManager.GetInformation(num).GetAbility1Explanation();
        }
        private void setAbility2Information(int num)
        {
            m_nameTextArray[3].text = informationManager.GetInformation(num).GetAbility2Name();
            m_iconArray[3].sprite = informationManager.GetInformation(num).GetAbility2Icon();
            m_explanationTextArray[3].text = informationManager.GetInformation(num).GetAbility2Explanation();
        }
        private void setAbility3Information(int num)
        {
            m_nameTextArray[4].text = informationManager.GetInformation(num).GetAbility3Name();
            m_iconArray[4].sprite = informationManager.GetInformation(num).GetAbility3Icon();
            m_explanationTextArray[4].text = informationManager.GetInformation(num).GetAbility3Explanation();
        }

        #endregion

        #region unity event
        private void Start()
        {
            setCharacterInformation(0);
            setdeathblowInfomation(0);
            setAbility1Information(0);
            setAbility2Information(0);
            setAbility3Information(0);
        }

        #endregion

        #region event system finction
        public void OnSelected(int num)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            setCharacterInformation(num);
            setdeathblowInfomation(num);
            setAbility1Information(num);
            setAbility2Information(num);
            setAbility3Information(num);
        }

        #endregion
    }
}