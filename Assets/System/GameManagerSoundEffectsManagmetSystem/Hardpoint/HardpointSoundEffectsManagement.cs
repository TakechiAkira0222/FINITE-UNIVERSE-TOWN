using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;
using Takechi.GameManagerSystem.SoundEffects;
using UnityEngine;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointSoundEffectsManagement : GameManagmentBasicSoundEffectsManagement
    {
        #region serializeField
        [Header("=== HardpointGameManagerParameter ===")]
        [SerializeField] private HardpointGameManagerParameters m_hardpointGameManagerParameters;

        #endregion

        #region private Variable
        private HardpointGameManagerParameters gameManagerParameters => m_hardpointGameManagerParameters;

        #endregion

        #region PlayOneShotFunction
        public void PlayOneShotRisingTeamPointsSound()
        {
            myMainAudioSource.PlayOneShot( gameManagerParameters.GetRisingTeamPointsSoundClip(), gameManagerParameters.GetRisingTeamPointsSoundClipVolume());
        }

        public void PlayOneShotGameEndSound()
        {
            myMainAudioSource.PlayOneShot(gameManagerParameters.GetGameEndSoundClip(), gameManagerParameters.GetGameEndSoundVolume());
        }

        #endregion

        #region public function

        public void RisingPointsSound()
        {
            if (!myMainAudioSource.isPlaying) { PlayOneShotRisingTeamPointsSound(); }
        }

        #endregion
    }
}
