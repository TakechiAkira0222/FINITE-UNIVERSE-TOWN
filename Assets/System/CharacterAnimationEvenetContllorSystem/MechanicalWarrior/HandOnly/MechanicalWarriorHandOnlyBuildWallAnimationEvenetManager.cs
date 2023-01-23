using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.BuildWallAnimationEvent
{
    public class MechanicalWarriorHandOnlyBuildWallAnimationEvenetManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== MechanicalWarriorAddressManagement === ")]
        [SerializeField] private MechanicalWarriorAddressManagement m_mechanicalWarriorAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_mechanicalWarriorSoundEffectsManagement;

        #endregion

        #region private variable
        private MechanicalWarriorAddressManagement addressManagement => m_mechanicalWarriorAddressManagement;
        private MechanicalWarriorStatusManagement statusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_mechanicalWarriorSoundEffectsManagement;
        private GameObject networkModelAssaultRifle =>  addressManagement.GetNetworkModelAssaultRifleObject();
        private GameObject handOnlyModelAssaultRifle => addressManagement.GetHandOnlyModelAssaultRifleObject();
        private Animator   networkModelAnimator =>  addressManagement.GetNetworkModelAnimator();
        private Animator   handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Transform  wallInstantiateTransform => addressManagement.GetWallInstantiateTransfrom();
        private GameObject wallInstans =>  addressManagement.GetWallInstans();
        private string     wallPath => addressManagement.GetWallPath();
        private float      wallDuration_Seconds => statusManagement.GetWallDuration_Seconds();

        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;
        #endregion

        #region Unity Animation Event
        /// <summary>
        /// Mechanical Warior BuildWall Start
        /// </summary>
        public void MechanicalWarriorBuildWallStart()
        {
            // operation
            keyInputStateManagement.SetOperation(false);

            // assaultRifle
            setAssaultRifleSetActive(false);

            // animation weiht
            m_networkModelAnimatorWeight  = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator,  AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);
            
            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);

        }
        /// <summary>
        /// Mechanical Warior BuildWall End
        /// </summary>
        public void MechanicalWarriorBuildWallEnd()
        {
            // operation
            keyInputStateManagement.SetOperation(true);

            // assaultRifle
            setAssaultRifleSetActive(true);

            // animation weiht
            SetLayerWeight(networkModelAnimator,  AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            // ik weiht
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();

            if (!myPhotonView.IsMine) return;

            WallInstantiate( wallInstantiateTransform , wallDuration_Seconds);
        }

        #endregion

        #region private finction
        private void setAssaultRifleSetActive(bool flag)
        {
            handOnlyModelAssaultRifle.SetActive(flag);
            networkModelAssaultRifle.SetActive(flag);
        }

        private void WallInstantiate( Transform wallTransform, float time)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( wallPath + wallInstans.name, wallTransform.position, new Quaternion( 0, wallTransform.rotation.y, 0, wallTransform.rotation.w));
            StartCoroutine(DelayMethod( time, () => { PhotonNetwork.Destroy(instans); }));
        }

        #endregion
    }
}
