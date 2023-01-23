using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;

using UnityEngine;
using UnityEngine.Playables;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class MechanicalWarriorHandOnlyDeathblowAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== MechanicalWarriorAddressManagement === ")]
        [SerializeField] private MechanicalWarriorAddressManagement     m_mechanicalWarriorAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement      m_mechanicalWarriorStatusManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;

        #endregion

        #region private variable
        private MechanicalWarriorAddressManagement addressManagement => m_mechanicalWarriorAddressManagement;
        private MechanicalWarriorStatusManagement  statusManagement => m_mechanicalWarriorStatusManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private GameObject myAvater => addressManagement.GetMyAvater();
        private Animator   networkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private GameObject networkModelObject =>  addressManagement.GetNetworkModelObject();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject bulletsInstans => addressManagement.GetDeathblowBulletsInstans();
        private Transform  magazineTransfrom => addressManagement.GetMagazineTransfrom();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private string bulletsPath  => addressManagement.GetDeathblowBulletsPath();
        private float  force => statusManagement.GetDeathblowShootingForce();
        private float  durationTime => statusManagement.GetDeathblowDurationOfBullet();
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        #endregion

        private void Awake()
        {
            m_playableDirector.played  += Director_Played;
            m_playableDirector.stopped += Director_Stopped;
        }

        #region UnityAnimatorEvent
        /// <summary>
        /// Mechanical Warrior Deathblow Start
        /// </summary>
        void MechanicalWarriorDeathblowStart()
        {
            m_playableDirector.Play();

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight( networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight( networkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
        }

        /// <summary>
        /// Mechanical Warrior Deathblow Shot
        /// </summary>
        void MechanicalWarriorDeathblowShot()
        {
            if (!myPhotonView.IsMine) return;

            Shooting( magazineTransfrom, force);

            myRb.transform.Translate( 0, 0, -1.5f);
        }

        /// <summary>
        /// Mechanical Warrior Deathblow End
        /// </summary>
        void MechanicalWarriorDeathblowEnd()
        {
            // animation weiht
            SetLayerWeight( networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
        }

        #endregion

        private void Director_Played(PlayableDirector obj)
        {
            // rb
            statusManagement.SetIsKinematic(true);

            // operation
            keyInputStateManagement.SetOperation(false);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            if (statusManagement.photonView.IsMine)
            {
                handOnlyModelObject.SetActive(false);
                networkModelObject.SetActive(true);
            }
        }

        private void Director_Stopped(PlayableDirector obj)
        {
            if (statusManagement.photonView.IsMine)
            {
                handOnlyModelObject.SetActive(true);
                networkModelObject.SetActive(false);
            }

            // rb
            statusManagement.SetIsKinematic(false);

            // operation
            keyInputStateManagement.SetOperation(true);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);
        }

        #region Recursive function
        private void Shooting(Transform magazine, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( bulletsPath + bulletsInstans.name, magazine.position, Quaternion.identity);

            Rigidbody rb = instans.GetComponent<Rigidbody>();
            rb.AddForce( myAvater.transform.forward * force, ForceMode.Impulse);

            StartCoroutine( DelayMethod( durationTime, () => { PhotonNetwork.Destroy(instans); }));
        }

        #endregion
    }
}