using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Takechi.CharacterController.SoundEffects
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlayableCharacterSoundEffects", menuName = "PlayableCharacterData/PlayableCharacterSoundEffects")]
    public class PlayableCharacterSoundEffects : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Tooltip("ジャンプ")]
        private AudioClip m_jumpSoundClip;
        [SerializeField, Range(0f, 1f)] 
        private float m_jumpSoundClipVolume = 0.5f;
        [SerializeField, Tooltip(" 足音　コンクリート")]
        private AudioClip m_concreteFootstepsSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_concreteFootstepsSoundClipVolume = 0.5f; 

        #endregion

        #region GetFunction

        public AudioClip GetJumpSoundClip() { return m_jumpSoundClip; }
        public AudioClip GetConcreteFootstepsSoundClip() { return m_concreteFootstepsSoundClip; }
        public float GetJumpSoundClipVolume() { return m_jumpSoundClipVolume; }
        public float GetConcreteFootstesSoundClipVolume() { return m_concreteFootstepsSoundClipVolume; }

        #endregion
    }
}