using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.SoundEffects;

namespace Takechi.ManagmentButton.AudioSourceCallback
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonAudioSourceCallbackManagemant : MonoBehaviour
    {
        [Header(" === BasicUiSoundEffects ===")]
        [SerializeField] BasicUiSoundEffects m_basicUiSound;
        [Header(" this.AudioSource ")]
        [SerializeField] AudioSource m_audioSource;

        private void Reset()
        {
            m_audioSource = this.transform.GetComponent<AudioSource>();
        }

        public void OnDecisionSound_OneShot() 
        {
            m_audioSource.PlayOneShot(m_basicUiSound.GetDecisionSoundClip(), m_basicUiSound.GettDecisionSoundVolume());
            Debug.Log(" <color=green>ButtonAudioSourceCallbackManagemant</color>.<color=yellow>OnDecisionSound_OneShot</color>()");
        }

        public void OnCancelSound_OneShot()
        {
            m_audioSource.PlayOneShot(m_basicUiSound.GetCancelSoundClip(), m_basicUiSound.GetCancelSoundVolume());
            Debug.Log(" <color=green>ButtonAudioSourceCallbackManagemant</color>.<color=yellow>OnCancelSound_OneShot</color>()");
        }

        public void OnCursorSelectSoundClip_OneShot()
        {
            m_audioSource.PlayOneShot( m_basicUiSound.GetCursorSelectSoundClip(), m_basicUiSound.GetCursorSelectSoundVolume());
            Debug.Log(" <color=green>ButtonAudioSourceCallbackManagemant</color>.<color=yellow>OnCursorSelectSoundClip_OneShot</color>()");
        }
    }
}
