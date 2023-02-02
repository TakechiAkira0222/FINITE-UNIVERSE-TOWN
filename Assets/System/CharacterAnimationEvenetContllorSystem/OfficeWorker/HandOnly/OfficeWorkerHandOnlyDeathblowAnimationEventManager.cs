using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.KeyInputStete;

using UnityEngine;
using UnityEngine.Playables;

using Takechi.CharacterController.Reference;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.AnimationEvent;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerHandOnlyDeathblowAnimationEventManager : AnimationEventManagement
    {
        #region SerializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== OfficeWorkerStatusManagement ===")]
        [SerializeField] private OfficeWorkerStatusManagement m_officeWorkerStatusManagement;
        [Header("=== OfficeWorkerSoundEffectsManagement ===")]
        [SerializeField] private OfficeWorkerSoundEffectsManagement m_officeWorkerSoundEffectsManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableAsset m_deathblowTimeline;
        [SerializeField] private GameObject    m_deathblowAreaEffect;

        #endregion
        private CharacterAddressManagement          addressManagement => m_characterAddressManagement;
        private OfficeWorkerStatusManagement        statusManagement => m_officeWorkerStatusManagement;
        private OfficeWorkerSoundEffectsManagement  soundEffectsManagement => m_officeWorkerSoundEffectsManagement;
        private PlayableDirector   myAvatarPlayableDirector => addressManagement.GetMyAvatarPlayableDirector();
        private Animator   handOnlyModelAnimator =>      addressManagement.GetHandOnlyModelAnimator();
        private Animator   networkModelAnimator =>   addressManagement.GetNetworkModelAnimator();
        private GameObject handOnlyModelObject =>  addressManagement.GetHandOnlyModelObject();
        private GameObject networkModelObject =>   addressManagement.GetNetworkModelObject();
        /// <summary>
        /// �d�݂̈ꎞ�ۊ�
        /// </summary>
        private float m_networkModelAnimatorWeight = 0;

        #region UnityAnimatorEvent
        // <summary>
        // �K�E�Zanimation�̊J�n
        // </summary>
        void OfficeWorkerDeathblowStart()
        {
            myAvatarPlayableDirector.played += Director_Played;
            myAvatarPlayableDirector.stopped += Director_Stopped;
            myAvatarPlayableDirector.playableAsset = m_deathblowTimeline;

            // playableDirector
            myAvatarPlayableDirector.Play();

            // audioSource
            soundEffectsManagement.PlayOneShotVoiceOfDeathblowStart();

            // animation weiht
            m_networkModelAnimatorWeight = networkModelAnimator.GetLayerWeight(networkModelAnimator.GetLayerIndex(AnimatorLayers.overrideLayer));
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, 0.1f);
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
            // audioSource
            soundEffectsManagement.PlayOneShotPowerUp();

            // effect 
            ActivationDeathblowAreaEffect();
            Invoke(nameof(ExitDeathblowAreaEffect), statusManagement.GetDeathblowMoveDuration_Seconds());

            // animation weiht
            SetLayerWeight(networkModelAnimator, AnimatorLayers.overrideLayer, m_networkModelAnimatorWeight);
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

            myAvatarPlayableDirector.played -= Director_Played;
            myAvatarPlayableDirector.stopped -= Director_Stopped;
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
