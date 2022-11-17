using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace takechi.CharacterController.Jump
{
    public class CharacterBasicJump : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rb;

        private float m_upForce = 1250f;
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
            if (Input.GetKeyDown( KeyCode.Space))
            {
                m_rb.AddForce(new Vector3( 0, m_upForce, 0)); 
            }
        }
    }
}
