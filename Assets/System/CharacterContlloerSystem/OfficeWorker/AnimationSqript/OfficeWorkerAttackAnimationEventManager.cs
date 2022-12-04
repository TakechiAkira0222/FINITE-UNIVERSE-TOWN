using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [SerializeField] private GameObject m_swordEffectTrail;
        [SerializeField] private string     m_targetTagName = "PlayerCharacter";
        [SerializeField, Range( 1.0f, 2.5f)] private float m_withinRange = 1.3f;
        [SerializeField, Range( 2.5f, 8f)] private float   m_outOfRange = 5f;
        [SerializeField, Range( 5, 15)] private int        m_trackingFrame = 10;

        #endregion
        #region UnityAnimatorEvent

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            m_swordEffectTrail.SetActive(true);

            StartCoroutine(nameof(AttackTowardsTarget));
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

            StartCoroutine(nameof(AttackTowardsTarget));
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

            StartCoroutine(nameof(AttackTowardsTarget));
        }

        /// <summary>
        /// ThirdAttack Animation End
        /// </summary>
        void OfficeWorkerThirdAttackEnd()
        {
            m_swordEffectTrail.SetActive(false);
        }

        #endregion

        /// <summary>
        /// �U�����̔񓯊��Ǐ]
        /// </summary>
        /// <returns></returns>
        private IEnumerator AttackTowardsTarget()
        {
            GameObject nearObj = serchTag( this.gameObject, m_targetTagName);

            Vector3 tagetDir = Vector3.Normalize( nearObj.transform.position - this.transform.position);

            float dis = Vector3.Distance( nearObj.transform.position, this.transform.position);

            if ( dis > m_withinRange && dis < m_outOfRange)
            {
                for (int turn = 0; turn < m_trackingFrame; turn++)
                {
                    this.transform.position += tagetDir / m_trackingFrame;
                    yield return new WaitForSeconds( 0.01f);
                }
            }
        }

        /// <summary>
        /// ��ԋ߂������̎w��^�O�I�u�W�F�N�g��m��B
        /// </summary>
        /// <param name="nowObj"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private GameObject serchTag( GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //�����p�ꎞ�ϐ�
            float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
                                        //string nearObjName = "";    //�I�u�W�F�N�g����
            GameObject targetObj = null; //�I�u�W�F�N�g

            //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
            foreach ( GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
                tmpDis = Vector3.Distance( obs.transform.position, nowObj.transform.position);

                //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
                //�ꎞ�ϐ��ɋ������i�[
                if ( nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }
            }
            //�ł��߂������I�u�W�F�N�g��Ԃ�
            //return GameObject.Find(nearObjName);
            return targetObj;
        }
    }
}
