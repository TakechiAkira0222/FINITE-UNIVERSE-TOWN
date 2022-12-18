using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Takechi.CharacterController.Parameters;
using Takechi.ScriptReference.AnimationParameter;
using UnityEngine;
using UnityEngine.SearchService;

namespace Takechi.CharacterController.KeyInputStete
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterKeyInputStateManagement : MonoBehaviour
    {
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private bool m_operation = true;

        public event Action<CharacterStatusManagement> InputToStartDash = delegate { };
        public event Action<CharacterStatusManagement> InputToStopDash   = delegate { };
        public event Action<CharacterStatusManagement> InputToJump = delegate { };
        public event Action<CharacterStatusManagement> InputToNormalAttack = delegate { };
        public event Action<CharacterStatusManagement> InputToDeathblow = delegate { };
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

            if (Input.GetKey(KeyCode.LeftShift)) { InputToStartDash(characterStatusManagement); }
            else { InputToStopDash(characterStatusManagement); }

            if (Input.GetKeyDown(KeyCode.Space) && characterStatusManagement.GetIsGrounded()) { InputToJump(characterStatusManagement);}
            if (Input.GetMouseButtonDown(0)) { InputToNormalAttack(characterStatusManagement); }
            if (Input.GetKeyDown(KeyCode.Q) && characterStatusManagement.GetCanUseDeathblow()) { InputToDeathblow(characterStatusManagement); }

            InputToMovement(characterStatusManagement, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            InputToViewpoint(characterStatusManagement, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public void StopInput() 
        { 
            m_operation = false;
            InputToMovement(characterStatusManagement, 0, 0);
            InputToViewpoint(characterStatusManagement, 0, 0);
        }
        public void StartInput() { m_operation = true; }
    }
}