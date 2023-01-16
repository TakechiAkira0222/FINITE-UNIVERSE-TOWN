using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificSoundEffects.OfficeWorker
{
    [Serializable]
    [CreateAssetMenu(fileName = "OfficeWorkerSpecificSoundEffects", menuName = "OfficeWorkerSpecificSoundEffects")]
    public class OfficeWorkerSpecificSoundEffects : ScriptableObject
    {
        [SerializeField] private AudioClip m_voiceOfNormalAttack;

        #region GetStatusFunction

        public AudioClip GetVoiceOfNormalAttack() { return m_voiceOfNormalAttack; }
        #endregion
    }
}
