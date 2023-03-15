using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Information;
using Takechi.CharacterController.Parameters;
using Takechi.UI.SliderContlloer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.UI.BattleCanvasUi
{
    public class CharacterBasicBattleCanvasUiController : SliderContlloerManager
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement   m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement    m_characterStatusManagement;
        [Header("=== ScriptSetting===")]
        [SerializeField] private Slider m_deathblowSlider;
        [SerializeField] private Slider m_massSlider;
        [SerializeField] private Image  m_deathblowFillImage;
        [SerializeField, NamedArrayAttribute(new string[] { "ability1", "ability2", "ability3" })] private Slider[] m_abilitySliderArray = new Slider[3];
        [SerializeField, NamedArrayAttribute(new string[] { "ability1", "ability2", "ability3" })] private Image[]  m_abilityiconArray = new Image[3];
        [Header(" === Exclusive Ui ===")]
        [SerializeField] private GameObject m_hardpointGameSystemUi;
        [SerializeField] private GameObject m_dominationGameSystemUi;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        PlayableCharacterInformation   characterInformation => addressManagement.GetCharacterInformation();
        private Slider deathblowSlider => m_deathblowSlider;
        private Slider massSlider      => m_massSlider;
        private Image deathblowFillImage => m_deathblowFillImage;
        private Slider[] abilitySliderArray => m_abilitySliderArray;
        private Image[]  abilityiconArray   => m_abilityiconArray;
        
        #endregion

        public class StorageAction
        {
            public Action hardpointAction  = delegate { };
            public Action dominationAction = delegate { };
            public StorageAction( GameObject hardpointUi, GameObject dominationUi)
            {
                hardpointAction += () =>
                {
                    hardpointUi.gameObject.SetActive(true);
                    dominationUi.gameObject.SetActive(false);
                };

                dominationAction += () =>
                {
                    hardpointUi.gameObject.SetActive(false);
                    dominationUi.gameObject.SetActive(true);
                };
            }
        }

        public class OriginalColor
        {
            public readonly Color32 notAvailableAbilityDeathblowColor = new Color( 0, 255, 255, 0.3f); 
            public readonly Color32 notAvailableAbilityColor = new Color( 255, 255, 255, 0.3f); 

            public readonly Color32 usableColor = new Color( 255, 255, 0, 0.5f); 
        }

        private OriginalColor originalColor = new OriginalColor();

        private Dictionary<string, Action> m_storageActionDictionary = new Dictionary<string, Action>();

        #region setup function
        private void setUpSlider()
        {
            setValue(massSlider, statusManagement.GetMass() / statusManagement.GetCleanMass());
            Debug.Log($" massSlider.value <color=blue>to set</color>.");
            setValue(deathblowSlider, 0);
            Debug.Log($" deathblowSlider.value <color=blue>to set</color>.");

            foreach (Slider slider in abilitySliderArray)
            {
                setValue(slider, 0);
                Debug.Log($" {slider.name}.value <color=blue>to set</color>.");
            }
        }

        #endregion

        #region set function
        private void setAbilityiconArray()
        {
            abilityiconArray[0].sprite = characterInformation.GetAbility1Icon();
            abilityiconArray[1].sprite = characterInformation.GetAbility2Icon();
            abilityiconArray[2].sprite = characterInformation.GetAbility3Icon();
            Debug.Log($" setAbilityiconArray.sprite to set.");
        }

        #endregion

        #region update variable function

        private void updateSliderValue()
        {
            updateValue(massSlider, statusManagement.GetMass(), statusManagement.GetCleanMass());

            updateValue(deathblowSlider, statusManagement.GetCanUseDeathblow_TimeCount_Seconds(), statusManagement.GetCanUseDeathblow_RecoveryTime_Seconds());
            updateChangeOnColor(statusManagement.GetCanUseDeathblow(), deathblowFillImage, originalColor.notAvailableAbilityDeathblowColor);

            updateValue(abilitySliderArray[0], statusManagement.GetCanUseAbility1_TimeCount_Seconds(), statusManagement.GetCanUseAbility1_RecoveryTime_Seconds());
            updateChangeOnColor(statusManagement.GetCanUseAbility1(), abilityiconArray[0], originalColor.notAvailableAbilityColor);

            updateValue(abilitySliderArray[1], statusManagement.GetCanUseAbility2_TimeCount_Seconds(), statusManagement.GetCanUseAbility2_RecoveryTime_Seconds());
            updateChangeOnColor(statusManagement.GetCanUseAbility2(), abilityiconArray[1], originalColor.notAvailableAbilityColor);

            updateValue(abilitySliderArray[2], statusManagement.GetCanUseAbility3_TimeCount_Seconds(), statusManagement.GetCanUseAbility3_RecoveryTime_Seconds());
            updateChangeOnColor(statusManagement.GetCanUseAbility3(), abilityiconArray[2], originalColor.notAvailableAbilityColor);
        }

        private void updateChangeOnColor(bool flag, Image image, Color32 notAvailableColor)
        {
            if (flag) image.color = originalColor.usableColor;
            else image.color = notAvailableColor;
        }
        #endregion

        #region unity evenet
        private void Start()
        {
            setUpSlider();

            setAbilityiconArray();

            StorageAction storageAction = new StorageAction( m_hardpointGameSystemUi, m_dominationGameSystemUi);

            m_storageActionDictionary.Add( RoomStatusName.GameType.hardpoint, storageAction.hardpointAction);
            m_storageActionDictionary.Add( RoomStatusName.GameType.domination, storageAction.dominationAction);

            m_storageActionDictionary[(string)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.gameTypeKey]]();
        }

        private void Update()
        {
            updateSliderValue();
        }

        #endregion
    }
}
