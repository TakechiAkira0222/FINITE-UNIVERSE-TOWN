using UnityEngine;
using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using System.IO;
using System;
using Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class MechanicalWarriorHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField

        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_soundEffectsManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;

        [SerializeField] private GameObject m_instans;
        [SerializeField] private Transform  m_magazineTransfrom;
     
        #endregion

        #region private
        private PhotonView thisPhotonView => m_thisPhotonView;
        private MechanicalWarriorStatusManagement mechanicalWarriorStatusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_soundEffectsManagement;
        private float force => m_mechanicalWarriorStatusManagement.GetShootingForce();
        private float durationTime => m_mechanicalWarriorStatusManagement.GetDurationOfBullet();

        private string path => m_mechanicalWarriorStatusManagement.GetBulletsPath();


        #endregion

        private void Awake()
        {
            
        }

        private void Reset()
        {
            m_thisPhotonView = this.transform.GetComponent<PhotonView>();
        }

        #region UnityAnimatorEvent

        /// <summary>
        /// FirstShot Animation Start
        /// </summary>
        void MechanicalWarriorFirstShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!mechanicalWarriorStatusManagement.GetMyPhotonView().IsMine) return;

            Shooting(m_magazineTransfrom, force);
        }

        /// <summary>
        /// SecondShot Animation End
        /// </summary>
        void MechanicalWarriorSecondShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!mechanicalWarriorStatusManagement.GetMyPhotonView().IsMine) return;

            Shooting(m_magazineTransfrom, force);
        }

        /// <summary>
        /// ThirdShot Animation End
        /// </summary>
        void MechanicalWarriorThirdShot()
        {
            soundEffectsManagement.PlayOneShotNormalShot();

            if (!mechanicalWarriorStatusManagement.GetMyPhotonView().IsMine) return;
            Shooting(m_magazineTransfrom, force);
        }

        #endregion

        #region set Function

        #endregion

        #region Recursive function

        private void Shooting( Transform magazine, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( path + m_instans.name, magazine.position, Quaternion.identity);

            instans.GetComponent<Rigidbody>().AddForce( magazine.forward * 100 * force);

            StartCoroutine(DelayMethod( durationTime, () =>
            {
                PhotonNetwork.Destroy(instans);
            }));
        }

        private IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        #endregion
    }
}