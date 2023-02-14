using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ContactJudgment;

using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;
using Takechi.CharacterController.Reference;
using Takechi.CharacterController.KeyInputStete;

namespace Takechi.CharacterController.DamageJudgment
{
    [RequireComponent(typeof(PhotonView))]
    public class CharacterBasicDamageJudgment : ContactJudgmentManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== Script Setting ===")]
        [SerializeField] private PhotonView m_thisPhotnView;
        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        private PhotonView thisPhotonView => m_thisPhotnView;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private GameObject myAvater  => addressManagement.GetMyAvater();
        private GameObject attackHitEffct => addressManagement.GetAttackHitEffct();
        private string     attackHitsEffectFolderName => addressManagement.GetAttackHitsEffectFolderName();

        private bool isStan = false;

        #endregion

        private void Reset()
        {
            m_thisPhotnView = this.GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => { resetStanState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetStanState</color>() <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => { resetStanState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetStanState</color>() <color=green>to remove.</color>");
        }

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

                AttackHitsEffectInstantiation(collision.contacts[0].point);
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

                AttackHitsEffectInstantiation(collision.contacts[0].point);
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

                    AttackHitsEffectInstantiation(collision.contacts[0].point);
                }
            }
        }
        private void OnTriggerEnter( Collider other)
        {
            if (!myPhotonView.IsMine) return;

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
                if (isStan) return;

                int number =
                     other.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ( checkTeammember(number)) return;

                int stanTime = other.transform.root.GetComponent<SlimeStatusManagement>().GetStanDsuration_Seconds();

                isStan = true;
                keyInputStateManagement.SetIsStan(true);
                addressManagement.GetStanMenu().SetActive(true);
                thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, true);

                StartCoroutine(DelayMethod( stanTime , ()=> 
                {
                    isStan = false;
                    keyInputStateManagement.SetIsStan(false);
                    addressManagement.GetStanMenu().SetActive(false);
                    thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, false);
                }));
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
        ///  attack Hits Effect instantiate
        /// </summary>
        /// <param name="collision"></param>
        private void AttackHitsEffectInstantiation( Vector3 point)
        {
            GameObject effct = PhotonNetwork.Instantiate(attackHitsEffectFolderName + attackHitEffct.name, point, Quaternion.identity);
            StartCoroutine(DelayMethod( 0.1f, () => { PhotonNetwork.Destroy(effct); }));
        }

        [PunRPC]
        private void RPC_StanEffectSync(bool flag)
        {
            addressManagement.GetStanEffect().SetActive(flag);
        }

        /// <summary>
        /// stanState reset
        /// </summary>
        private void resetStanState()
        {
            isStan = false;
            keyInputStateManagement.SetIsStan(false);
            addressManagement.GetStanMenu().SetActive(false);
            thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, false);
        }
    }
}
