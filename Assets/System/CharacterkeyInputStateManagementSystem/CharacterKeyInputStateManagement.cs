using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

using Takechi.CharacterController.Parameters;

namespace Takechi.CharacterController.KeyInputStete
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterKeyInputStateManagement : MonoBehaviour
    {
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [SerializeField] private KeyCode m_dashKey = KeyCode.LeftShift;
        [SerializeField] private KeyCode m_jumpKey = KeyCode.Space;
        [SerializeField] private KeyCode m_deathblowKey = KeyCode.Q;
        [SerializeField] private KeyCode m_ability1 = KeyCode.E;
        [SerializeField] private KeyCode m_ability2 = KeyCode.F;
        [SerializeField] private KeyCode m_ability3 = KeyCode.C;

        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private bool m_operation = true;

        public event Action<CharacterStatusManagement> InputToStartDash = delegate { };
        public event Action<CharacterStatusManagement> InputToStopDash   = delegate { };
        public event Action<CharacterStatusManagement> InputToJump = delegate { };
        public event Action<CharacterStatusManagement> InputToNormalAttack = delegate { };
        public event Action<CharacterStatusManagement> InputToDeathblow = delegate { };
        public event Action<CharacterStatusManagement> InputToAblity1 = delegate { };
        public event Action<CharacterStatusManagement> InputToAblity2 = delegate { };
        public event Action<CharacterStatusManagement> InputToAblity3 = delegate { };
        public event Action<CharacterStatusManagement, float ,float> InputToMovement = delegate { };
        public event Action<CharacterStatusManagement, float ,float> InputToViewpoint = delegate { };

        void Reset()
        {
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
        }

        void Update()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;
            if (!m_operation) return;

            if (Input.GetMouseButtonDown(0)) { InputToNormalAttack(characterStatusManagement); }

            if (Input.GetKey(m_dashKey)) { InputToStartDash(characterStatusManagement); }
            else { InputToStopDash(characterStatusManagement); }

            if (Input.GetKeyDown(m_jumpKey) && characterStatusManagement.GetIsGrounded()) { InputToJump(characterStatusManagement);}
            if (Input.GetKeyDown(m_deathblowKey) && characterStatusManagement.GetCanUseDeathblow()) { InputToDeathblow(characterStatusManagement); }
            if (Input.GetKeyDown(m_ability1) && characterStatusManagement.GetCanUseAbility1()) { InputToAblity1(characterStatusManagement); }
            if (Input.GetKeyDown(m_ability2) && characterStatusManagement.GetCanUseAbility2()) { InputToAblity2(characterStatusManagement); }
            if (Input.GetKeyDown(m_ability3) && characterStatusManagement.GetCanUseAbility3()) { InputToAblity3(characterStatusManagement); }

            InputToMovement(characterStatusManagement, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            InputToViewpoint(characterStatusManagement, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public bool GetOperation() { return m_operation; }
        public void SetOperation( bool value) 
        {
            m_operation = value;
            Debug.Log($" SetOperation = <color=green>{value}</color>");
        }
    }
}