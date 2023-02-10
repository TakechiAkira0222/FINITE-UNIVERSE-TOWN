using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.SoundEffects
{
    [RequireComponent(typeof(CharacterKeyInputStateManagement))]
    public class CharacterBasicSoundEffectsStateManagement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStateManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== PlayableCharacterSoundEffects ===")]
        [SerializeField] private PlayableCharacterSoundEffects playableCharacterSoundEffects;

        #endregion

        #region  protected
        protected CharacterAddressManagement addressManagement => m_characterAddressManagement;
        protected CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        protected AudioSource myMainAudioSource => addressManagement.GetMyMainAudioSource();

        #endregion

        public AudioSource GetMainAudioSource() { return myMainAudioSource; }
        public bool GetIsPlaying() { return myMainAudioSource.isPlaying; }

        void Reset()
        {
            m_characterKeyInputStateManagement = this.GetComponent<CharacterKeyInputStateManagement>();
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
        }

        protected virtual void OnEnable()
        {
            keyInputStateManagement.InputToJump += (status, addressManagement) => { PlayOneShotJumpSound(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
        }
        
        protected virtual void OnDisable()
        {
            keyInputStateManagement.InputToJump -= (status, addressManagement) => { PlayOneShotJumpSound(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");
        }

        #region PlayOneShotFunction
        public void PlayOneShotJumpSound()
        {
            myMainAudioSource.PlayOneShot( playableCharacterSoundEffects.GetJumpSoundClip() , playableCharacterSoundEffects.GetJumpSoundClipVolume());
        }
        public void PlayOneShotConcreteFootstepsSound()
        {
            myMainAudioSource.PlayOneShot(playableCharacterSoundEffects.GetConcreteFootstepsSoundClip(), playableCharacterSoundEffects.GetConcreteFootstesSoundClipVolume());
        }

        #endregion
    }
}
