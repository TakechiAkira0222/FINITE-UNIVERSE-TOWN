using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;
using UnityEngine;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== OfficeWorkerAddressManagement === ")]
        [SerializeField] private OfficeWorkerAddressManagement m_officeWorkerAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;

        #endregion

        #region private variable
        private OfficeWorkerAddressManagement addressManagement => m_officeWorkerAddressManagement;
        private CharacterStatusManagement     characterStatusManagement => m_characterStatusManagement;
        private OfficeWorkerSoundEffectsManagement officeWorkerSoundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private PhotonView  myPhotonView => addressManagement.GetMyPhotonView();
        private Rigidbody   myRb => addressManagement.GetMyRigidbody();
        private AudioSource audioSource => addressManagement.GetMyMainAudioSource();
        private GameObject  swordObject => addressManagement.GetSwordObject();
        private GameObject  swordEffectTrail => addressManagement.GetSwordEffectTrail();
        private Collider    myCollider => addressManagement.GetMyCollider();
        private Collider    swordCollider => swordObject.GetComponent<Collider>();

        #endregion

        #region UnityAnimatorEvent

        private void Awake()
        {
            Physics.IgnoreCollision( swordCollider, myCollider, false);
        }

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            officeWorkerSoundEffectsManagement.PlayOneShotFirstNormalAttack();
            officeWorkerSoundEffectsManagement.PlayOneShotVoiceOfFirstNormalAttack();

            if (myPhotonView.IsMine)
            {

            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// FastAttack Animation End
        /// </summary>
        void OfficeWorkerFastAttackEnd()
        {
            if (myPhotonView.IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// SecondAttack Animation Start
        /// </summary>
        void OfficeWorkerSecondAttackStart()
        {
            officeWorkerSoundEffectsManagement.PlayOneShotSecondNormalAttack();
            officeWorkerSoundEffectsManagement.PlayOneShotVoiceOfSecondNormalAttack();

            if (myPhotonView.IsMine)
            {

            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// SecondAttack Animation End
        /// </summary>
        void OfficeWorkerSecondAttackEnd()
        {
            if (myPhotonView.IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// ThirdAttack Animation Start
        /// </summary>
        void OfficeWorkerThirdAttackStart()
        {
            if (myPhotonView.IsMine)
            {

            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// ThirdAttack Animation End
        /// </summary>
        void OfficeWorkerThirdAttackEnd()
        {
            if (myPhotonView.IsMine) return;

            setSwordStatus(false);
        }

        #endregion

        #region set Function
        void setSwordStatus(bool flag)
        {
            swordEffectTrail.SetActive(flag);
            swordCollider.enabled = flag;
        }

        #endregion
    }
}