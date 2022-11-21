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
        /// 必殺技animationの開始
        /// </summary>
        void OfficeWorkerDeathblowStart()
        {
            m_equipment.SetActive(false);

            m_basicJump.enabled = false;
            m_basicMovement.enabled = false;
            m_basicViewpoint.enabled = false;
        }

        /// <summary>
        /// 必殺技のレイザー放射の開始
        /// </summary>
        void OfficeWorkerDeathblowLaserTrigger()
        {
            m_deathblowLaserEffect.SetActive(true);
        }

        /// <summary>
        /// 必殺技のareaエフェクトの開始
        /// </summary>
        void OfficeWorkerDeathblowAuraTrigger()
        {
            m_deathblowAuraEffect.SetActive(true);
        }

        /// <summary>
        /// 必殺技animationの終了
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