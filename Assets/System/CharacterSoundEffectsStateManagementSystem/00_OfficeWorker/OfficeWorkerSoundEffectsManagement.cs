using Photon.Pun;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;
using System.Security.Cryptography;
using Takechi.CharacterController.Address;

namespace Takechi.CharacterController.Parameters
{
    public class OfficeWorkerSoundEffectsManagement : CharacterBasicSoundEffectsStateManagement
    {
        #region SerializeField
        [Header("=== OfficeWorkerSpecificSoundEffects ===")]
        [SerializeField] private OfficeWorkerSpecificSoundEffects officeWorkerSpecificSoundEffects;
        #endregion

        #region unity evenet

        protected override void OnDisable()
        {
            base.OnDisable();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
        }

        #endregion

        #region PlayOneShotFunction
        public void PlayOneShotVoiceOfFirstNormalAttack()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfFirstNormalAttackClip(), officeWorkerSpecificSoundEffects.GetVoiceOfFirstNormalAttackVolume());
        }
        public void PlayOneShotVoiceOfSecondNormalAttack()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfSecondNormalAttackClip(), officeWorkerSpecificSoundEffects.GetVoiceOfSecondNormalAttackVolume());
        }
        public void PlayOneShotVoiceOfCallOut()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfCallOutClip(), officeWorkerSpecificSoundEffects.GetVoiceOfCallOutVolume());
        }
        public void PlayOneShotVoiceOfDeathblowStart()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfDeathblowStartClip(), officeWorkerSpecificSoundEffects.GetVoiceOfDeathblowStartVolume());
        }
        public void PlayOneShotVoiceOfThrowWatermelon()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfThrowWatermelonClip(), officeWorkerSpecificSoundEffects.GetVoiceOfThrowWatermelonVolume());
        }
        public void PlayOneShotPowerUp()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetDeathblowPowerUpClip(), officeWorkerSpecificSoundEffects.GetDeathblowPowerUpVolume());
        }
        public void PlayOneShotFirstNormalAttack()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetFirstNormalAttackClip(), officeWorkerSpecificSoundEffects.GetFirstNormalAttackVolume());
        }
        public void PlayOneShotSecondNormalAttack()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetSecondNormalAttackClip(), officeWorkerSpecificSoundEffects.GetSecondNormalAttackVolume());
        }
        public void PlayOneShotFlying()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetFlyingClip(), officeWorkerSpecificSoundEffects.GetFlyingVolume());
        }
        public void PlayOneShotPraySound()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetPraySoundClip(), officeWorkerSpecificSoundEffects.GetPraySoundVolume());
        }
        public void PlayOneShotWatermelonBreaking()
        {
            myMainAudioSource.PlayOneShot(officeWorkerSpecificSoundEffects.GetWatermelonBreakingClip(), officeWorkerSpecificSoundEffects.GetWatermelonBreakingVolume());
        }

        #endregion
    }
}

