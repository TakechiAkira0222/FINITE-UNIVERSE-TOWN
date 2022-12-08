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
