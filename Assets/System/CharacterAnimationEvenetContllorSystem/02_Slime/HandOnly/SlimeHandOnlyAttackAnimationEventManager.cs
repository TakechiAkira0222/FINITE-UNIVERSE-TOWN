using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine;


namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class SlimeHandOnlyAttackAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== SlimeAddressManagement === ")]
        [SerializeField] private SlimeAddressManagement m_slimeAddressManagement;
        [Header("=== SlimeStatusManagement ===")]
        [SerializeField] private SlimeStatusManagement  m_slimeStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private SlimeSoundEffectsManagement m_slimeSoundEffectsManagement;

        #endregion

        #region private variable
        private SlimeAddressManagement addressManagement => m_slimeAddressManagement;
        private SlimeStatusManagement  statusManagement => m_slimeStatusManagement;
        private SlimeSoundEffectsManagement soundEffectsManagement => m_slimeSoundEffectsManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Transform  attackLaserPointTransfrom => addressManagement.GetAttackLaserPointTransfrom();
        private string     attackEffectPath => addressManagement.GetAttackLaserEffectPath();
        private GameObject attackEffectInstans => addressManagement.GetAttackLaserEffectInstans();
        private bool isMine => myPhotonView.IsMine;

        private GameObject attackLaserGameObject;
        #endregion

        public void SlimeAttackStart()
        {
            if (!isMine) return;
            attackLaserGameObject = Shooting( attackLaserPointTransfrom);
        }

        public void SlimeAttackEnd()
        {
            if (!isMine) return;
            PhotonNetwork.Destroy(attackLaserGameObject);
        }

        #region Recursive function
        private GameObject Shooting(Transform laserPoint) { return PhotonNetwork.Instantiate(attackEffectPath + attackEffectInstans.name, laserPoint.position, laserPoint.rotation); }
        #endregion
    }
}
