using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.UI.BattleUiMenu
{
    public class CharacterBasicUserUiMenuController : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement inputStateManagement => m_characterKeyInputStateManagement;


        #endregion

        #region unity event
        private void OnEnable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            inputStateManagement.InputUserMenu += (statusManagement, addressManagement) =>
            {
                StateChangeOnMenu(addressManagement.GetUserUiMenu());
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
                //StateChangeOnMenu(addressManagement.GetBattleUiMenu());
                inputStateManagement.SetIsUserMenu(addressManagement.GetUserUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            inputStateManagement.InputUserMenu -= ( statusManagement, addressManagement) => 
            {
                StateChangeOnMenu(addressManagement.GetUserUiMenu());
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());
                // StateChangeOnMenu(addressManagement.GetBattleUiMenu());
                inputStateManagement.SetIsUserMenu(addressManagement.GetUserUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to remove.</color>");
        }

        #endregion

        private void StateChangeOnMenu( GameObject menu) { menu.SetActive(!menu.activeSelf); }
        private void StateChangeOnCanvas( Canvas canvas) { canvas.gameObject.SetActive(!canvas.gameObject.activeSelf); }
    }
}
