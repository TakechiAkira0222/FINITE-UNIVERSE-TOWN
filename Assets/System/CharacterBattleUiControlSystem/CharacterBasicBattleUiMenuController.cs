using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.UI.SliderContlloer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.UI.BattleUiMenu
{
    public class CharacterBasicBattleUiMenuController : SliderContlloerManager
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== ScriptSetting===")]
        [SerializeField] private Slider m_deathblowSlider;
        [SerializeField] private Slider m_massSlider;
        [SerializeField] private Slider m_ability1Slider;
        [SerializeField] private Slider m_ability2Slider;
        [SerializeField] private Slider m_ability3Slider;
        [Header(" === Exclusive Ui ===")]
        [SerializeField] private GameObject m_hardpointGameSystemUi;
        [SerializeField] private GameObject m_dominationGameSystemUi;

        #endregion

        #region private variable
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private Slider deathblowSlider => m_deathblowSlider;
        private Slider massSlider =>     m_massSlider;
        private Slider ability1Slider => m_ability1Slider;
        private Slider ability2Slider => m_ability2Slider;
        private Slider ability3Slider => m_ability3Slider;
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

        private Dictionary<string, Action> m_storageActionDictionary = new Dictionary<string, Action>();


        private void Start()
        {
            setValue(massSlider, statusManagement.GetMass() / statusManagement.GetCleanMass());
            Debug.Log($" massSlider.value <color=blue>to set</color>.");
            setValue(deathblowSlider, 0);
            Debug.Log($" deathblowSlider.value <color=blue>to set</color>.");
            setValue(ability1Slider, 0);
            Debug.Log($" ability1Slider.value <color=blue>to set</color>.");
            setValue(ability2Slider, 0);
            Debug.Log($" ability2Slider.value <color=blue>to set</color>.");
            setValue(ability3Slider, 0);
            Debug.Log($" ability3Slider.value <color=blue>to set</color>.");

            StorageAction storageAction = new StorageAction( m_hardpointGameSystemUi, m_dominationGameSystemUi);

            m_storageActionDictionary.Add( RoomStatusName.GameType.hardpoint, storageAction.hardpointAction);
            m_storageActionDictionary.Add( RoomStatusName.GameType.domination, storageAction.dominationAction);

            m_storageActionDictionary[(string)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.gameTypeKey]]();
        }

        private void Update()
        {
            updateValue(massSlider, statusManagement.GetMass(), statusManagement.GetCleanMass());
            updateValue(deathblowSlider, statusManagement.GetCanUseDeathblow_TimeCount_Seconds() , statusManagement.GetCanUseDeathblow_RecoveryTime_Seconds());
            updateValue(ability1Slider, statusManagement.GetCanUseAbility1_TimeCount_Seconds() , statusManagement.GetCanUseAbility1_RecoveryTime_Seconds());
            updateValue(ability2Slider, statusManagement.GetCanUseAbility2_TimeCount_Seconds() , statusManagement.GetCanUseAbility2_RecoveryTime_Seconds());
            updateValue(ability3Slider, statusManagement.GetCanUseAbility3_TimeCount_Seconds() , statusManagement.GetCanUseAbility3_RecoveryTime_Seconds());
        }
    }
}
