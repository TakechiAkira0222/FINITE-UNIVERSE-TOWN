using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Takechi.CharacterController.SoundEffects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayableCharacterSoundEffects", menuName = "PlayableCharacterSoundEffects")]
    public class PlayableCharacterSoundEffects : ScriptableObject
    {
        #region SerializeField

        [SerializeField, Tooltip("ƒWƒƒƒ“ƒv")]
        private AudioClip m_jumpSoundClip;
        [SerializeField, Range(0f, 1f)] private float m_jumpSoundClipVolume = 0.5f;

        #endregion

        #region GetFunction

        public AudioClip GetJumpSoundClip() { return m_jumpSoundClip; }
        public float GetJumpSoundClipVolume() { return m_jumpSoundClipVolume; }
       
        #endregion
    }
}