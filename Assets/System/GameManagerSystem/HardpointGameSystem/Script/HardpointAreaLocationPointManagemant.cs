using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointAreaLocationPointManagemant : MonoBehaviour
    {
        [SerializeField] private HardpointGameManagement m_gameManagement;
        [SerializeField] private GameObject m_navigationText; 

        private List<GameObject> m_hitCharacterList = new List<GameObject>(4);
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
                uiCursor.transform.localEulerAngles += new Vector3(0, 0.3f, 0);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                if (  !m_hitCharacterList.Contains(other.gameObject)) m_hitCharacterList.Add(other.gameObject);
            }

            foreach ( GameObject o in m_hitCharacterList)
            {
                int num = o.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    m_hitTeamAMemberList.Add(o);
                }
                else
                {
                    m_hitTeamBMemberList.Add(o);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                foreach (GameObject o in m_hitTeamAMemberList)
                {
                    Debug.Log(" ATeamMember : " + o.name);
                }

                foreach (GameObject o in m_hitTeamBMemberList)
                {
                    Debug.Log(" BTeamMember : " + o.name);
                }

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
            if (other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                if ( !m_hitCharacterList.Contains(other.gameObject)) m_hitCharacterList.Remove(other.gameObject);
            }

            foreach (GameObject o in m_hitCharacterList)
            {
                int num = o.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    m_hitTeamAMemberList.Remove(o);
                }
                else
                {
                    m_hitTeamBMemberList.Remove(o);
                }
            }
        }
    }
}
