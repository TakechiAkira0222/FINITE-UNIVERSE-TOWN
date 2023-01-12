using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static UnityEngine.Rendering.DebugUI;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        [SerializeField] private HardpointGameManagement m_gameManagement;
        [SerializeField] private GameObject m_navigationText; 

        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<HardpointGameManagement>(); 
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine(uiRotation(m_navigationText));
            m_gameManagement.ChangePointIocation += resetMemberList;
            Debug.Log("HardpointAreaLocationPoint.ChangePointIocation += <color=yellow>restresetMemberList</color>");
        }
        private void OnDisable()
        {
            m_gameManagement.ChangePointIocation -= resetMemberList;
            Debug.Log("HardpointAreaLocationPoint.ChangePointIocation -= <color=yellow>restresetMemberList</color>");
        }

        protected override void OnTriggerStay( Collider other)
        {
            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    if ( hitTeamBMemberList.Count != 0) return;
                    m_gameManagement.SetTeamAPoint(1);
                }
                else
                {
                    if ( hitTeamAMemberList.Count != 0) return;
                    m_gameManagement.SetTeamBPoint(1);
                }
            }
        }
    }
}
