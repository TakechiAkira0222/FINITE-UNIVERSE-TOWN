using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Unity.Mathematics;
using UnityEngine;

namespace Takechi.CharacterController.Jump
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicJump : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region private
        private CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        #region UnityEvent

        private void OnEnable()
        {
            characterKeyInputStateManagement.InputToJump += (characterStatusManagement) =>
            {
                if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

                if (!characterStatusManagement.GetIsGrounded()) return;

                float upForce = characterStatusManagement.GetJumpPower();
                float cleanMass = characterStatusManagement.GetCleanMass();
                Rigidbody rb = characterStatusManagement.GetMyRigidbody();

                rb.AddForce(transform.up * (upForce * (rb.mass / cleanMass)), ForceMode.Impulse);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
        }
        private void OnDisable()
        {
            characterKeyInputStateManagement.InputToJump -= (characterStatusManagement) =>
            {
                if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

                if (!characterStatusManagement.GetIsGrounded()) return;

                float upForce = characterStatusManagement.GetJumpPower();
                float cleanMass = characterStatusManagement.GetCleanMass();
                Rigidbody rb = characterStatusManagement.GetMyRigidbody();

                rb.AddForce(transform.up * (upForce * (rb.mass / cleanMass)), ForceMode.Impulse);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");
        }

        #endregion
    }
}
