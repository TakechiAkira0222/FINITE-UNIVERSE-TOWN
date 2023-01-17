using Photon.Pun;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;
using System.Security.Cryptography;

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
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfFirstNormalAttackClip(), officeWorkerSpecificSoundEffects.GetVoiceOfFirstNormalAttackVolume());
        }
        public void PlayOneShotVoiceOfSecondNormalAttack()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfSecondNormalAttackClip(), officeWorkerSpecificSoundEffects.GetVoiceOfSecondNormalAttackVolume());
        }
        public void PlayOneShotVoiceOfCallOut()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfCallOutClip(), officeWorkerSpecificSoundEffects.GetVoiceOfCallOutVolume());
        }
        public void PlayOneShotVoiceOfDeathblowStart()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfDeathblowStartClip(), officeWorkerSpecificSoundEffects.GetVoiceOfDeathblowStartVolume());
        }
        public void PlayOneShotPowerUp()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetDeathblowPowerUpClip(), officeWorkerSpecificSoundEffects.GetDeathblowPowerUpVolume());
        }
        public void PlayOneShotFirstNormalAttack()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetFirstNormalAttackClip(), officeWorkerSpecificSoundEffects.GetFirstNormalAttackVolume());
        }
        public void PlayOneShotSecondNormalAttack()
        {
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetSecondNormalAttackClip(), officeWorkerSpecificSoundEffects.GetSecondNormalAttackVolume());
        }
        #endregion
    }
}

