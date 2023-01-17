using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;
using UnityEngine;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private GameObject m_swordObject;
        [SerializeField] private GameObject m_swordEffectTrail;

        #endregion

        #region private
        private PhotonView thisPhotonView => m_thisPhotonView;
        private CharacterStatusManagement    characterStatusManagement => m_characterStatusManagement;
        private OfficeWorkerSoundEffectsManagement officeWorkerSoundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private GameObject  swordObject => m_swordObject;
        private GameObject  swordEffectTrail => m_swordEffectTrail;
        private Rigidbody   rb => characterStatusManagement.GetMyRigidbody();
        private AudioSource audioSource => characterStatusManagement.GetMyMainAudioSource();
        private Collider    swordCollider => swordObject.GetComponent<Collider>();

        #endregion

        #region UnityAnimatorEvent

        private void Awake()
        {
            Physics.IgnoreCollision( swordCollider, characterStatusManagement.GetMyCollider(), false);
        }

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            officeWorkerSoundEffectsManagement.PlayOneShotFirstNormalAttack();
            officeWorkerSoundEffectsManagement.PlayOneShotVoiceOfFirstNormalAttack();

            if (characterStatusManagement.GetMyPhotonView().IsMine)
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
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// SecondAttack Animation Start
        /// </summary>
        void OfficeWorkerSecondAttackStart()
        {
            officeWorkerSoundEffectsManagement.PlayOneShotSecondNormalAttack();
            officeWorkerSoundEffectsManagement.PlayOneShotVoiceOfSecondNormalAttack();

            if (characterStatusManagement.GetMyPhotonView().IsMine)
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
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// ThirdAttack Animation Start
        /// </summary>
        void OfficeWorkerThirdAttackStart()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine)
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
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

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