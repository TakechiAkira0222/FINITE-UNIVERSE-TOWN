using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;

using UnityEngine;
using UnityEngine.Playables;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class MechanicalWarriorHandOnlyDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== MechanicalWarriorAddressManagement === ")]
        [SerializeField] private MechanicalWarriorAddressManagement     m_mechanicalWarriorAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement      m_mechanicalWarriorStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement       m_characterKeyInputStateManagement;
        [Header("=== CharacterControllerReferenceManagement ===")]
        [SerializeField] private CharacterControllerReferenceManagement m_characterControllerReferenceManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;

        #endregion

        #region private variable
        private MechanicalWarriorAddressManagement addressManagement => m_mechanicalWarriorAddressManagement;
        private MechanicalWarriorStatusManagement  statusManagement => m_mechanicalWarriorStatusManagement;
        private CharacterKeyInputStateManagement   keyInputStateManagement => m_characterKeyInputStateManagement;
        private CharacterControllerReferenceManagement controllerReferenceManagement => m_characterControllerReferenceManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Animator   handNetworkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private GameObject myAvater => addressManagement.GetMyAvater();
        private GameObject handOnlyModelObject => addressManagement.GetHandOnlyModelObject();
        private GameObject handNetworkModelObject => addressManagement.GetNetworkModelObject();
        private GameObject bulletsInstans => addressManagement.GetDeathblowBulletsInstans();
        private Transform  magazineTransfrom => addressManagement.GetMagazineTransfrom();
        private Rigidbody  myRb => addressManagement.GetMyRigidbody();
        private string bulletsPath  => addressManagement.GetDeathblowBulletsPath();
        private float  force => statusManagement.GetDeathblowShootingForce();
        private float  durationTime => statusManagement.GetDeathblowDurationOfBullet();
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_weightTemporaryComplement = 0;
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
            m_weightTemporaryComplement = handNetworkModelAnimator.GetLayerWeight(handNetworkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight( handNetworkModelAnimator, AnimatorLayers.overrideLayer, 0f);
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
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, m_weightTemporaryComplement);
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
                handNetworkModelObject.SetActive(true);
            }
        }

        private void Director_Stopped(PlayableDirector obj)
        {
            if (statusManagement.photonView.IsMine)
            {
                handOnlyModelObject.SetActive(true);
                handNetworkModelObject.SetActive(false);
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
        private void SetLayerWeight(Animator animator, string layerName, float weight)
        {
            int LayerIndex = animator.GetLayerIndex(layerName);
            animator.SetLayerWeight(LayerIndex, weight);
            Debug.Log($" handNetworkModelAnimator.<color=yellow>SetLayerWeight</color>({LayerIndex}, {weight}); ");
        }
        private IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        #endregion
    }
}