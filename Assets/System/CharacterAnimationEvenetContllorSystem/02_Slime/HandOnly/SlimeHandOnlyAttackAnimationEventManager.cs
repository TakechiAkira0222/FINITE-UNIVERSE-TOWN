using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;


namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class SlimeHandOnlyAttackAnimationEventManager : MonoBehaviour
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
        private GameObject attackEffect => addressManagement.GetAttackLaserEffectObject();
        //private GameObject bulletsInstans => addressManagement.GetNormalBulletsInstans();
        //private Transform magazineTransfrom => addressManagement.GetMagazineTransfrom();

        #endregion

        public void SlimeAttackStart()
        {
            attackEffect.SetActive(true);
            Debug.Log("");
        }

        public void SlimeAttackEnd()
        {
            attackEffect.SetActive(false);
        }
    }
}
