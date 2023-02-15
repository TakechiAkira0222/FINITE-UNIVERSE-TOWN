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
            float tmpDis = 0;            //距離用一時変数
            float nearDis = 0;           //最も近いオブジェクトの距離
            List<GameObject> taggetObjectList = new List<GameObject>(); 

            // 条件つきList代入
            foreach ( GameObject obj in GameObject.FindGameObjectsWithTag(tagName))
            {
                PhotonView targetPhotonView = obj.transform.root.GetComponent<PhotonView>();

                if ( thisPhotonView.ControllerActorNr == targetPhotonView.ControllerActorNr) continue;

                taggetObjectList.Add(obj);
            }

            GameObject tagetObject = null;

            //タグ指定されたオブジェクトを配列で取得する
            foreach (GameObject obs in taggetObjectList)
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance( obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    tagetObject = obs;
                }
            }

            //最も近かったオブジェクトを返す
            //return GameObject.Find(nearObjName);
            return tagetObject;
        }
    }
}
