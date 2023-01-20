using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.Deathblow
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicDeathblow : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        #endregion

        #region protected
        protected CharacterStatusManagement statusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        private void Reset()
        {
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
            m_characterKeyInputStateManagement = this.GetComponent<CharacterKeyInputStateManagement>();
        }

        protected virtual void Update()
        {
            AvailabilityTimeControl();
        }

        protected virtual void OnEnable()
        {
            keyInputStateManagement.InputToDeathblow += ( statusManagement, addressManagement) => { WhileUsingIt(statusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToDeathblow function <color=green>to add.</color>");
        }

        protected virtual void OnDisable()
        {
            keyInputStateManagement.InputToDeathblow -= (statusManagement, addressManagement) => { WhileUsingIt(statusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToDeathblow function <color=green>to remove.</color>");
        }

        private void WhileUsingIt(CharacterStatusManagement characterStatusManagement)
        {
            characterStatusManagement.SetCanUseDeathblow(false);
            characterStatusManagement.SetCanUseDeathblow_TimeCount_Seconds(0);
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>SetCanUseDeathblow</color>(<color=blue>false</color>) <color=green>to set.</color>");
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>.SetCanUseDeathblow_TimeCount_Seconds</color>(<color=blue>0</color>) <color=green>to set.</color>");
        }

        private void AvailabilityTimeControl()
        {
            if (!statusManagement.GetCanUseDeathblow())
            {
                statusManagement.UpdateCanUseDeathblow_TimeCount_Seconds(Time.deltaTime);

                if (statusManagement.GetCanUseDeathblow_RecoveryTime_Seconds() <= statusManagement.GetCanUseDeathblow_TimeCount_Seconds())
                {
                    statusManagement.SetCanUseDeathblow(true);
                    Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>SetCanUseDeathblow</color>(<color=blue>true</color>) <color=green>to set.</color>");
                }
            }
        }
    }
}