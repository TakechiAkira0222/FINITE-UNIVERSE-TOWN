using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Address;

namespace Takechi.CharacterController.KeyInputStete
{
    [RequireComponent(typeof(CharacterAddressManagement))]
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterKeyInputStateManagement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [SerializeField] private KeyCode m_dashKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode m_jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode m_deathblowKey = KeyCode.Q;
        [SerializeField] private KeyCode m_ability1 = KeyCode.E;
        [SerializeField] private KeyCode m_ability2 = KeyCode.F;
        [SerializeField] private KeyCode m_ability3 = KeyCode.C;

        #endregion

        #region privet variable
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();

        private bool m_operation = true;

        #endregion

        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToStartDash = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToStopDash   = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToJump = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToNormalAttack = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToDeathblow = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToAblity1 = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToAblity2 = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement> InputToAblity3 = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement, float ,float> InputToMovement = delegate { };
        public event Action<CharacterStatusManagement, CharacterAddressManagement, float ,float> InputToViewpoint = delegate { };

        void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
            m_characterAddressManagement = this.transform.GetComponent<CharacterAddressManagement>();
        }

        void Update()
        {
            if (!myPhotonView.IsMine) return;
            if (!m_operation) return;

            if (Input.GetMouseButtonDown(0)) { InputToNormalAttack(statusManagement, addressManagement); }

            if (Input.GetKey(m_dashKey)) { InputToStartDash(statusManagement, addressManagement); }
            else { InputToStopDash(statusManagement, addressManagement); }

            if (Input.GetKeyDown(m_jumpKey) && statusManagement.GetIsGrounded()) { InputToJump(statusManagement, addressManagement);}
            if (Input.GetKeyDown(m_deathblowKey) && statusManagement.GetCanUseDeathblow()) { InputToDeathblow(statusManagement, addressManagement); }
            if (Input.GetKeyDown(m_ability1) && statusManagement.GetCanUseAbility1()) { InputToAblity1(statusManagement, addressManagement); }
            if (Input.GetKeyDown(m_ability2) && statusManagement.GetCanUseAbility2()) { InputToAblity2(statusManagement, addressManagement); }
            if (Input.GetKeyDown(m_ability3) && statusManagement.GetCanUseAbility3()) { InputToAblity3(statusManagement, addressManagement); }

            InputToMovement(statusManagement, addressManagement,Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            InputToViewpoint(statusManagement, addressManagement, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public bool GetOperation() { return m_operation; }
        public void SetOperation( bool value) 
        {
            m_operation = value;
            Debug.Log($" SetOperation = <color=green>{value}</color>");
        }
    }
}