using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.ViewpointOperation;
using UnityEngine;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private GameObject m_equipment;
        [SerializeField] private GameObject m_deathblowLaserEffect;
        [SerializeField] private GameObject m_deathblowAuraEffect;
        [SerializeField] private CharacterBasicMovement m_basicMovement;
        [SerializeField] private CharacterBasicJump m_basicJump;
        [SerializeField] private CharacterBasicViewpointOperation m_basicViewpoint;
        #endregion

        #region UnityAnimatorEvent
        /// <summary>
        /// �K�E�Zanimation�̊J�n
        /// </summary>
        void OfficeWorkerDeathblowStart()
        {
            m_equipment.SetActive(false);

            m_basicJump.enabled = false;
            m_basicMovement.enabled = false;
            m_basicViewpoint.enabled = false;
        }

        /// <summary>
        /// �K�E�Z�̃��C�U�[���˂̊J�n
        /// </summary>
        void OfficeWorkerDeathblowLaserTrigger()
        {
            m_deathblowLaserEffect.SetActive(true);
        }

        /// <summary>
        /// �K�E�Z��area�G�t�F�N�g�̊J�n
        /// </summary>
        void OfficeWorkerDeathblowAuraTrigger()
        {
            m_deathblowAuraEffect.SetActive(true);
        }

        /// <summary>
        /// �K�E�Zanimation�̏I��
        /// </summary>
        void OfficeWorkerDeathblowEnd()
        {
            m_equipment.SetActive(true);
            m_deathblowAuraEffect.SetActive(false);
            m_deathblowLaserEffect.SetActive(false);

            m_basicJump.enabled = true;
            m_basicMovement.enabled = true;
            m_basicViewpoint.enabled = true;
        }
        #endregion
    }
}