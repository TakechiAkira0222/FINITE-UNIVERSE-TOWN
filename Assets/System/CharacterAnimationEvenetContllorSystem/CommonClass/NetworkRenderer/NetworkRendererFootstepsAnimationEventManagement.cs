using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;

namespace Takechi.CharacterController.Footsteps
{
    public class NetworkRendererFootstepsAnimationEventManagement : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private CharacterBasicSoundEffectsStateManagement m_characterBasicSoundEffectsStateManagement;

        #endregion

        #region private variable
        private CharacterBasicSoundEffectsStateManagement soundEffectsStateManagement => m_characterBasicSoundEffectsStateManagement;

        #endregion

        #region UnityAnimatorEvent
        /// <summary>
        /// FootstepsSound
        /// </summary>
        private void FootstepsSound()
        {
          
        }
        #endregion
    }
}
