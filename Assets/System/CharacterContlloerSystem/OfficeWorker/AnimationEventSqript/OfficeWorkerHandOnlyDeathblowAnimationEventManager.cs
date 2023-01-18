using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Jump;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;

using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using Takechi.CharacterController.Reference;
using Takechi.CharacterController.Address;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerHandOnlyDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_officeWorkerStatusManagement;
        [Header("=== CharacterKeyInputStateManagement ===")]
        [SerializeField] private CharacterKeyInputStateManagement m_characterKeyInputStateManagement;
        [Header("=== CharacterControllerReferenceManagement ===")]
        [SerializeField] private CharacterControllerReferenceManagement m_characterControllerReferenceManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector m_playableDirector;
        [SerializeField] private GameObject m_deathblowAreaEffect;

        #endregion
        private CharacterAddressManagement             addressManagement => m_characterAddressManagement;
        private OfficeWorkerStatusManagement           statusManagement => m_officeWorkerStatusManagement;
        private OfficeWorkerSoundEffectsManagement     soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private CharacterKeyInputStateManagement       keyInputStateManagement => m_characterKeyInputStateManagement;
        private CharacterControllerReferenceManagement controllerReferenceManagement => m_characterControllerReferenceManagement;
        private Animator handOnlyModelAnimator =>      addressManagement.GetHandOnlyModelAnimator();
        private Animator handNetworkModelAnimator =>   addressManagement.GetNetworkModelAnimator();
        private GameObject handOnlyModelObject =>  addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject =>   addressManagement.GetNetworkModelObject();

        #region UnityAnimatorEvent

        /// <summary>
        /// �d�݂̈ꎞ�ۊ�
        /// </summary>
        private float m_weightTemporaryComplement = 0;

        private void Awake()
        {
            m_playableDirector.played += Director_Played;
            m_playableDirector.stopped += Director_Stopped;
        }

        // <summary>
        // �K�E�Zanimation�̊J�n
        // </summary>
        void OfficeWorkerDeathblowStart()
        {
            // playableDirector
            m_playableDirector.Play();

            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfDeathblowStart();

            // animation weiht
            m_weightTemporaryComplement = handNetworkModelAnimator.GetLayerWeight(handNetworkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, 0.1f);
        }

        /// <summary>
        /// �K�E�Z���т̊J�n
        /// </summary>
        void OfficeWorkerDeathblowCallOutVoice()
        {
            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfCallOut();
        }

        // <summary>
        // �K�E�Zanimation�̏I��
        // </summary>
        void OfficeWorkerDeathblowEnd()
        {
            // animation weiht
            SetLayerWeight(handNetworkModelAnimator, AnimatorLayers.overrideLayer, m_weightTemporaryComplement);

            // audioSource
            soundEffectsManagement.PlayOneShotPowerUp();

            // effect 
            ActivationDeathblowAreaEffect();
            Invoke(nameof(ExitDeathblowAreaEffect), statusManagement.GetDeathblowMoveDuration_Seconds());
        }

        #endregion

        private void Director_Stopped(PlayableDirector obj)
        {
            // operation
            keyInputStateManagement.SetOperation(true);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(true);

            // rb
            statusManagement.SetIsKinematic(false);

            if (statusManagement.photonView.IsMine)
            {
                ActivationStatusManagement();
                Invoke(nameof(ExitStatusManagement), statusManagement.GetDeathblowMoveDuration_Seconds());

                handOnlyModelObject.SetActive(true);
                networkModelObject.SetActive(false);
            }
        }

        private void Director_Played(PlayableDirector obj)
        {
            // operation
            keyInputStateManagement.SetOperation(false);

            // animation Controler
            controllerReferenceManagement.GetMovementAnimationControler().SetInterfere(false);

            // rb
            statusManagement.SetIsKinematic(true);

            if (statusManagement.photonView.IsMine)
            {
                handOnlyModelObject.SetActive(false);
                networkModelObject.SetActive(true);
            }
        }

        private void SetLayerWeight(Animator animator, string layerName, float weight)
        {
            int LayerIndex = animator.GetLayerIndex(layerName);
            animator.SetLayerWeight(LayerIndex, weight);
            Debug.Log($" handNetworkModelAnimator.<color=yellow>SetLayerWeight</color>({LayerIndex}, {weight}); ");
        }

        #region recursive function

        /// <summary>
        /// �X�e�[�^�X�̕ύX�@�J�n
        /// </summary>
        private void ActivationStatusManagement()
        {
            statusManagement.UpdateAttackPower(statusManagement.GetAttackPowerIncrease());
            statusManagement.UpdateMovingSpeed(statusManagement.GetMoveingSpeedIncrease());
            statusManagement.UpdateJumpPower(statusManagement.GetJumpPowerIncrease());

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// �X�e�[�^�X�ύX�@�I��
        /// </summary>
        private void ExitStatusManagement()
        {
            statusManagement.UpdateAttackPower(-statusManagement.GetAttackPowerIncrease());
            statusManagement.UpdateMovingSpeed(-statusManagement.GetMoveingSpeedIncrease());
            statusManagement.UpdateJumpPower(-statusManagement.GetJumpPowerIncrease());

            statusManagement.UpdateLocalPlayerCustomProrerties();
        }

        /// <summary>
        /// �K�E�Zarea�G�t�F�N�g�@�J�n
        /// </summary>
        private void ActivationDeathblowAreaEffect()
        {
            m_deathblowAreaEffect.SetActive(true);
        }

        /// <summary>
        /// �K�E�ZArea�G�t�F�N�g�@�I��
        /// </summary>
        private void ExitDeathblowAreaEffect()
        {
            m_deathblowAreaEffect.SetActive(false);
        }

        #endregion
    }
}
