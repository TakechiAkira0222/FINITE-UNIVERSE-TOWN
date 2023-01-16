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
        [SerializeField, Tooltip("ジャンプ")]
        private AudioClip m_jumpSoundClip;

        #endregion

        #region GetFunction

        /// <summary>
        /// ジャンプの音
        /// </summary>
        public AudioClip GetJumpSoundClip() { return m_jumpSoundClip; }
       
        #endregion
    }
}