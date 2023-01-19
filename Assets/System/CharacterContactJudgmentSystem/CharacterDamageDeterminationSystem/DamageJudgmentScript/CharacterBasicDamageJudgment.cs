using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ContactJudgment;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;

namespace Takechi.CharacterController.DamageJudgment
{
    public class CharacterBasicDamageJudgment : ContactJudgmentManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private GameObject attackHitEffct => addressManagement.GetAttackHitEffct();
        private string attackHitsEffectFolderName => addressManagement.GetAttackHitsEffectFolderName();

        #endregion

        void Start()
        {
            if ( m_characterStatusManagement == null)
            {
                m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
                Debug.LogWarning(" m_characterStatusManagement It wasn't set, so I set it.");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!myPhotonView.IsMine) return;

            if ( collision.gameObject.tag == DamageFromPlayerToPlayer.weaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = 
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                m_characterStatusManagement.UpdateMass(-power);
                m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(KnockBack(collision, power));

                EffectInstantiation(collision.contacts[0].point);
            }
            else if ( collision.gameObject.tag == DamageFromPlayerToPlayer.bulletsTagName)
            {
                int number =
                   collision.transform.GetComponent<PhotonView>().ControllerActorNr;

                if( checkTeammember(number)) return;

                float power =
                    (float)PhotonNetwork.LocalPlayer.Get(number).CustomProperties[CharacterStatusKey.attackPowerKey];

                m_characterStatusManagement.UpdateMass(-power);
                m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(KnockBack(collision, power));

                EffectInstantiation(collision.contacts[0].point);
            }

            foreach (string s in DamageFromObjectToPlayer.objectNameList)
            {
                if ( collision.gameObject.name == s)
                {
                    float power =
                       DamageFromObjectToPlayer.objectDamagesDictionary[s];

                    m_characterStatusManagement.UpdateMass(-power);
                    m_characterStatusManagement.UpdateLocalPlayerCustomProrerties();

                    StartCoroutine(KnockBack(collision, power));

                    EffectInstantiation(collision.contacts[0].point);
                }
            }
        }

        /// <summary>
        /// knockBack
        /// </summary>
        /// <returns></returns>
        private IEnumerator KnockBack(Collision collision, float power)
        {
            var impulse = (myRb.transform.position - collision.contacts[0].point).normalized;

            myRb.AddForce( impulse * ( power * 5000), ForceMode.Impulse);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collision"></param>
        private void EffectInstantiation( Vector3 point)
        {
            GameObject effct = PhotonNetwork.Instantiate(attackHitsEffectFolderName + attackHitEffct.name, point, Quaternion.identity);
            StartCoroutine(DelayMethod( 0.1f, () => { PhotonNetwork.Destroy(effct); }));
        }
    }
}
