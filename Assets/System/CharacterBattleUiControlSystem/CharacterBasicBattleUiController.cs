using System.Collections;
using System.Collections.Generic;
using System.Data;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.BattleUi
{
    public class CharacterBasicBattleUiController : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [SerializeField] private Slider m_deathblowSlider;
        [SerializeField] private Slider m_massSlider;
        [SerializeField] private Slider m_ability1Slider;
        [SerializeField] private Slider m_ability2Slider;
        [SerializeField] private Slider m_ability3Slider;

        #endregion

        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private Slider deathblowSlider => m_deathblowSlider;

        private Slider massSlider => m_massSlider; 
        private Slider ability1Slider => m_ability1Slider;
        private Slider ability2Slider => m_ability2Slider;
        private Slider ability3Slider => m_ability3Slider;


        private void Awake()
        {
            setValue( massSlider, characterStatusManagement.GetMyRigidbody().mass /characterStatusManagement.GetCleanMass());
            Debug.Log($" massSlider.value <color=blue>to set</color>.");
            setValue(deathblowSlider, 0);
            Debug.Log($" deathblowSlider.value <color=blue>to set</color>.");
            setValue(ability1Slider, 0);
            Debug.Log($" ability1Slider.value <color=blue>to set</color>.");
            setValue(ability2Slider, 0);
            Debug.Log($" ability2Slider.value <color=blue>to set</color>.");
            setValue(ability3Slider, 0);
            Debug.Log($" ability3Slider.value <color=blue>to set</color>.");
        }
        private void Update()
        {
            updateValue(massSlider, characterStatusManagement.GetMyRigidbody().mass, characterStatusManagement.GetCleanMass());
            updateValue(deathblowSlider, characterStatusManagement.GetCanUseDeathblow_TimeCount_Seconds() , characterStatusManagement.GetCanUseDeathblow_RecoveryTime_Seconds());
            updateValue(ability1Slider,  characterStatusManagement.GetCanUseAbility1_TimeCount_Seconds() ,  characterStatusManagement.GetCanUseAbility1_RecoveryTime_Seconds());
            updateValue(ability2Slider,  characterStatusManagement.GetCanUseAbility2_TimeCount_Seconds() ,  characterStatusManagement.GetCanUseAbility2_RecoveryTime_Seconds());
            updateValue(ability3Slider,  characterStatusManagement.GetCanUseAbility3_TimeCount_Seconds() ,  characterStatusManagement.GetCanUseAbility3_RecoveryTime_Seconds());
        }

        void setValue( Slider slider, float value)
        {
            slider.value = value;
        }

        void updateValue( Slider slider, float value, float max)
        {
            slider.value =  value / max;
        }
    }
}
