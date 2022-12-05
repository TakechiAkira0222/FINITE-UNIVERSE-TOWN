using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.DamagesThePlayerObject;
using UnityEngine;

namespace Takechi.CharacterController.DamageJudgment
{
    public class CharacterBasicDamageJudgment : MonoBehaviour
    {
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private Rigidbody m_rb;
        [SerializeField, Range(10 ,20)] private int   m_knockBackFrame = 15;
        [SerializeField] private float m_damageParameter = 5;

        void Start()
        {
            if (m_characterStatusManagement == null)
            {
                m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
                Debug.LogWarning(" m_characterStatusManagement It wasn't set, so I set it.");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            foreach ( string s in ObjectReferenceThatDamagesThePlayer.s_DamagesThePlayerObjectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    m_characterStatusManagement.updateMass( -m_damageParameter);
                    StartCoroutine(nameof(knockBack));
                }
            }
        }

        /// <summary>
        /// ƒmƒbƒNback
        /// </summary>
        /// <returns></returns>
        private IEnumerator knockBack()
        {
            for (int turn = 0; turn < m_knockBackFrame; turn++)
            {
                m_rb.AddForce(( -m_rb.transform.forward * Mathf.Abs( m_damageParameter) * 1000) / m_knockBackFrame, ForceMode.Impulse);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
