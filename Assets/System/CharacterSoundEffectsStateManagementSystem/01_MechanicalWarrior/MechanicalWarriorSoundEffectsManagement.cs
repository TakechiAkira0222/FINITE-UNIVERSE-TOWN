using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;
using UnityEngine;

namespace Takechi.CharacterController.SoundEffects
{
    public class MechanicalWarriorSoundEffectsManagement : CharacterBasicSoundEffectsStateManagement
    {
        [Header("=== MechanicalWarreiorSpecificSoundEffects ===")]
        [SerializeField] private MechanicalWarreiorSpecificSoundEffects  mechanicalWarreiorSpecificSoundEffects;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        #region PlayOneShotFunction
        public void PlayOneShotNormalShoot() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetNormalShootClip(), mechanicalWarreiorSpecificSoundEffects.GetNormalShootVolume()); }
        public void PlayOneShotDeathbolwShoot() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetDeathbolwShootClip(), mechanicalWarreiorSpecificSoundEffects.GetDeathbolwShootVolume()); }
        public void PlayOneShotDeathbolwCocking() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetDeathbolwCockingClip(), mechanicalWarreiorSpecificSoundEffects.GetDeathbolwCockingVolume()); }
        public void PlayOneShotDeathbolwReload() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetDeathbolwReloadClip(), mechanicalWarreiorSpecificSoundEffects.GetDeathbolwReloadVolume()); }
        public void PlayOneShotEnemySearchClickButton() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetEnemySearchClickButtonClip(), mechanicalWarreiorSpecificSoundEffects.GetEnemySearchClickButtonVolume()); }
        public void PlayOneShotEnemyOnSearch() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetEnemyOnSearchClip(), mechanicalWarreiorSpecificSoundEffects.GetEnemyOnSearchVolume()); }
        public void PlayOneShotSmokeExplosion() { myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetSmokeExplosionClip(), mechanicalWarreiorSpecificSoundEffects.GetSmokeExplosionVolume()); }

        #endregion
    }
}

