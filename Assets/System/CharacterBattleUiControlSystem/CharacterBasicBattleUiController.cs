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
        [SerializeField] private Slider m_ability1Slider;
        [SerializeField] private Slider m_ability2Slider;
        [SerializeField] private Slider m_ability3Slider;

        #endregion

        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private Slider deathblowSlider => m_deathblowSlider;
        private Slider ability1Slider => m_ability1Slider;
        private Slider ability2Slider => m_ability2Slider;
        private Slider ability3Slider => m_ability3Slider;


        private void Awake()
        {
            setValue(deathblowSlider, 0);
            setValue(ability1Slider, 0);
            setValue(ability2Slider, 0);
            setValue(ability3Slider, 0);
        }
        private void Update()
        {
            Debug.Log(m_characterStatusManagement.GetCanUseDeathblow_TimeCount_Seconds() / m_characterStatusManagement.GetCanUseDeathblow_RecoveryTime_Seconds());
            setValue(deathblowSlider, m_characterStatusManagement.GetCanUseDeathblow_TimeCount_Seconds() / m_characterStatusManagement.GetCanUseDeathblow_RecoveryTime_Seconds());
            setValue(ability1Slider,  m_characterStatusManagement.GetCanUseAbility1_TimeCount_Seconds() / m_characterStatusManagement.GetCanUseAbility1_RecoveryTime_Seconds());
            setValue(ability2Slider,  m_characterStatusManagement.GetCanUseAbility2_TimeCount_Seconds() / m_characterStatusManagement.GetCanUseAbility2_RecoveryTime_Seconds());
            setValue(ability3Slider,  m_characterStatusManagement.GetCanUseAbility3_TimeCount_Seconds() / m_characterStatusManagement.GetCanUseAbility3_RecoveryTime_Seconds());
        }

        void setValue( Slider slider, float value)
        {
            slider.value = value;
        }

        void UpdateValue( Slider slider, float max ,float value)
        {
            slider.value =  value / max;
        }
    }
}
