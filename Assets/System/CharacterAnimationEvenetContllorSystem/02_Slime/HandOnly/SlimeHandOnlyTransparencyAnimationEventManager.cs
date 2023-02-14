using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TakechiEngine.PUN;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SoundEffects;
using UnityEngine.Rendering;
using System.ComponentModel;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.AnimationEvent
{
    public class SlimeHandOnlyTransparencyAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== SlimeAddressManagement === ")]
        [SerializeField] private SlimeAddressManagement m_slimeAddressManagement;
        [Header("=== SlimeStatusManagement ===")]
        [SerializeField] private SlimeStatusManagement m_slimeStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private SlimeSoundEffectsManagement m_slimeSoundEffectsManagement;

        #endregion

        #region private variable
        private SlimeAddressManagement addressManagement => m_slimeAddressManagement;
        private SlimeStatusManagement statusManagement => m_slimeStatusManagement;
        private SlimeSoundEffectsManagement soundEffectsManagement => m_slimeSoundEffectsManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Collider myCollider => addressManagement.GetMyCollider();
        private Canvas displayToOthersCanvas => addressManagement.GetDisplayToOthersCanvas();
        private GameObject networkModelObject => addressManagement.GetNetworkModelObject();
        private Animator networkModelAnimator =>  addressManagement.GetNetworkModelAnimator();
        private Animator handOnlyModelAnimator => addressManagement.GetHandOnlyModelAnimator();
        private Renderer handonlyModelRenderer => addressManagement.GetHandOnlyModelRenderer();
        private ParticleSystem transparencyPaticleSystem => addressManagement.GetTransparencyPaticleSystem();
        private int duration_seconds => statusManagement.GetTransparencyDsuration_Swconds();
        private bool isMine => myPhotonView.IsMine;

        private bool isTransparency = false;
        /// <summary>
        /// èdÇ›ÇÃàÍéûï€ä«
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;
        private float m_handOnlyModelAnimatorWeight = 0;

        #endregion

        private void Awake()
        {

        }
        private void OnEnable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings += () => { resetTransparencyState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetTransparencyState</color>() <color=green>to add.</color>");
        }

        private void OnDisable()
        {
            statusManagement.InitializeCharacterInstanceStateSettings -= () => { resetTransparencyState(); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} :  statusManagement.InitializeCharacterInstanceStateSettings <color=yellow>resetTransparencyState</color>() <color=green>to remove.</color>");
        }

        #region unity animation event
        void SlimeTransparencyStart()
        {
            // operation
            keyInputStateManagement.SetOperation(false);
        }

        void SlimeTransparency()
        {
            if (isTransparency) return;

            isTransparency = true;
            transparencyPaticleSystem.Play();

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            m_handOnlyModelAnimatorWeight = networkModelAnimator.GetLayerWeight(handOnlyModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0f);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, 0f);

            if (!isMine)
            {
                networkModelObject.SetActive(false);
                displayToOthersCanvas.gameObject.SetActive(false);

                StartCoroutine(DelayMethod(duration_seconds, () =>
                {
                    isTransparency = false;
                    networkModelObject.SetActive(true);
                    displayToOthersCanvas.gameObject.SetActive(true);
                    transparencyPaticleSystem.Play();
                }));
            }
            else
            {
                handonlyModelRenderer.material.color = Color.black;
                StartCoroutine(DelayMethod(duration_seconds, () =>
                {
                    isTransparency = false;
                    handonlyModelRenderer.material.color = Color.white;
                    transparencyPaticleSystem.Play();
                }));
            }
        }
        void SlimeTransparencyEnd()
        {
            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
            SetLayerWeight(handOnlyModelAnimator, AnimatorLayers.overrideLayer, m_handOnlyModelAnimatorWeight);

            // operation
            keyInputStateManagement.SetOperation(true);
        }

        #endregion

        #region reset function

        private void resetTransparencyState()
        {
            isTransparency = false;

            if (!isMine)
            {
                networkModelObject.SetActive(true);
                displayToOthersCanvas.gameObject.SetActive(true);
            }
            else
            {
                handonlyModelRenderer.material.color = Color.white;
            }
        }

        #endregion
    }
}

