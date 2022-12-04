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
        /// 攻撃時の非同期追従
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
        /// 一番近い距離の指定タグオブジェクトを知る。
        /// </summary>
        /// <param name="nowObj"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private GameObject serchTag( GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //距離用一時変数
            float nearDis = 0;          //最も近いオブジェクトの距離
                                        //string nearObjName = "";    //オブジェクト名称
            GameObject targetObj = null; //オブジェクト

            //タグ指定されたオブジェクトを配列で取得する
            foreach ( GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance( obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if ( nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }
            }
            //最も近かったオブジェクトを返す
            //return GameObject.Find(nearObjName);
            return targetObj;
        }
    }
}
