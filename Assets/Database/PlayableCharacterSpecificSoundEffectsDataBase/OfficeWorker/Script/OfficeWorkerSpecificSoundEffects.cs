using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.CharacterController.SpecificSoundEffects.OfficeWorker
{
    [Serializable]
    [CreateAssetMenu(fileName = "OfficeWorkerSpecificSoundEffects", menuName = "OfficeWorkerSpecificSoundEffects")]
    public class OfficeWorkerSpecificSoundEffects : ScriptableObject
    {
        #region SerializeField

        [SerializeField, Header("í èÌçUåÇ1ÇÃê∫")] private AudioClip m_voiceOfFirstNormalAttack;
        [SerializeField, Range( 0f,1f)] private float m_voiceOfFirstNormalAttackVolume = 0.5f;

        [SerializeField, Header("í èÌçUåÇ2ÇÃê∫")] private AudioClip m_voiceOfSecondNormalAttack;
        [SerializeField, Range( 0f,1f)] private float m_voiceOfSecondNormalAttackVolume = 0.5f;

        [SerializeField, Header("ïKéEãZ ã©Ç—")] private AudioClip m_voiceOfCallOut;
        [SerializeField, Range( 0f,1f)] private float m_voiceOfCallOutVolume = 0.5f;

        [SerializeField, Header("ïKéEãZÅ@énÇ‹ÇËÇÃê∫")] private AudioClip m_voiceOfDeathblowStart;
        [SerializeField, Range( 0f, 1f)] private float m_voiceOfDeathblowStartVolume = 0.5f;

        [SerializeField, Header("ïKéEãZÅ@ÉpÉèÅ[ÉAÉbÉv")] private AudioClip m_deathblowPowerUp;
        [SerializeField, Range( 0f, 1f)] private float m_deathblowPowerUpVolume = 0.5f;

        [SerializeField, Header("í èÌçUåÇ1")] private AudioClip m_firstNormalAttack;
        [SerializeField, Range( 0f, 1f)] private float m_firstNormalAttackVolume = 0.5f;

        [SerializeField, Header("í èÌçUåÇ2")] private AudioClip m_secondNormalAttack;
        [SerializeField, Range( 0f, 1f)] private float m_secondNormalAttackVolume = 0.5f;

        #endregion

        #region GetAudioClipFunction
        public AudioClip GetVoiceOfFirstNormalAttackClip()  { return m_voiceOfFirstNormalAttack; }
        public AudioClip GetVoiceOfSecondNormalAttackClip() { return m_voiceOfSecondNormalAttack; }
        public AudioClip GetVoiceOfCallOutClip() { return m_voiceOfCallOut; }
        public AudioClip GetVoiceOfDeathblowStartClip() { return m_voiceOfDeathblowStart; }
        public AudioClip GetFirstNormalAttackClip()  { return m_firstNormalAttack; }
        public AudioClip GetSecondNormalAttackClip() { return m_secondNormalAttack; }
        public AudioClip GetDeathblowPowerUpClip() { return m_deathblowPowerUp; }

        public float GetVoiceOfFirstNormalAttackVolume() { return m_voiceOfFirstNormalAttackVolume; }
        public float GetVoiceOfSecondNormalAttackVolume() { return m_voiceOfSecondNormalAttackVolume; }
        public float GetVoiceOfCallOutVolume() { return m_voiceOfCallOutVolume; }
        public float GetVoiceOfDeathblowStartVolume() { return m_voiceOfDeathblowStartVolume; }
        public float GetFirstNormalAttackVolume()  { return m_firstNormalAttackVolume; }
        public float GetSecondNormalAttackVolume() { return m_secondNormalAttackVolume; }
        public float GetDeathblowPowerUpVolume()   { return m_deathblowPowerUpVolume; }
        #endregion
    }
}
