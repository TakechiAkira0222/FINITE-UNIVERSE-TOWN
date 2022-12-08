using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Takechi.CharacterController.Parameters;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private GameObject m_sword;
        [SerializeField] private GameObject m_swordEffectTrail;
        [SerializeField] private string     m_targetTagName = "PlayerCharacter";
        [SerializeField, Range( 1.0f, 2.5f)] private float m_withinRange = 1.3f;
        [SerializeField, Range( 2.5f, 8f)] private float   m_outOfRange = 5f;
        [SerializeField, Range( 5, 15)] private int        m_trackingFrame = 10;
        #endregion

        private Collider m_swordCollider;
        #region UnityAnimatorEvent

        private void Awake()
        {
            m_swordCollider = m_sword.GetComponent<Collider>();
            Physics.IgnoreCollision( m_swordCollider, m_characterStatusManagement.Collider, false);
        }

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            if(!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(true);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackStart), RpcTarget.OthersBuffered);
        }

        /// <summary>
        /// FastAttack Animation End
        /// </summary>
        void OfficeWorkerFastAttackEnd()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(false);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackEnd), RpcTarget.OthersBuffered);
        }

        /// <summary>
        /// SecondAttack Animation Start
        /// </summary>
        void OfficeWorkerSecondAttackStart()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(true);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackStart), RpcTarget.OthersBuffered);
        }

        /// <summary>
        /// SecondAttack Animation End
        /// </summary>
        void OfficeWorkerSecondAttackEnd()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(false);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackEnd), RpcTarget.OthersBuffered);
        }

        /// <summary>
        /// ThirdAttack Animation Start
        /// </summary>
        void OfficeWorkerThirdAttackStart()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(true);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackStart), RpcTarget.OthersBuffered);
        }

        /// <summary>
        /// ThirdAttack Animation End
        /// </summary>
        void OfficeWorkerThirdAttackEnd()
        {
            if (!m_characterStatusManagement.PhotonView.IsMine) return;

            m_swordEffectTrail.SetActive(false);
            m_thisPhotonView.RPC(nameof(RpcOfficeWorkerAttackEnd), RpcTarget.OthersBuffered);
        }

        #endregion

        #region PunRPCFanction

        [PunRPC]
        void RpcOfficeWorkerAttackStart()
        {
            setSwordStatus(true);
            StartCoroutine(nameof(AttackTowardsTarget));
        }

        [PunRPC]
        void RpcOfficeWorkerAttackEnd()
        {
            setSwordStatus(false);
        }

        [PunRPC]
        void RpcsetPhysicsIgnoreCollision()
        {

        }

        #endregion

        void setSwordStatus(bool flag)
        {
            m_swordEffectTrail.SetActive(flag);
            m_swordCollider.enabled = flag;
        }

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
