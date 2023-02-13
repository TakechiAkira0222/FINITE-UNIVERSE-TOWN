using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ContactJudgment;

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
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private GameObject myAvater  => addressManagement.GetMyAvater();
        private GameObject attackHitEffct => addressManagement.GetAttackHitEffct();
        private string     attackHitsEffectFolderName => addressManagement.GetAttackHitsEffectFolderName();

        #endregion

        private void OnCollisionEnter(Collision collision)
        {
            if (!myPhotonView.IsMine) return;

            if ( collision.gameObject.tag == DamageFromPlayerToPlayer.ColliderTag.weaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ( checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                statusManagement.UpdateMass(-power);
                statusManagement.UpdateLocalPlayerCustomProrerties();

                StartCoroutine(KnockBack(collision, power));

                EffectInstantiation(collision.contacts[0].point);
            }
            else if ( collision.gameObject.tag == DamageFromPlayerToPlayer.ColliderTag.bulletsTagName)
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
        private void OnTriggerEnter( Collider other)
        {
            if ( other.name == DamageFromPlayerToPlayer.ColliderName.slimeAblityTornadoColliderName)
            {
                int number =
                     other.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                myRb.AddForce( myAvater.transform.up * ( power * 1000), ForceMode.Impulse);

                statusManagement.UpdateMass(-power);
                statusManagement.UpdateLocalPlayerCustomProrerties();
            }

            if (other.name == DamageFromPlayerToPlayer.ColliderName.slimeAblityStanColliderName)
            {
                int number =
                     other.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                Debug.Log("stan èàóù");
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (!myPhotonView.IsMine) return;

            if (other.tag == DamageFromPlayerToPlayer.ColliderTag.lazerEffectTagName)
            {
                int number =
                     other.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if (checkTeammember(number)) return;

                float power = statusManagement.GetCustomPropertiesTeamAttackPower(number);

                statusManagement.UpdateMass(-power);
                statusManagement.UpdateLocalPlayerCustomProrerties();
            }
        }

        /// <summary>
        /// knockBack
        /// </summary>
        /// <returns></returns>
        private IEnumerator KnockBack(Collision collision, float power)
        {
            var impulse = (myRb.transform.position - collision.contacts[0].point).normalized;

            myRb.AddForce(impulse * (power * 1000), ForceMode.Impulse);
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
