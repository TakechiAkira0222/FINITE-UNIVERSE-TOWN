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
    public class SlimeHandOnlyTrnadoAnimationEventManager : TakechiPunCallbacks
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
        private GameObject trnadoEffect => addressManagement.GetTrnadoEffectObject();
        private float duration_seconds =>  statusManagement.GetTrnadoEffectDsuration_Seconds();
        private Collider myCollider => addressManagement.GetMyCollider();

        #endregion

        private void Awake()
        {
            Physics.IgnoreCollision(trnadoEffect.GetComponent<Collider>(), myCollider, false);
        }

        public void SlimeTrnadoStart()
        {
            trnadoEffect.SetActive(true);
            StartCoroutine( DelayMethod( duration_seconds, () => { trnadoEffect.SetActive(false); } ));
        }

        public  void SlimeTrnadoEnd()
        {
           
        }
    }
}

