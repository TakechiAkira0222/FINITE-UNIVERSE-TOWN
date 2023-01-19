using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Reference;
using UnityEngine;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.Ablity1AnimationEvent
{
    public class MechanicalWarriorHandOnlyAblity1AnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== MechanicalWarriorStatusManagement ===")]
        [SerializeField] private MechanicalWarriorStatusManagement m_mechanicalWarriorStatusManagement;
        [Header("=== MechanicalWarriorSoundEffectsManagement ===")]
        [SerializeField] private MechanicalWarriorSoundEffectsManagement m_mechanicalWarriorSoundEffectsManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== CharacterControllerReferenceManagement ===")]
        [SerializeField] private CharacterControllerReferenceManagement m_characterControllerReferenceManagement;

        #endregion

        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private MechanicalWarriorStatusManagement statusManagement => m_mechanicalWarriorStatusManagement;
        private MechanicalWarriorSoundEffectsManagement soundEffectsManagement => m_mechanicalWarriorSoundEffectsManagement;
        private CharacterKeyInputStateManagement keyInputStateManagement => m_characterKeyInputStateManagement;
        private CharacterControllerReferenceManagement controllerReferenceManagement => m_characterControllerReferenceManagement;
        private Animator handNetworkModelAnimator => addressManagement.GetNetworkModelAnimator();
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private string playerCharacterPrefabTag => SearchForPrefabTag.playerCharacterPrefabTag;
        private string characterModelPrefabName => SearchForPrefabName.characterModelPrefabName;
        private bool IsMine => myPhotonView.IsMine;
        private float enemySearch_Seconds => statusManagement.GetEnemySearch_Seconds();
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_weightTemporaryComplement = 0;
        #endregion

        /// <summary>
        /// MechanicalWarrior Ablity1 Start
        /// </summary>
        void MechanicalWarriorAblity1Start()
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
        /// MechanicalWarrior Ablity1 End
        /// </summary>
        void MechanicalWarriorAblity1End()
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
                Debug.Log(o.name);
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

        private void SetLayerWeight( Animator animator, string layerName, float weight)
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
    }
}
