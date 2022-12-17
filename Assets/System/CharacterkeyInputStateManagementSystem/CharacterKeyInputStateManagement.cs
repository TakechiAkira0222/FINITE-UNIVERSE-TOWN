using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.KeyInputStete
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterKeyInputStateManagement : MonoBehaviour
    {
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;

        public event Action<CharacterStatusManagement> GetKeyDownLeftShift = delegate { };
        public event Action<CharacterStatusManagement> GetKeyUpLeftShift   = delegate { };
        public event Action<CharacterStatusManagement> GetKeyDownSpace   = delegate { };
        public event Action<CharacterStatusManagement> GetMouseButtonDown = delegate { };
        public event Action<CharacterStatusManagement> GetKeyDownKeyCodeQ = delegate { };

        void Reset()
        {
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
        }

        void Update()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            if (Input.GetKeyDown(KeyCode.LeftShift)) { GetKeyDownLeftShift(characterStatusManagement); }
            if (Input.GetKeyUp(KeyCode.LeftShift)) { GetKeyUpLeftShift(characterStatusManagement); }
            if (Input.GetKeyDown(KeyCode.Space) && characterStatusManagement.GetIsGrounded()) { GetKeyDownSpace(characterStatusManagement);}
            if (Input.GetMouseButtonDown(0)) { GetMouseButtonDown(characterStatusManagement); }
            if (Input.GetKeyDown(KeyCode.Q)) { GetKeyDownKeyCodeQ(characterStatusManagement); }
        }
    }
}