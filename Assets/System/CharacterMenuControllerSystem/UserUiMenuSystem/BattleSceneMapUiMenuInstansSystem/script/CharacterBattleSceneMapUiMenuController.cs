using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.RoomStatus;
using Takechi.PlayableCharacter.FadingCanvas;
using TakechiEngine.PUN;

namespace Takechi.UI.BattleUiMenu.BattleScene
{
    public class CharacterBattleSceneMapUiMenuController : TakechiPunCallbacks
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== script setting ===")]
        [SerializeField] private ToFade m_toFade;
        #endregion

        #region private variable
        protected CharacterAddressManagement addressManagement => m_characterAddressManagement;
        protected CharacterStatusManagement statusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        protected RoomStatusManagement roomStatusManagement => addressManagement.GetMyRoomStatusManagement();
        protected bool isMine => addressManagement.GetMyPhotonView().IsMine;
        protected ToFade toFade => m_toFade;

        #endregion

        #region unity event
        public override void OnEnable()
        {
            if (!isMine) return;

            base.OnEnable();

            keyInputStateManagement.InputDisplayMapMenu += (statusManagement, addressManagement) =>
            {
                StateChangeOnMenu(addressManagement.GetMapUiMenu());
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());

                keyInputStateManagement.SetIsDisplayMap(addressManagement.GetMapUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to add.</color>");
        }

        public override void OnDisable()
        {
            if (!isMine) return;

            base.OnDisable();

            keyInputStateManagement.InputDisplayMapMenu -= (statusManagement, addressManagement) =>
            {
                StateChangeOnMenu(addressManagement.GetMapUiMenu());
                StateChangeOnCanvas(addressManagement.GetReticleCanvas());

                keyInputStateManagement.SetIsDisplayMap(addressManagement.GetMapUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to remove.</color>");
        }

        #endregion
    }
}
