using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.Ablity1
{
    [RequireComponent(typeof(CharacterAddressManagement))]
    [RequireComponent(typeof(CharacterStatusManagement))]
    [RequireComponent(typeof(CharacterKeyInputStateManagement))]
    public class CharacterBasicAblity1 : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== statusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region protected
        protected CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        protected CharacterAddressManagement addressManagement => m_characterAddressManagement;
        protected CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion


        #region UnityEvenet
        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
            m_characterAddressManagement = this.transform.GetComponent<CharacterAddressManagement>();
            m_characterKeyInputStateManagement = this.transform.GetComponent<CharacterKeyInputStateManagement>();
        }

        protected virtual void Update()
        {
            AvailableTimeControl();
        }

        protected virtual void OnEnable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            characterKeyInputStateManagement.InputToAblity1 += (statusManagement, addressManagement) => { WhileUsingIt(statusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToAblity1 function <color=green>to add.</color>");
        }

        protected virtual void OnDisable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            characterKeyInputStateManagement.InputToAblity1 -= (statusManagement, addressManagement) => { WhileUsingIt(statusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToAblity1 function <color=green>to remove.</color>");
        }

        private void WhileUsingIt(CharacterStatusManagement statusManagement)
        {
            statusManagement.SetCanUseAbility1(false);
            statusManagement.SetCanUsecanUseAbility1_TimeCount_Seconds(0);
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : statusManagement.<color=yellow>SetCanUseAbility1</color>(<color=blue>false</color>) <color=green>to set.</color>");
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : statusManagement.<color=yellow>.SetCanUsecanUseAbility1_TimeCount_Seconds</color>(<color=blue>0</color>) <color=green>to set.</color>");
        }

        #endregion

        private void AvailableTimeControl()
        {
            if (!statusManagement.GetCanUseAbility1())
            {
                statusManagement.UpdateCanUseAbility1_TimeCount_Seconds(Time.deltaTime);

                if (statusManagement.GetCanUseAbility1_RecoveryTime_Seconds() <= statusManagement.GetCanUseAbility1_TimeCount_Seconds())
                {
                    statusManagement.SetCanUseAbility1(true);
                    Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : statusManagement.<color=yellow>SetCanUseAbility1</color>(<color=blue>true</color>) <color=green>to set.</color>");
                }
            }
        }
    }
}

