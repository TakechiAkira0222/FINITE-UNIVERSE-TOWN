using UnityEngine;
using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class MechanicalWarriorHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== MechanicalWarriorAddressManagement === ")]
        [SerializeField] private MechanicalWarriorAddressManagement m_mechanicalWarriorAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_soundEffectsManagement;

        #endregion

        #region private variable
        private MechanicalWarriorAddressManagement addressManagement => m_mechanicalWarriorAddressManagement;
        private MechanicalWarriorStatusManagement mechanicalWarriorStatusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_soundEffectsManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private GameObject bulletsInstans => addressManagement.GetNormalBulletsInstans();
        private Transform  magazineTransfrom => addressManagement.GetMagazineTransfrom();
        private string bulletsPath => addressManagement.GetNormalBulletsPath();
        private float  force => mechanicalWarriorStatusManagement.GetNormalShootingForce();
        private float  durationTime => mechanicalWarriorStatusManagement.GetNormalDurationOfBullet();

        #endregion

        #region UnityAnimatorEvent
        /// <summary>
        /// FirstShot Animation Start
        /// </summary>
        void MechanicalWarriorFirstShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!myPhotonView.IsMine) return;
            Shooting(magazineTransfrom, force);
        }
        /// <summary>
        /// SecondShot Animation End
        /// </summary>
        void MechanicalWarriorSecondShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!myPhotonView.IsMine) return;
            Shooting(magazineTransfrom, force);
        }
        /// <summary>
        /// ThirdShot Animation End
        /// </summary>
        void MechanicalWarriorThirdShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!myPhotonView.IsMine) return;
            Shooting(magazineTransfrom, force);
        }

        #endregion

        #region Recursive function
        private void Shooting( Transform magazine, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( bulletsPath + bulletsInstans.name, magazine.position, Quaternion.identity);

            instans.GetComponent<Rigidbody>().AddForce( magazine.forward * force, ForceMode.Impulse);

            StartCoroutine(DelayMethod(durationTime, () => { PhotonNetwork.Destroy(instans); }));
        }
        private IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        #endregion
    }
}