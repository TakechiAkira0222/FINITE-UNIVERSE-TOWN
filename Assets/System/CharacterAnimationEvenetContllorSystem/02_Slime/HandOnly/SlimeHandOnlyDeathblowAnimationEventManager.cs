using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using UnityEngine;


namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class SlimeHandOnlyDeathblowAnimationEventManager : AnimationEventManagement
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
        private Animator networkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private Animator handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private Transform  attackLaserPointTransfrom => addressManagement.GetAttackLaserPointTransfrom();
        private string     attackEffectPath => addressManagement.GetAttackLaserEffectPath();
        private GameObject attackEffectInstans => addressManagement.GetAttackLaserEffectInstans();
        private bool isMine => myPhotonView.IsMine;
        private GameObject attackLaserGameObject;
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;

        #endregion

        public void SlimeDeathblowStart()
        {
            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            //if (!isMine) return;
            //attackLaserGameObject = Shooting( attackLaserPointTransfrom);
        }

        public void SlimeDeathblow()
        {
           
        }

        public void SlimeDeathblowEnd()
        {
            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            //if (!isMine) return;
            //PhotonNetwork.Destroy(attackLaserGameObject);
        }

        #region Recursive function
        private GameObject Shooting(Transform laserPoint) { return PhotonNetwork.Instantiate(attackEffectPath + attackEffectInstans.name, laserPoint.position, laserPoint.rotation); }
        #endregion
    }
}
