using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.SoundEffects
{
    [RequireComponent(typeof(CharacterKeyInputStateManagement))]
    public class CharacterBasicSoundEffectsStateManagement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStateManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== PlayableCharacterSoundEffects ===")]
        [SerializeField] private PlayableCharacterSoundEffects playableCharacterSoundEffects;

        #endregion

        #region  protected
        protected CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        protected CharacterKeyInputStateManagement characterKeyInputStateManagement => m_characterKeyInputStateManagement;

        #endregion

        void Reset()
        {
            m_characterKeyInputStateManagement = this.GetComponent<CharacterKeyInputStateManagement>();
            m_characterStatusManagement = this.GetComponent<CharacterStatusManagement>();
        }

        protected virtual void OnEnable()
        {
            m_characterKeyInputStateManagement.InputToJump += (c_status) => { PlayOneShotJumpSound(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
        }
        
        protected virtual void OnDisable()
        {
            m_characterKeyInputStateManagement.InputToJump -= (c_status) => { PlayOneShotJumpSound(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to remove.</color>");
        }

        #region PlayOneShotFunction
        public void PlayOneShotJumpSound()
        { 
            characterStatusManagement.GetMyMainAudioSource().PlayOneShot( playableCharacterSoundEffects.GetJumpSoundClip() , playableCharacterSoundEffects.GetJumpSoundClipVolume());
        }

        #endregion
    }
}
