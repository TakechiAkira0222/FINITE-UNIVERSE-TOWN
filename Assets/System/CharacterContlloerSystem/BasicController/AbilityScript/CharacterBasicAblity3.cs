using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.Ablity3
{
    public class CharacterBasicAblity3 : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        #endregion

        #region protected
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        private void Reset()
        {
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
            m_characterKeyInputStateManagement = this.GetComponent<CharacterKeyInputStateManagement>();
        }

        protected virtual void Update()
        {
            AvailableTimeControl();
        }

        protected virtual void OnEnable()
        {
            characterKeyInputStateManagement.InputToAblity3 += (characterStatusManagement) => { WhileUsingIt(characterStatusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToAblity3 function <color=green>to add.</color>");
        }

        protected virtual void OnDisable()
        {
            characterKeyInputStateManagement.InputToAblity3 -= (characterStatusManagement) => { WhileUsingIt(characterStatusManagement); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterKeyInputStateManagement.InputToAblity3 function <color=green>to remove.</color>");
        }

        private void WhileUsingIt(CharacterStatusManagement characterStatusManagement)
        {
            characterStatusManagement.SetCanUseAbility3(false);
            characterStatusManagement.SetCanUsecanUseAbility3_TimeCount_Seconds(0);
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>SetCanUseAbility3</color>(<color=blue>false</color>) <color=green>to set.</color>");
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>.SetCanUsecanUseAbility3_TimeCount_Seconds</color>(<color=blue>0</color>) <color=green>to set.</color>");
        }

        private void AvailableTimeControl()
        {
            if (!characterStatusManagement.GetCanUseAbility3())
            {
                characterStatusManagement.UpdateCanUseAbility3_TimeCount_Seconds(Time.deltaTime);

                if (characterStatusManagement.GetCanUseAbility3_RecoveryTime_Seconds() <= characterStatusManagement.GetCanUseAbility3_TimeCount_Seconds())
                {
                    characterStatusManagement.SetCanUseAbility3(true);
                    Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : characterStatusManagement.<color=yellow>SetCanUseAbility3</color>(<color=blue>true</color>) <color=green>to set.</color>");
                }
            }
        }
    }
}

