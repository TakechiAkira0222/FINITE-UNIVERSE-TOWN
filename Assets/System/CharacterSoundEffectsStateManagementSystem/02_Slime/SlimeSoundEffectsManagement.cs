using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;
using Takechi.CharacterController.SpecificSoundEffects.Slime;
using UnityEngine;


namespace Takechi.CharacterController.SoundEffects
{
    public class SlimeSoundEffectsManagement : CharacterBasicSoundEffectsStateManagement
    {
        [Header("=== SlimeSpecificSoundEffects ===")]
        [SerializeField] private SlimeSpecificSoundEffects m_slimeSpecificSoundEffects;

        private SlimeSpecificSoundEffects slimeSpecificSoundEffects => m_slimeSpecificSoundEffects;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        #region PlayOneShotFunction
        public void PlayOneShotNormalShot() { myMainAudioSource.PlayOneShot(slimeSpecificSoundEffects.GetNormalShootClip(), slimeSpecificSoundEffects.GetNormalShootVolume()); }
        public void PlayOneShotStan() { myMainAudioSource.PlayOneShot(slimeSpecificSoundEffects.GetStanClip(), slimeSpecificSoundEffects.GetStanVolume()); }
        public void PlayOneShotTrnado() { myMainAudioSource.PlayOneShot(slimeSpecificSoundEffects.GetTrnadoClip(), slimeSpecificSoundEffects.GetTrnadoVolume()); }
        public void PlayOneShotTrransparency() { myMainAudioSource.PlayOneShot(slimeSpecificSoundEffects.GetTrransparencyClip(), slimeSpecificSoundEffects.GetTrransparencyVolume()); }
        public void PlayOneShotDeathblowInstantiate() { myMainAudioSource.PlayOneShot(slimeSpecificSoundEffects.GetDeathblowInstantiateClip(), slimeSpecificSoundEffects.GetDeathblowInstantiateVolume()); }

        #endregion
    }
}
