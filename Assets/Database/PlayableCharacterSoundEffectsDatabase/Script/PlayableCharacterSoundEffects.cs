using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

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
        private float m_jumpSoundVolume = 0.5f;
        [SerializeField, Tooltip(" 足音　コンクリート")]
        private AudioClip m_concreteFootstepsSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_concreteFootstepsSoundVolume = 0.5f;
        [SerializeField, Tooltip(" 被ダメ")]
        private AudioClip m_takenDamageSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_takenDamageSoundVolume = 0.5f;
        [SerializeField, Tooltip("死亡")]
        private AudioClip m_deathSoundClip;
        [SerializeField, Range(0f, 1f)]
        private float m_deathSoundVolume = 0.5f;

        #endregion

        #region GetFunction

        public AudioClip GetJumpSoundClip() => m_jumpSoundClip; 
        public AudioClip GetConcreteFootstepsSoundClip() => m_concreteFootstepsSoundClip; 
        public AudioClip GetTakenDamageSoundClip() => m_takenDamageSoundClip;
        public AudioClip GetDeathSoundClip() => m_deathSoundClip;
        public float GetJumpSoundVolume() => m_jumpSoundVolume; 
        public float GetConcreteFootstesSoundVolume() => m_concreteFootstepsSoundVolume;
        public float GetTakenDamageSoundVolume() => m_takenDamageSoundVolume;
        public float GetDeathSoundVolume() => m_deathSoundVolume;

        #endregion
    }
}