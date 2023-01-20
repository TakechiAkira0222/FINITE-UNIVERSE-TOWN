using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.EnemySearchAnimationEvent
{
    public class MechanicalWarriorHandOnlyEnemySearchAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_mechanicalWarriorSoundEffectsManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private MechanicalWarriorStatusManagement statusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_mechanicalWarriorSoundEffectsManagement;
        private Animator   handNetworkModelAnimator =>  addressManagement.GetNetworkModelAnimator();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private string playerCharacterPrefabTag => SearchForPrefabTag.playerCharacterPrefabTag;
        private string characterModelPrefabName => SearchForPrefabName.characterModelPrefabName;
        private bool  IsMine => myPhotonView.IsMine;
        private float enemySearch_Seconds => statusManagement.GetEnemySearch_Seconds();
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_weightTemporaryComplement = 0;
        #endregion

        /// <summary>
        /// MechanicalWarrior EnemySearch Start
        /// </summary>
        void MechanicalWarriorEnemySearchStart()
        {
            // operation
            keyInputStateManagement.SetOperation(false);

            // animation weiht
            m_weightTemporaryComplement = handNetworkModelAnimator.GetLayerWeight(handNetworkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            controllerReferenceManagement.GetIKAnimationController().SetAllIkWeight(0);

            // enemySearch
            onEnemySearch();
            StartCoroutine(DelayMethod( enemySearch_Seconds, () => { resetEnemySearch(); }));
        }
        /// <summary>
        /// MechanicalWarrior EnemySearch End
        /// </summary>
        void MechanicalWarriorEnemySearchEnd()
        {
            // operation
            keyInputStateManagement.SetOperation(true);

            // animation weiht
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, m_weightTemporaryComplement);
            controllerReferenceManagement.GetIKAnimationController().ResetAllIkWeight();
        }

        private void onEnemySearch()
        {
            if (!IsMine) return;

            foreach (GameObject o in GameObject.FindGameObjectsWithTag(playerCharacterPrefabTag))
            {
                Outline outline = o.transform.Find(characterModelPrefabName).GetComponent<Outline>();
                outline.OutlineMode = Outline.Mode.SilhouetteOnly;
            }
        }

        private void resetEnemySearch()
        {
            if (!IsMine) return;

            foreach (GameObject o in GameObject.FindGameObjectsWithTag(playerCharacterPrefabTag))
            {
                Outline outline = o.transform.Find(characterModelPrefabName).GetComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineVisible;
            }
        }
    }
}
