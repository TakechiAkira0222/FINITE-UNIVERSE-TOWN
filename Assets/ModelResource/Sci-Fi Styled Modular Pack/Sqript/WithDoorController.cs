using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.MapObject.AnimatorController
{
    public class WithDoorController : MonoBehaviour
    {
        #region SerialzeField
        [SerializeField] private Animator m_animator;
        [SerializeField] private bool     m_isOperation = true; 
        [SerializeField] private float    m_openingTime = 3;
        #endregion

        #region UnityEvent
        void Reset()
        {
            m_isOperation = true;
            m_openingTime = 3;
            m_animator = this.transform.parent.gameObject.GetComponent<Animator>();
        }

        private void Update()
        {
            // ‰¼
            if(Vector3.Distance(this.gameObject.transform.position, Camera.main.transform.position) <= 2)
            {
                m_animator.SetBool("character_nearby", true);

                Invoke(nameof(OnCharacter_nearbyFalse), m_openingTime);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!m_isOperation) return;

            m_animator.SetBool("character_nearby", true);

            Invoke(nameof(OnCharacter_nearbyFalse), m_openingTime);
        }

        void OnCollisionExit(Collision collision)
        {
           
        }

        private void OnCharacter_nearbyFalse()
        {
            m_animator.SetBool("character_nearby", false);
        }
        #endregion

    }
}
