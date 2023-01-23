using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.UI.SliderContlloer;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.UI.DisplayToOthers
{
    public class CharacterBasicDisplayToOthersController : SliderContlloerManager
    {
        #region SerializeField

        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private Slider m_massSlider;
        [SerializeField] private Text   m_nickNameText;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();

        #endregion

        void Start()
        {
            m_nickNameText.text = myPhotonView.Owner.NickName;

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
            {
                m_nickNameText.color = Color.red;
            }
            else { m_nickNameText.color = Color.blue; }

            setValue(m_massSlider, statusManagement.GetCleanMass());
        }

        private void Update()
        {
            if (PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.massKey] == null) return;

            int number = myPhotonView.ControllerActorNr;
            float mass  =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.massKey];

            updateValue( m_massSlider, mass, m_characterStatusManagement.GetCleanMass());
        }
    }
}

