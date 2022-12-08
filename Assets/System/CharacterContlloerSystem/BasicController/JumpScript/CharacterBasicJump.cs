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
        #region PropertyClass
        public class RayProperty
        {
            public float m_distance;
            public Vector3 m_direction;

            public RayProperty(float distance, Vector3 direction)
            {
                m_distance = distance;
                m_direction = direction;
            }
        }
        #endregion

        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private Rigidbody m_rb;
        #endregion

        #region private
        private float m_upForce => m_characterStatusManagement.JumpPower;
        private float m_cleanMass => m_characterStatusManagement.CleanMass;

        private RayProperty rayProperty =
            new RayProperty( 0.1f, Vector3.down);

        /// <summary>
        /// ’…’n”»’è
        /// </summary>
        private bool m_isGrounded
        {
            get
            {
                Ray ray =
                    new Ray(m_rb.gameObject.transform.position + new Vector3(0, 0.1f),
                             rayProperty.m_direction * rayProperty.m_distance);

                RaycastHit raycastHit;


                if (Physics.Raycast(ray, out raycastHit, rayProperty.m_distance))
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.green);
                    return true;
                }
                else
                {
                    Debug.DrawRay(ray.origin, ray.direction * rayProperty.m_distance, Color.red);
                    return false;
                }
            }
        }

        #endregion

        #region UnityEvent

        void Reset()
        {
            m_rb = this.transform.GetComponent<Rigidbody>();
        }

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
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            if (!m_isGrounded) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rb.AddForce( transform.up * ( m_upForce * ( m_rb.mass / m_cleanMass)), ForceMode.Impulse);
            }
        }
        #endregion
    }
}
