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
        [SerializeField, Tooltip("�W�����v")]
        private AudioClip m_jumpSoundClip;

        #endregion

        #region GetFunction

        /// <summary>
        /// �W�����v�̉�
        /// </summary>
        public AudioClip GetJumpSoundClip() { return m_jumpSoundClip; }
       
        #endregion
    }
}