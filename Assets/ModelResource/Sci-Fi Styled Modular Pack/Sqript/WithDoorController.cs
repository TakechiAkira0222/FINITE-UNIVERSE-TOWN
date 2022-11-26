using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.MapObject.AnimatorController
{
    public class WithDoorController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;

        void Reset()
        {
            m_animator = this.transform.parent.gameObject.GetComponent<Animator>();
        }

        void OnCollisionEnter(Collision collision)
        {
            m_animator.SetBool("character_nearby", true);

            Invoke(nameof(OnCharacter_nearbyFalse), 3);
        }

        void OnCollisionExit(Collision collision)
        {
           
        }

        private void OnCharacter_nearbyFalse()
        {
            m_animator.SetBool("character_nearby", false);
        }
    }
}
