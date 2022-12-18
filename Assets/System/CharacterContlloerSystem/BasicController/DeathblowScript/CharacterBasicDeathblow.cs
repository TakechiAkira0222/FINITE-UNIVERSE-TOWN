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
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        protected virtual void Update()
        {
            AvailabilityTimeControl();
        }

        protected virtual void OnEnable()
        {
            characterKeyInputStateManagement.InputToDeathblow += (characterStatusManagement) => 
            {
                characterStatusManagement.SetCanUseDeathblow(false);
                Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterStatusManagement.<color=yellow>SetCanUseDeathblow</color>(<color=blue>false</color>) <color=green>to set.</color>");
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToDeathblow function <color=green>to add.</color>");
        }

        protected virtual void OnDisable()
        {
            characterKeyInputStateManagement.InputToDeathblow -= (characterStatusManagement) => 
            {
                characterStatusManagement.SetCanUseDeathblow(false);
                Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterStatusManagement.<color=yellow>SetCanUseDeathblow</color>(<color=blue>false</color>) <color=green>to set.</color>");
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  m_movementAnimatoinAction function <color=green>to remove.</color>");
        }

        private void AvailabilityTimeControl()
        {
            if (!characterStatusManagement.GetCanUseDeathblow())
            {
                characterStatusManagement.UpdateCanUseDeathblow_TimeCount_Seconds(Time.deltaTime);

                if (characterStatusManagement.GetCanUseDeathblow_RecoveryTime_Seconds() <= characterStatusManagement.GetCanUseDeathblow_TimeCount_Seconds())
                {
                    characterStatusManagement.SetCanUseDeathblow_TimeCount_Seconds(0);
                    characterStatusManagement.SetCanUseDeathblow(true);
                    Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterStatusManagement.<color=yellow>SetCanUseDeathblow</color>(<color=blue>true</color>) <color=green>to set.</color>");
                }
            }
        }
    }
}