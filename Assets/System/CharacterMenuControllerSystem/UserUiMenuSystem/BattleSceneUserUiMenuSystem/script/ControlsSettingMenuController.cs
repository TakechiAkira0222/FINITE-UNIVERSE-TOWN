using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Takechi.ExternalData;
using static Takechi.ScriptReference.CryptoRedPath.ReferenceCryptoRedPath;
using System.Reflection;
using UnityEngine.UI;
using Takechi.CharacterController.ViewpointOperation;

namespace Takechi.UI.ControlsSettingMenu
{
    public class ControlsSettingData
    {
        public float xSensityvity = 1f;
        public float ySensityvity = 1f;
    }

    public class ControlsSettingMenuController : ExternalDataManagement
    {
        [SerializeField] private CharacterBasicViewpointOperation m_characterBasicViewpointOperation;

        [SerializeField] private Text m_sensitivityXText;
        [SerializeField] private Slider m_sensitivityXSlider;

        [SerializeField] private Text m_sensitivityYText;
        [SerializeField] private Slider m_sensitivityYSlider;

        private CharacterBasicViewpointOperation viewpointOperation => m_characterBasicViewpointOperation;
        private ControlsSettingData controlsSettingData = new ControlsSettingData();
        private string cleanpath => Cleanpath.controlsSettingDataPath;

        private void OnEnable()
        {
            controlsSettingData = LoadData(controlsSettingData, cleanpath);

            m_sensitivityXSlider.value = controlsSettingData.xSensityvity;
            m_sensitivityYSlider.value = controlsSettingData.ySensityvity;

            m_sensitivityXText.text = m_sensitivityXSlider.value.ToString("N2");
            m_sensitivityYText.text = m_sensitivityYSlider.value.ToString("N2");
        }

        public void OnDragChangeVariable()
        {
            m_sensitivityXText.text = m_sensitivityXSlider.value.ToString("N2");
            m_sensitivityYText.text = m_sensitivityYSlider.value.ToString("N2");
        }

        public void OnSave()
        {
            controlsSettingData.xSensityvity = m_sensitivityXSlider.value;
            controlsSettingData.ySensityvity = m_sensitivityYSlider.value;

            m_sensitivityXText.text = m_sensitivityXSlider.value.ToString("N2");
            m_sensitivityYText.text = m_sensitivityYSlider.value.ToString("N2");

            viewpointOperation.SetXSensityvity(controlsSettingData.xSensityvity);
            viewpointOperation.SetYSensityvity(controlsSettingData.ySensityvity);

            DataSave( controlsSettingData, cleanpath);
        }
    }
}
