using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.SoundEffects;
using Takechi.CharacterController.SpecificSoundEffects.OfficeWorker;
using Photon.Pun;
using Takechi.CharacterController.KeyInputStete;

namespace Takechi.CharacterController.Parameters
{
    public class OfficeWorkerSoundEffectsManagement : CharacterBasicSoundEffectsStateManagement
    {
        [Header("=== OfficeWorkerSpecificSoundEffects ===")]
        [SerializeField] private OfficeWorkerSpecificSoundEffects officeWorkerSpecificSoundEffects;

        protected override void OnDisable()
        {
            characterKeyInputStateManagement.InputToNormalAttack += (c_status) => { c_status.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfNormalAttack()); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
            base.OnDisable();
        }
        protected override void OnEnable()
        {
            characterKeyInputStateManagement.InputToNormalAttack += (c_status) => { c_status.GetMyMainAudioSource().PlayOneShot(officeWorkerSpecificSoundEffects.GetVoiceOfNormalAttack()); };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : m_characterKeyInputStateManagement.InputToJump function <color=green>to add.</color>");
            base.OnEnable();
        }
    }
}

