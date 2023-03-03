using Photon.Pun;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.ContactJudgment;

namespace Takechi.CharacterController.SmokeGrenade
{
    public class MechanicalWarriorSmokeGrenade : ContactJudgmentManagement
    {
        [SerializeField] private ParticleSystem m_detonationEffect;
        [SerializeField] private GameObject m_smokeEffect;

        private void OnCollisionEnter(Collision collision)
        {
            m_detonationEffect.Play();
            m_smokeEffect.SetActive(true);
        }
    }
}
