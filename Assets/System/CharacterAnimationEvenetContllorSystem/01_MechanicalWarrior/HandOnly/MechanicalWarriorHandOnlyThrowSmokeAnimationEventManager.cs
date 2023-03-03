using UnityEngine;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.ThrowSmokeAnimationEvent
{
    public class MechanicalWarriorHandOnlyThrowSmokeAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== MechanicalWarriorAddressManagement === ")]
        [SerializeField] private MechanicalWarriorAddressManagement m_mechanicalWarriorAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement  m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_mechanicalWarriorSoundEffectsManagement;

        #endregion

        #region private variable
        private MechanicalWarriorAddressManagement addressManagement => m_mechanicalWarriorAddressManagement;
        private MechanicalWarriorStatusManagement  statusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_mechanicalWarriorSoundEffectsManagement;
        private GameObject networkModelAssaultRifle  =>  addressManagement.GetNetworkModelAssaultRifleObject();
        private GameObject handOnlyModelAssaultRifle =>  addressManagement.GetHandOnlyModelAssaultRifleObject();
        private GameObject throwInstans => addressManagement.GetthrowSmokeInstans();
        private Transform  throwTransfrom => addressManagement.GetThrowSmokeInstantiateTransfrom();
        private string throwInstansPath => addressManagement.GetThrowSmokePath();
        private float  force => statusManagement.GetThrowSmokeForce();
        private float  smokeDuration_Seconds => statusManagement.GetSmokeDuration_Seconds();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private bool   isMine => myPhotonView.IsMine;

        #endregion

        /// <summary>
        /// MechanicalWarrior ThrowSmoke Start
        /// </summary>
        void MechanicalWarriorThrowSmokeStart()
        {
            // assaultRifle
            setAssaultRifleSetActive(false);

            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);
        }

        /// <summary>
        /// MechanicalWarrior ThrowSmoke Throw
        /// </summary>
        void MechanicalWarriorThrowSmokeThrow()
        {
            if (!isMine) return;
            throwing(throwTransfrom, force);
        }

        /// <summary>
        /// MechanicalWarrior ThrowSmoke End
        /// </summary>
        void MechanicalWarriorThrowSmokeEnd()
        {
            // assaultRifle
            setAssaultRifleSetActive(true);

            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
        }

        #region private finction
        private void setAssaultRifleSetActive(bool flag)
        {
            handOnlyModelAssaultRifle.SetActive(flag);
            networkModelAssaultRifle.SetActive(flag);
        }

        private void throwing(Transform throwTransform, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( throwInstansPath + throwInstans.name, throwTransform.position, Quaternion.identity);

            instans.transform.Rotate( new Vector3( 0, 0, 60f));
            instans.GetComponent<Rigidbody>().AddForce(( throwTransform.forward + ( throwTransform.up / 10)) * force, ForceMode.Impulse);

            StartCoroutine(DelayMethod( smokeDuration_Seconds, () => { PhotonNetwork.Destroy(instans); }));
        }

        #endregion
    }
}
