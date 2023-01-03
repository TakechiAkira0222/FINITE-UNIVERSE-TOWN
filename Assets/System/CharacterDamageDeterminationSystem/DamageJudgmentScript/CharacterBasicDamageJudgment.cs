using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Takechi.ScriptReference.AnimationParameter;
using Takechi.ScriptReference.CustomPropertyKey;
using Takechi.ScriptReference.DamagesThePlayerObject;
using UnityEngine;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.CharacterController.DamageJudgment
{
    public class CharacterBasicDamageJudgment : MonoBehaviour
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        private Rigidbody m_rb => m_characterStatusManagement.GetMyRigidbody();

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
            if (!m_characterStatusManagement.GetMyPhotonView().IsMine) return;

            if (collision.gameObject.tag == ObjectReferenceThatDamagesThePlayer.s_PlayerCharacterWeaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                float power = 
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                m_characterStatusManagement.UpdateMass(-power);
                m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(knockBack(collision, power));
            }

            foreach (string s in ObjectReferenceThatDamagesThePlayer.s_DamagesThePlayerObjectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    float power = ObjectReferenceThatDamagesThePlayer.s_DamageObjectPowerDictionary[s];

                    m_characterStatusManagement.UpdateMass(-power);
                    m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();

                    StartCoroutine(knockBack(collision, power));
                }
            }
        }

        /// <summary>
        /// knockBack
        /// </summary>
        /// <returns></returns>
        private IEnumerator knockBack(Collision collision, float power)
        {
            float knockBackFrame = m_characterStatusManagement.GetCleanMass() - m_rb.mass;
            
            foreach(ContactPoint contactPoint in collision.contacts)
            {
                var impulse = (m_rb.transform.position - contactPoint.point).normalized;

                for (int turn = 0; turn < knockBackFrame; turn++)
                {
                    m_rb.AddForce((impulse * Mathf.Abs(power) * 1000) / knockBackFrame, ForceMode.Impulse);
                    yield return new WaitForSeconds(Time.deltaTime);
                }
            }
        }
    }
}
