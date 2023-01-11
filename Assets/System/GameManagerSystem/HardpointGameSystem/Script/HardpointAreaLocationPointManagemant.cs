using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static UnityEngine.Rendering.DebugUI;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointAreaLocationPointManagemant : MonoBehaviour
    {
        [SerializeField] private HardpointGameManagement m_gameManagement;
        [SerializeField] private GameObject m_navigationText; 

        private List<GameObject> m_hitTeamAMemberList = new List<GameObject>(4);
        private List<GameObject> m_hitTeamBMemberList = new List<GameObject>(4);

        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<HardpointGameManagement>(); 
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine(uiRotation(m_navigationText));
        }

        protected IEnumerator uiRotation(GameObject uiCursor)
        {
            while (uiCursor.activeSelf)
            {
                float size = System.Math.Min(Vector3.Distance( uiCursor.transform.position, Camera.main.transform.position), 30) / 1000;

                uiCursor.transform.LookAt( Camera.main.transform);
                uiCursor.transform.localScale = new Vector3( size, size, size);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

            if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
            {
                m_hitTeamAMemberList.Add(other.gameObject);
            }
            else
            {
                m_hitTeamBMemberList.Add(other.gameObject);
            }
        }

        private void OnTriggerStay( Collider other)
        {
            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    if ( m_hitTeamBMemberList.Count != 0) return;
                    m_gameManagement.SetTeamAPoint(1);
                }
                else
                {
                    if ( m_hitTeamAMemberList.Count != 0) return;
                    m_gameManagement.SetTeamBPoint(1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!PhotonNetwork.IsMasterClient) return;

            int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

            if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
            {
                m_hitTeamAMemberList.Remove(other.gameObject);
            }
            else
            {
                m_hitTeamBMemberList.Remove(other.gameObject);
            }
        }
    }
}
