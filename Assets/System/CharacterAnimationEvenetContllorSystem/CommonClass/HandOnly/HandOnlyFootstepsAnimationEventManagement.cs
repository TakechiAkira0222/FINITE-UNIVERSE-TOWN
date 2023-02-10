using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;


namespace Takechi.CharacterController.Footsteps
{
    public class HandOnlyFootstepsAnimationEventManagement : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== CharacterBasicSoundEffectsStateManagement ===")]
        [SerializeField] private CharacterBasicSoundEffectsStateManagement m_characterBasicSoundEffectsStateManagement;
        [Header("=== Script Setting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;

        #endregion

        #region private variable
        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private CharacterBasicSoundEffectsStateManagement soundEffectsStateManagement => m_characterBasicSoundEffectsStateManagement;
        private PhotonView thisPhotonView => m_thisPhotonView;

        #endregion

        #region unity event
        private void Reset()
        {
            m_thisPhotonView = this.GetComponent<PhotonView>();
        }

        #endregion

        #region UnityAnimatorEvent
        /// <summary>
        /// FootstepsSound
        /// </summary>
        private void FootstepsSound()
        {
            if (!statusManagement.GetIsGrounded()) return;

            soundEffectsStateManagement.PlayOneShotConcreteFootstepsSound();

            thisPhotonView.RPC(nameof(RPC_PlayOneShotConcreteFootstepsSound), RpcTarget.OthersBuffered);
        }
        #endregion

        [PunRPC]
        private void RPC_PlayOneShotConcreteFootstepsSound()
        {
            soundEffectsStateManagement.PlayOneShotConcreteFootstepsSound();
        }
    }
}
