using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

namespace Takechi.CharacterController.Jump
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

    public class CharacterBasicJump : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private Rigidbody m_rb;
        [SerializeField] private float m_upForce = 200f;
        #endregion

        #region private
        private RayProperty rayProperty =
            new RayProperty( 0.1f, Vector3.down);

        /// <summary>
        /// ���n����
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

        }

        void Update()
        {
            if (!m_isGrounded)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_rb.AddForce(transform.up * m_upForce, ForceMode.Impulse);
            }
        }
        #endregion
    }
}
