using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.ContactJudgment;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;
using Takechi.CharacterController.Parameters;

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
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private GameObject attackHitEffct => addressManagement.GetAttackHitEffct();
        private string     attackHitsEffectFolderName => addressManagement.GetAttackHitsEffectFolderName();

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (!myPhotonView.IsMine) return;

            if ( collision.gameObject.tag == DamageFromPlayerToPlayer.weaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                statusManagement.UpdateMass(-power);
                statusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(KnockBack(collision, power));

                EffectInstantiation(collision.contacts[0].point);
            }
            else if ( collision.gameObject.tag == DamageFromPlayerToPlayer.bulletsTagName)
            {
                int number =
                   collision.transform.GetComponent<PhotonView>().ControllerActorNr;

                if( checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                statusManagement.UpdateMass(-power);
                statusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(KnockBack(collision, power));

                EffectInstantiation(collision.contacts[0].point);
            }

            foreach (string s in DamageFromObjectToPlayer.objectNameList)
            {
                if ( collision.gameObject.name == s)
                {
                    float power =
                       DamageFromObjectToPlayer.objectDamagesDictionary[s];

                    statusManagement.UpdateMass(-power);
                    statusManagement.UpdateLocalPlayerCustomProrerties();

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

            myRb.AddForce( impulse * ( power * 1000), ForceMode.Impulse);
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
