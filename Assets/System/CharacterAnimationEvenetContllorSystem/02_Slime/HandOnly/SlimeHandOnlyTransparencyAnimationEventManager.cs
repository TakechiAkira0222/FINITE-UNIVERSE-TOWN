using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TakechiEngine.PUN;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine.Rendering;
using System.ComponentModel;

namespace Takechi.CharacterController.AnimationEvent
{
    public class SlimeHandOnlyTransparencyAnimationEventManager : TakechiPunCallbacks
    {
        #region SerializeField
        [Header("=== SlimeAddressManagement === ")]
        [SerializeField] private SlimeAddressManagement m_slimeAddressManagement;
        [Header("=== SlimeStatusManagement ===")]
        [SerializeField] private SlimeStatusManagement m_slimeStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private SlimeSoundEffectsManagement m_slimeSoundEffectsManagement;

        #endregion

        #region private variable
        private SlimeAddressManagement addressManagement => m_slimeAddressManagement;
        private SlimeStatusManagement  statusManagement =>  m_slimeStatusManagement;
        private SlimeSoundEffectsManagement soundEffectsManagement => m_slimeSoundEffectsManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Collider   myCollider   => addressManagement.GetMyCollider();
        private GameObject networkModelObject => addressManagement.GetNetworkModelObject();
        private int  duration_seconds  => statusManagement.GetTransparencyDsuration_Swconds();
        private bool isMine => myPhotonView.IsMine;

        #endregion

        private void Awake()
        {
            
        }

        void SlimeTransparencyStart()
        {
           
        }

        void SlimeTransparency()
        {
            if (!isMine)
            {
                networkModelObject.SetActive(false);
                StartCoroutine(DelayMethod(duration_seconds, () => { networkModelObject.SetActive(true); }));
            }
        }

        void SlimeTransparencyEnd()
        {
           
        }
    }
}

