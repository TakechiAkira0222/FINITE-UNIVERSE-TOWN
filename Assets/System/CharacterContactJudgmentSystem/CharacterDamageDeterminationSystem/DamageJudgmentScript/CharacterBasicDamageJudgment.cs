using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ContactJudgment;

using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;
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
        private GameObject aiSliemHitEffect => addressManagement.GetAiSlimeHitEffect();
        private GameObject stanEffect =>  addressManagement.GetStanEffect();
        private string     attackHitsEffectFolderName => addressManagement.GetAttackHitsEffectFolderName();

        private bool isStan = false;
        private bool isAiSlimeHit = false;

        private struct AdjustmentParameter 
        {
            public const int knockBackPrameter = 50;
            public const int tornadoPrameter = 300;
        }

        #endregion

        private void Reset()
        {
            m_thisPhotnView = this.GetComponent<PhotonView>();
        }

        private void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => 
            {
                resetStanState();
                resetAiSlimeHitState();
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow> reset function </color> <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => 
            {
                resetStanState();
                resetAiSlimeHitState();
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow> reset function</color> <color=green>to remove.</color>");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!myPhotonView.IsMine) return;

            if ( collision.gameObject.tag == DamageFromPlayerToPlayer.ColliderTag.weaponTagName)
            {
                int number = 
                    collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ( checkTeammember(number)) return;

                TemplateForDamageProcessing(collision, number);
            }

            if ( collision.gameObject.tag == DamageFromPlayerToPlayer.ColliderTag.bulletsTagName)
            {
                int number =
                   collision.transform.GetComponent<PhotonView>().ControllerActorNr;

                if( checkTeammember(number)) return;

                TemplateForDamageProcessing(collision, number);
            }

            if( collision.gameObject.name == DamageFromPlayerToPlayer.ColliderName.aiSlimeColliderName)
            {
                if (isAiSlimeHit) return;

                PhotonView controllerPhotonView =
                    collision.transform.root.GetComponent<PhotonView>();

                int controllerActorNr =
                     controllerPhotonView.ControllerActorNr;

                if ( checkTeammember(controllerActorNr))
                {
                    isAiSlimeHit = true;
                    thisPhotonView.RPC(nameof(RPC_AiSliemHitEffectSync), RpcTarget.AllBufferedViaServer, isAiSlimeHit);
                    statusManagement.UpdateMovingSpeed(2);
                }
                else
                {
                    isAiSlimeHit = true;
                    thisPhotonView.RPC(nameof(RPC_AiSliemHitEffectSync), RpcTarget.AllBufferedViaServer, isAiSlimeHit);
                    statusManagement.UpdateMovingSpeed(-2);
                    TemplateForDamageProcessing( collision, controllerActorNr);
                }
            }

            foreach (string s in DamageFromObjectToPlayer.objectNameList)
            {
                if ( collision.gameObject.name == s)
                {
                    float power =
                       DamageFromObjectToPlayer.objectDamagesDictionary[s];

                    statusManagement.UpdateMass(-power);
                    statusManagement.UpdateLocalPlayerCustomProrerties();

                    var impulse = (myRb.transform.position - collision.contacts[0].point).normalized;

                    myRb.AddForce(impulse * (power * AdjustmentParameter.knockBackPrameter), ForceMode.Impulse);
                    // StartCoroutine(KnockBack(collision, power));

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

                myRb.AddForce( myAvater.transform.up * ( power * AdjustmentParameter.tornadoPrameter), ForceMode.Impulse);

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
                keyInputStateManagement.SetIsStan(isStan);
                addressManagement.GetStanMenu().SetActive(isStan);
                thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, isStan);

                StartCoroutine(DelayMethod( stanTime , ()=> 
                {
                    isStan = false;
                    keyInputStateManagement.SetIsStan(isStan);
                    addressManagement.GetStanMenu().SetActive(isStan);
                    thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, isStan);
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

            myRb.AddForce(impulse * ( power * AdjustmentParameter.knockBackPrameter), ForceMode.Impulse);
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

        /// <summary>
        /// template for damage processing
        /// </summary>
        /// <param name="collision"></param>
        /// <param name="controllerActorNr"></param>
        private void TemplateForDamageProcessing( Collision collision, int controllerActorNr)
        {
            float power = statusManagement.GetCustomPropertiesTeamAttackPower(controllerActorNr);

            statusManagement.UpdateMass(-power);
            statusManagement.UpdateLocalPlayerCustomProrerties();

            var impulse = (myRb.transform.position - collision.contacts[0].point).normalized;

            myRb.AddForce(impulse * (power * AdjustmentParameter.knockBackPrameter), ForceMode.Impulse);

           // StartCoroutine(KnockBack(collision, power));

            AttackHitsEffectInstantiation(collision.contacts[0].point);
        }

        #region PunRPC function
        [PunRPC]
        private void RPC_StanEffectSync(bool flag)
        {
            stanEffect.SetActive(flag);
        }
        [PunRPC]
        private void RPC_AiSliemHitEffectSync(bool flag)
        {
            aiSliemHitEffect.SetActive(flag);
        }

        #endregion

        #region state reset function 
        /// <summary>
        /// stanState reset
        /// </summary>
        private void resetStanState()
        {
            isStan = false;
            keyInputStateManagement.SetIsStan(isStan);
            addressManagement.GetStanMenu().SetActive(isStan);
            thisPhotonView.RPC(nameof(RPC_StanEffectSync), RpcTarget.AllBufferedViaServer, isStan);
        }
        /// <summary>
        /// sliemHit state  reset
        /// </summary>
        private void resetAiSlimeHitState()
        {
            isAiSlimeHit = false;
            thisPhotonView.RPC(nameof(RPC_AiSliemHitEffectSync), RpcTarget.AllBufferedViaServer, isAiSlimeHit);
        }

        #endregion
    }
}
