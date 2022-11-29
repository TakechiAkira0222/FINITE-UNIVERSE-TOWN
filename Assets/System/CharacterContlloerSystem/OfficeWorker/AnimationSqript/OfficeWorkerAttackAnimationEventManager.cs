using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private GameObject m_swordEffectTrail;
        #endregion
        #region UnityAnimatorEvent

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            m_swordEffectTrail.SetActive(true);
        }

        /// <summary>
        /// FastAttack Animation End
        /// </summary>
        void OfficeWorkerFastAttackEnd()
        {
            m_swordEffectTrail.SetActive(false);
        }

        /// <summary>
        /// SecondAttack Animation Start
        /// </summary>
        void OfficeWorkerSecondAttackStart()
        {
            m_swordEffectTrail.SetActive(true);
        }

        /// <summary>
        /// SecondAttack Animation End
        /// </summary>
        void OfficeWorkerSecondAttackEnd()
        {
            m_swordEffectTrail.SetActive(false);
        }

        /// <summary>
        /// ThirdAttack Animation Start
        /// </summary>
        void OfficeWorkerThirdAttackStart()
        {
            m_swordEffectTrail.SetActive(true);
        }

        /// <summary>
        /// ThirdAttack Animation End
        /// </summary>
        void OfficeWorkerThirdAttackEnd()
        {
            m_swordEffectTrail.SetActive(false);
        }

        #endregion
    }
}
