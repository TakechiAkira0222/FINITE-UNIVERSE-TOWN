using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Unity.Mathematics;
using UnityEngine;

namespace Takechi.CharacterController.Jump
{
    [RequireComponent(typeof(CharacterKeyInputStateManagement))]
    public class CharacterBasicJump : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;

        #endregion

        #region private
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        #region UnityEvent
        private void Reset()
        {
            m_characterKeyInputStateManagement = this.transform.GetComponent<CharacterKeyInputStateManagement>();
        }

        private void OnEnable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            keyInputStateManagement.InputToJump += (statusManagement, addressManagement) =>
            {
                if (!statusManagement.GetIsGrounded()) return;

                float upForce = statusManagement.GetJumpPower();
                float cleanMass = statusManagement.GetCleanMass();
                Rigidbody rb = addressManagement.GetMyRigidbody();

                rb.AddForce( transform.up * (upForce * (rb.mass / cleanMass)), ForceMode.Impulse);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
        }
        private void OnDisable()
        {
            if (!addressManagement.GetMyPhotonView().IsMine) return;

            keyInputStateManagement.InputToJump -= (statusManagement, addressManagement) =>
            {
                if (!statusManagement.GetIsGrounded()) return;

                float upForce = statusManagement.GetJumpPower();
                float cleanMass = statusManagement.GetCleanMass();
                Rigidbody rb = addressManagement.GetMyRigidbody();

                rb.AddForce(transform.up * (upForce * (rb.mass / cleanMass)), ForceMode.Impulse);
            };

            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");
        }

        #endregion
    }
}
