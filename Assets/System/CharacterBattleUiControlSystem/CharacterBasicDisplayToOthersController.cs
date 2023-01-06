using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Takechi.UI.SliderContlloer;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.UI.DisplayToOthers
{
    public class CharacterBasicDisplayToOthersController : SliderContlloerManager
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [SerializeField] private Slider m_massSlider;

        #endregion

        void Start()
        {
            setValue( m_massSlider, m_characterStatusManagement.GetCleanMass());
        }

        private void Update()
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.massKey] == null) return;

            int number =
                    m_characterStatusManagement.GetMyPhotonView().ControllerActorNr;
            float mass  =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.massKey];

            updateValue( m_massSlider, mass, m_characterStatusManagement.GetCleanMass());
        }
    }
}

