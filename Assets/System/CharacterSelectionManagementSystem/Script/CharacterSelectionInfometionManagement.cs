using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Takechi.CharacterController.Information;
using Takechi.CharacterController.Parameters;
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
        [Header("=== PlayableCharacterParametersManager ===")]
        [SerializeField] private PlayableCharacterParametersManager  m_playableCharacterParametersManager;
        [Header("=== Script Setting ===")]
        [SerializeField] private Image m_featuresTypeIcon;
        [SerializeField] private Image m_attackTypeIcon;
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3"})]      private Text[]  m_nameTextArray = new Text[5];
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3" })]     private Text[]  m_explanationTextArray = new Text[5];
        [SerializeField, NamedArrayAttribute(new string[] { "character", "deathblow", "ability1", "ability2", "ability3" })]     private Image[] m_iconArray = new Image[5];
        [SerializeField, NamedArrayAttribute(new string[] { "mass", "attackPower", "movingSpeed", "jumpPower", "RespawnTime" })] private Text[]  m_parameterTextArray = new Text[5];
        [SerializeField, NamedArrayAttribute(new string[] { "deathblow", "ability1", "ability2", "ability3" })]                  private Text[]  m_recoveryTimeArray = new Text[4];
      
        private PlayableCharacterInformationManager informationManager => m_playableCharacterInformationManager;
        private PlayableCharacterParametersManager  parametersManager  => m_playableCharacterParametersManager;

        #region set variable

        private void setFeaturesTypeIcon(int num) { m_featuresTypeIcon.sprite = informationManager.GetInformation(num).GetCharacterFeaturesIcon(); }
        private void setAttackTypeIcon(int num) { m_attackTypeIcon.sprite = informationManager.GetInformation(num).GetCharacterAttackTypeIcon(); }
        private void setParameterText(int num)
        {
            m_parameterTextArray[0].text = parametersManager.GetParameters(num).GetCleanMass().ToString();
            m_parameterTextArray[1].text = parametersManager.GetParameters(num).GetAttackPower().ToString();
            m_parameterTextArray[2].text = parametersManager.GetParameters(num).GetSpeed().ToString();
            m_parameterTextArray[3].text = parametersManager.GetParameters(num).GetJumpPower().ToString();
            m_parameterTextArray[4].text = parametersManager.GetParameters(num).GetRespawnTime_Seconds().ToString();
        }
        private void setRecoveryTimeText(int num)
        {
            m_recoveryTimeArray[0].text = parametersManager.GetParameters(num).GetDeathblow_RecoveryTime_Seconds().ToString();
            m_recoveryTimeArray[1].text = parametersManager.GetParameters(num).GetAbility1_RecoveryTime_Seconds().ToString();
            m_recoveryTimeArray[2].text = parametersManager.GetParameters(num).GetAbility2_RecoveryTime_Seconds().ToString();
            m_recoveryTimeArray[3].text = parametersManager.GetParameters(num).GetAbility3_RecoveryTime_Seconds().ToString();
        }
        private void setCharacterInformation(int num)
        {
            m_nameTextArray[0].text = informationManager.GetInformation(num).GetCharacterName();
            m_iconArray[0].sprite   = informationManager.GetInformation(num).GetCharacterIcon();
            m_explanationTextArray[0].text = informationManager.GetInformation(num).GetCharacterExplanation();
        }
        private void setdeathblowInfomation(int num)
        {
            m_nameTextArray[1].text = informationManager.GetInformation(num).GetdeathblowName();
            m_iconArray[1].sprite   = informationManager.GetInformation(num).GetdeathblowIcon();
            m_explanationTextArray[1].text = informationManager.GetInformation(num).GetdeathblowExplanation();
        }
        private void setAbility1Information(int num)
        {
            m_nameTextArray[2].text = informationManager.GetInformation(num).GetAbility1Name();
            m_iconArray[2].sprite   = informationManager.GetInformation(num).GetAbility1Icon();
            m_explanationTextArray[2].text = informationManager.GetInformation(num).GetAbility1Explanation();
        }
        private void setAbility2Information(int num)
        {
            m_nameTextArray[3].text = informationManager.GetInformation(num).GetAbility2Name();
            m_iconArray[3].sprite   = informationManager.GetInformation(num).GetAbility2Icon();
            m_explanationTextArray[3].text = informationManager.GetInformation(num).GetAbility2Explanation();
        }
        private void setAbility3Information(int num)
        {
            m_nameTextArray[4].text = informationManager.GetInformation(num).GetAbility3Name();
            m_iconArray[4].sprite   = informationManager.GetInformation(num).GetAbility3Icon();
            m_explanationTextArray[4].text = informationManager.GetInformation(num).GetAbility3Explanation();
        }

        #endregion

        #region unity event
        private void Start()
        {
            setFeaturesTypeIcon(0);
            setAttackTypeIcon(0);
            setRecoveryTimeText(0);
            setCharacterInformation(0);
            setdeathblowInfomation(0);
            setAbility1Information(0);
            setAbility2Information(0);
            setAbility3Information(0);
            setParameterText(0);
        }

        #endregion

        #region event system finction
        public void OnSelected(int num)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            setFeaturesTypeIcon(num);
            setAttackTypeIcon(num);
            setRecoveryTimeText(num);
            setCharacterInformation(num);
            setdeathblowInfomation(num);
            setAbility1Information(num);
            setAbility2Information(num);
            setAbility3Information(num);
            setParameterText(num);
        }

        #endregion
    }
}