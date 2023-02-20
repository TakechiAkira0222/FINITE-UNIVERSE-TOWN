using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificSoundEffects.Slime
{
    [Serializable]
    [CreateAssetMenu(fileName = "SlimeSpecificSoundEffects", menuName = "SlimeData/SlimeSpecificSoundEffects")]
    public class SlimeSpecificSoundEffects : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Header(" normal attack")] private AudioClip m_normalShoot;
        [SerializeField, Range(0f, 1f)] private float m_normalShootVolume = 0.5f;

        [SerializeField, Header(" Abilty Stan")] private AudioClip m_stan;
        [SerializeField, Range(0f, 1f)] private float m_stanVolume = 0.5f;

        [SerializeField, Header(" Abilty Trnado")] private AudioClip m_trnado;
        [SerializeField, Range(0f, 1f)] private float m_trnadoVolume = 0.5f;

        [SerializeField, Header(" Abilty Trransparency")] private AudioClip m_trransparency;
        [SerializeField, Range(0f, 1f)] private float m_trransparencyVolume = 0.5f;

        [SerializeField, Header(" Deathblow Instantiate")] private AudioClip m_deathblowInstantiate;
        [SerializeField, Range(0f, 1f)] private float m_deathblowInstantiateVolume = 0.5f;


        #endregion

        #region GetAudioClipFunction
        public AudioClip GetNormalShootClip() => m_normalShoot;  
        public AudioClip GetStanClip() => m_stan;
        public AudioClip GetTrnadoClip() => m_trnado;
        public AudioClip GetTrransparencyClip() => m_trransparency;
        public AudioClip GetDeathblowInstantiateClip() => m_deathblowInstantiate;

        public float GetNormalShootVolume() => m_normalShootVolume;
        public float GetStanVolume() => m_stanVolume;
        public float GetTrnadoVolume() => m_trnadoVolume;
        public float GetTrransparencyVolume() => m_trransparencyVolume;
        public float GetDeathblowInstantiateVolume() => m_deathblowInstantiateVolume;


        #endregion
    }
}