using Photon.Voice.PUN.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SoundEffects
{
    [CreateAssetMenu(fileName = "PlayableCharacterSoundEffectsDataBase", menuName = "PlayableCharacterSoundEffectsDataBase")]
    public class PlayableCharacterSoundEffectsDataBase : ScriptableObject
    {
        [SerializeField]
        private List<PlayableCharacterSoundEffects> m_soundEffectsLists = new List<PlayableCharacterSoundEffects>();

        private List<PlayableCharacterSoundEffects> soundEffectsLists => m_soundEffectsLists;

        /// <summary>
        /// soundEffectsList‚ð•Ô‚·( get only)
        /// </summary>
        /// <returns></returns>
        public List<PlayableCharacterSoundEffects> GetParametersLists() { return soundEffectsLists; }
    }
}
