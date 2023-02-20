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
    public class SlimeHandOnlyTrnadoAnimationEventManager : AnimationEventManagement
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
        private bool isTrnad = false;

        #endregion

        #region unity event
        private void Awake()
        {
            Physics.IgnoreCollision(trnadoEffect.GetComponent<Collider>(), myCollider, false);
        }
        private void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => { reesetTrnadoState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>reesetTrnadoState</color>() <color=green>to add.</color>");
        }
        private void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => { reesetTrnadoState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>reesetTrnadoState</color>() <color=green>to remove.</color>");
        }

        #endregion

        #region unity animation event
        public void SlimeTrnadoStart()
        {
            soundEffectsManagement.PlayOneShotTrnado();
            if (isTrnad) return;

            OnTrnado();
            StartCoroutine(DelayMethod(duration_seconds, () => { reesetTrnadoState(); }));
        }

        public  void SlimeTrnadoEnd()
        {

        }

        #endregion

        #region private function
        private void OnTrnado()
        {
            isTrnad = true;
            trnadoEffect.SetActive(true);
        }

        #endregion

        #region  reset function

        private void reesetTrnadoState()
        {
            isTrnad = false;
            trnadoEffect.SetActive(false);
        }

        #endregion
    }
}

