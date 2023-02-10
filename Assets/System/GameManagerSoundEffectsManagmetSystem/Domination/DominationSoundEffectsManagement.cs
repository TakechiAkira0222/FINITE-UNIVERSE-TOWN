using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;
using Takechi.GameManagerSystem.SoundEffects;
using UnityEngine;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationSoundEffectsManagement : GameManagmentBasicSoundEffectsManagement
    {
        #region serializeField
        [Header("=== DominationGameManagerParameter ===")]
        [SerializeField] private DominationGameManagerParameters m_dominationGameManagerParameters;

        #endregion


        #region private variable
        private DominationGameManagerParameters gameManagerParameters => m_dominationGameManagerParameters;

        #endregion

        #region PlayOneShotFunction
        public void PlayOneShotRisingAreaPointsSound()
        {
            myMainAudioSource.PlayOneShot(gameManagerParameters.GetRisingAreaPointsSoundClip(), gameManagerParameters.GetRisingAreaPointsSoundClipVolume());
        }

        #endregion

        #region public function

        public void RisingPointsSound()
        {
            if (!myMainAudioSource.isPlaying) { PlayOneShotRisingAreaPointsSound(); }
        }

        #endregion
    }
}