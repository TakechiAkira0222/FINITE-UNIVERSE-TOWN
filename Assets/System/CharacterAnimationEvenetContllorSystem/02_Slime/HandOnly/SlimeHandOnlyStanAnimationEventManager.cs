using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using TakechiEngine.PUN;
using UnityEngine;

namespace Takechi.CharacterController.AnimationEvent
{
    public class SlimeHandOnlyStanAnimationEventManager : AnimationEventManagement
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
        private SlimeStatusManagement  statusManagement => m_slimeStatusManagement;
        private SlimeSoundEffectsManagement soundEffectsManagement => m_slimeSoundEffectsManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private GameObject stanEffect1 => addressManagement.GetStanEffectObjectArray()[0];
        private GameObject stanEffect2 => addressManagement.GetStanEffectObjectArray()[1];
        private Collider myCollider => addressManagement.GetMyCollider();

        #endregion

        private void Awake()
        {
           // Physics.IgnoreCollision(stanEffect2.GetComponent<Collider>(), myCollider, false);
        }

        public void SlimeStanStart()
        {
            stanEffect1.SetActive(true);
            soundEffectsManagement.PlayOneShotStan();
        }

        public void SlimeStan()
        {
            stanEffect1.SetActive(false);
            stanEffect2.SetActive(true);
        }

        public  void SlimeStanEnd()
        {
            stanEffect2.SetActive(false);
        }
    }
}

