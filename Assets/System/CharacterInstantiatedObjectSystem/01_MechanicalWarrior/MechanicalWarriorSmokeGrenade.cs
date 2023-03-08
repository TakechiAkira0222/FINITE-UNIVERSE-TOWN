using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.ContactJudgment;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;

namespace Takechi.CharacterController.SmokeGrenade
{
    public class MechanicalWarriorSmokeGrenade : ContactJudgmentManagement
    {
        [Header("=== MechanicalWarreiorSpecificSoundEffects ===")]
        [SerializeField] private MechanicalWarreiorSpecificSoundEffects mechanicalWarreiorSpecificSoundEffects;
        [SerializeField] private ParticleSystem m_detonationEffect;
        [SerializeField] private GameObject  m_smokeEffect;
        [SerializeField] private AudioSource m_audioSource;

        private bool isOnePlay = false;
        private AudioSource audioSource => m_audioSource;
            
        private void OnCollisionEnter(Collision collision)
        {
            if (isOnePlay) return;

            m_detonationEffect.Play();
            m_smokeEffect.SetActive(true);
            PlayOneShotSmokeExplosion();

            isOnePlay = true;
        }

        public void PlayOneShotSmokeExplosion() { audioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetSmokeExplosionClip(), mechanicalWarreiorSpecificSoundEffects.GetSmokeExplosionVolume()); }
    }
}
