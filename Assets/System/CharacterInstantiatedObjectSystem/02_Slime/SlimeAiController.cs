using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using TakechiEngine.PUN;
using UnityEngine;
using UnityEngine.AI;
using static Takechi.ScriptReference.SearchForPrefabs.ReferencingSearchForPrefabs;

namespace Takechi.CharacterController.Deathblow.AI
{
    public class SlimeAiController : TakechiPunCallbacks
    {
        [SerializeField] private PhotonView   m_thisPhotonView;
        [SerializeField] private NavMeshAgent m_navMeshAgent;
        [SerializeField] private ParticleSystem m_appearanceAndDisappearanceParticleSystem;
       
        private ParticleSystem appearanceAndDisappearanceParticleSystem => m_appearanceAndDisappearanceParticleSystem;
        private NavMeshAgent navMeshAgent => m_navMeshAgent;
        private PhotonView thisPhotonView => m_thisPhotonView;
        private bool isMine => m_thisPhotonView.IsMine;

        private void Reset()
        {
            m_navMeshAgent   = this.GetComponent<NavMeshAgent>();
            m_thisPhotonView = this.GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (!isMine) return;
            StartCoroutine(nameof(NavMeshSetsTagget));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if( collision.gameObject.tag == SearchForPrefabTag.playerCharacterPrefabTag)
            {
                appearanceAndDisappearanceParticleSystem.Play();
            }
        }

        private IEnumerator NavMeshSetsTagget()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);

                if (serchTag(this.gameObject, SearchForPrefabTag.playerCharacterPrefabTag) != null)
                {
                    navMeshAgent.SetDestination( serchTag(this.gameObject, SearchForPrefabTag.playerCharacterPrefabTag).transform.position);
                }
                else
                {
                    navMeshAgent.SetDestination(transform.position + Vector3.forward);
                }
            }
        }

        private GameObject serchTag(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;            //�����p�ꎞ�ϐ�
            float nearDis = 0;           //�ł��߂��I�u�W�F�N�g�̋���
            List<GameObject> taggetObjectList = new List<GameObject>(); 

            // ������List���
            foreach ( GameObject obj in GameObject.FindGameObjectsWithTag(tagName))
            {
                PhotonView targetPhotonView = obj.transform.root.GetComponent<PhotonView>();

                if ( thisPhotonView.ControllerActorNr == targetPhotonView.ControllerActorNr) continue;

                taggetObjectList.Add(obj);
            }

            GameObject tagetObject = null;

            //�^�O�w�肳�ꂽ�I�u�W�F�N�g��z��Ŏ擾����
            foreach (GameObject obs in taggetObjectList)
            {
                //���g�Ǝ擾�����I�u�W�F�N�g�̋������擾
                tmpDis = Vector3.Distance( obs.transform.position, nowObj.transform.position);

                //�I�u�W�F�N�g�̋������߂����A����0�ł���΃I�u�W�F�N�g�����擾
                //�ꎞ�ϐ��ɋ������i�[
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    tagetObject = obs;
                }
            }

            //�ł��߂������I�u�W�F�N�g��Ԃ�
            //return GameObject.Find(nearObjName);
            return tagetObject;
        }
    }
}
