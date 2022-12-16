using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Unity.Mathematics;
using UnityEngine;

namespace Takechi.CharacterController.Jump
{
    [RequireComponent(typeof(CharacterStatusManagement))]
    public class CharacterBasicJump : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
       
        #endregion

        #region private
        private float m_upForce => m_characterStatusManagement.GetJumpPower();
        private float m_cleanMass => m_characterStatusManagement.GetCleanMass();
        private Rigidbody m_rb => m_characterStatusManagement.GetMyRigidbody();

        #endregion

        #region UnityEvent

        void Start()
        {
            if ( m_characterStatusManagement == null)
            {
                m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
                Debug.LogWarning(" m_characterStatusManagement It wasn't set, so I set it.");
            }
        }

        void Update()
        {
            if (!m_characterStatusManagement.GetMyPhotonView().IsMine) return;

            if (!m_characterStatusManagement.GetIsGrounded()) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rb.AddForce( transform.up * ( m_upForce * ( m_rb.mass / m_cleanMass)), ForceMode.Impulse);
            }
        }
        #endregion
    }
}
