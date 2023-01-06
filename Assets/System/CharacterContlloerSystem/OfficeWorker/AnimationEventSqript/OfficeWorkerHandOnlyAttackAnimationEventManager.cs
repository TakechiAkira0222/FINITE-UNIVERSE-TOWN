using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class OfficeWorkerHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private GameObject m_swordObject;
        [SerializeField] private GameObject m_swordEffectTrail;
        //[SerializeField] private string m_targetTagName = "PlayerCharacter";
        //[SerializeField, Range(1.0f, 2.5f)] private float m_withinRange = 1.3f;
        //[SerializeField, Range(2.5f, 8f)] private float m_outOfRange = 5f;
        //[SerializeField, Range(5, 15)] private int m_trackingFrame = 10;

        #endregion

        #region private
        private PhotonView thisPhotonView => m_thisPhotonView;
        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;
        private GameObject swordObject => m_swordObject;
        private GameObject swordEffectTrail => m_swordEffectTrail;
        private Rigidbody  rb => characterStatusManagement.GetMyRigidbody();
        private Collider   swordCollider => swordObject.GetComponent<Collider>();
        //private string     targetTagName => m_targetTagName;
        //private float withinRange => m_withinRange;
        //private float outOfRange =>  m_outOfRange;
        //private float trackingFrame => m_trackingFrame;

        #endregion

        #region UnityAnimatorEvent

        private void Awake()
        {
            Physics.IgnoreCollision(swordCollider, characterStatusManagement.GetMyCollider(), false);
        }

        /// <summary>
        /// FastAttack Animation Start
        /// </summary>
        void OfficeWorkerFastAttackStart()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine)
            {
                //StartCoroutine(nameof(AttackTowardsTarget));
            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// FastAttack Animation End
        /// </summary>
        void OfficeWorkerFastAttackEnd()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// SecondAttack Animation Start
        /// </summary>
        void OfficeWorkerSecondAttackStart()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine)
            {
                //StartCoroutine(nameof(AttackTowardsTarget));
            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// SecondAttack Animation End
        /// </summary>
        void OfficeWorkerSecondAttackEnd()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

            setSwordStatus(false);
        }

        /// <summary>
        /// ThirdAttack Animation Start
        /// </summary>
        void OfficeWorkerThirdAttackStart()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine)
            {
                //StartCoroutine(nameof(AttackTowardsTarget));
            }
            else
            {
                setSwordStatus(true);
            }
        }

        /// <summary>
        /// ThirdAttack Animation End
        /// </summary>
        void OfficeWorkerThirdAttackEnd()
        {
            if (characterStatusManagement.GetMyPhotonView().IsMine) return;

            setSwordStatus(false);
        }

        #endregion

        #region set Function
        void setSwordStatus(bool flag)
        {
            swordEffectTrail.SetActive(flag);
            swordCollider.enabled = flag;
        }

        #endregion

        #region Recursive function
        ///// <summary>
        ///// �U�����̔񓯊��Ǐ]
        ///// </summary>
        ///// <returns></returns>
        //private IEnumerator AttackTowardsTarget()
        //{
        //    GameObject nearObj = serchTag(rb.gameObject, targetTagName);

        //    Vector3 tagetDir = Vector3.Normalize(nearObj.transform.position - rb.gameObject.transform.position);

        //    float dis = Vector3.Distance(nearObj.transform.position, rb.gameObject.transform.position);

        //    if (dis > withinRange && dis < outOfRange)
        //    {
        //        for (int turn = 0; turn < trackingFrame; turn++)
        //        {
        //            rb.gameObject.transform.position += tagetDir / trackingFrame;
        //            yield return new WaitForSeconds(0.01f);
        //        }
        //    }
        //}

        /// <summary>
        /// ��ԋ߂������̎w��^�O�I�u�W�F�N�g��m��B
        /// </summary>
        /// <param name="nowObj"></param>
        /// <param name="tagName"></param>
        /// <returns></returns>
        private GameObject serchTag(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //�����p�ꎞ�ϐ�
            float nearDis = 0;          //�ł��߂��I�u�W�F�N�g�̋���
                                        //string nearObjName = "";    //�I�u�W�F�N�g����
            GameObject targetObj = null; //�I�u�W�F�N�g

            //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
                //�ꎞ�ϐ��ɋ������i�[
                if (nearDis == 0 || nearDis > tmpDis)
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
        #endregion
    }
}