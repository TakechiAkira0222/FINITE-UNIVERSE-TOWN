using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior
{
    [Serializable]
    [CreateAssetMenu(fileName = "MechanicalWarreiorSpecificSoundEffects", menuName = "MechanicalWarreiorSpecificSoundEffects")]
    public class MechanicalWarreiorSpecificSoundEffects : ScriptableObject
    {
        #region SerializeField

        [SerializeField, Header("í èÌéÀåÇ")] private AudioClip m_normalShot;
        [SerializeField, Range(0f, 1f)] private float m_normalShotVolume = 0.5f;

        #endregion

        #region GetAudioClipFunction

        public AudioClip GetNormalShotClip() { return m_normalShot; }
        public float GetNormalShotVolume() { return m_normalShotVolume; }

        #endregion
    }
}
