using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;

namespace Takechi.GameManagerSystem.SoundEffects
{
    [RequireComponent(typeof(AudioSource))]
    public class GameManagmentBasicSoundEffectsManagement : MonoBehaviour
    {
        [SerializeField] AudioSource m_thisAudioSource;

        protected AudioSource myMainAudioSource => m_thisAudioSource;

        private void Reset()
        {
            m_thisAudioSource = this.GetComponent<AudioSource>();
        }

        protected virtual void OnEnable()
        {
        
        }

        protected virtual void OnDisable()
        {
         
        }
    }
}
