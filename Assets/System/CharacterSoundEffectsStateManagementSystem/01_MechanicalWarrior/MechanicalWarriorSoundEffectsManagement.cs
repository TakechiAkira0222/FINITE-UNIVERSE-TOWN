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

        public void PlayOneShotNormalShot()
        {
            myMainAudioSource.PlayOneShot(mechanicalWarreiorSpecificSoundEffects.GetNormalShotClip(), mechanicalWarreiorSpecificSoundEffects.GetNormalShotVolume());
        }

        #endregion
    }
}

