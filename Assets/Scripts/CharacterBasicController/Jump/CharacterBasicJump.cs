using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.Jump
{
    public class CharacterBasicJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rb;

        private float m_upForce = 200f;
        private bool  m_isGround;

        void Reset()
        {
            m_rb = this.transform.GetComponent<Rigidbody>(); 
        }

        void Start()
        {

        }

        void Update()
        {
            if ( Input.GetKeyDown( KeyCode.Space))
            {
                m_rb.AddForce( transform.up * m_upForce, ForceMode.Impulse);
            }
        }
    }
}
