using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Takechi.CharacterController.SoundEffects
{
    [Serializable]
    [CreateAssetMenu(fileName = "BasicUiSoundEffects", menuName = "BasicUiSoundEffects")]
    public class BasicUiSoundEffects : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Tooltip("Œˆ’è‰¹")] private AudioClip m_decisionSoundClip;
        [SerializeField, Range(0f, 1f)] private float m_decisionSoundVolume = 0.5f;

        [SerializeField, Tooltip("ƒLƒƒƒ“ƒZƒ‹‰¹")] private AudioClip m_cancelSoundClip;
        [SerializeField, Range(0f, 1f)] private float m_cancelSoundVolume = 0.5f;

        [SerializeField, Tooltip("Cursor‘I‘ð‰¹")] private AudioClip m_cursorSelectSoundClip;
        [SerializeField, Range(0f, 1f)] private float m_cursorSelectSoundVolume = 0.5f;
        #endregion

        #region GetFunction

        public AudioClip GetDecisionSoundClip() { return m_decisionSoundClip; }
        public AudioClip GetCancelSoundClip() { return m_cancelSoundClip; }
        public AudioClip GetCursorSelectSoundClip() { return m_cursorSelectSoundClip; }

        public float GettDecisionSoundVolume() { return m_decisionSoundVolume; }
        public float GetCancelSoundVolume() { return m_cancelSoundVolume; }
        public float GetCursorSelectSoundVolume() { return m_cursorSelectSoundVolume; }

        #endregion
    }
}