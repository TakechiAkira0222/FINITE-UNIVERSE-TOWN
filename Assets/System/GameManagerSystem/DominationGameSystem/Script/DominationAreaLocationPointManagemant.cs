using UnityEngine;
using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using Takechi.GameManagerSystem.Hardpoint;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        [SerializeField] private DominationGameManagement m_gameManagement;
        [SerializeField] private GameObject m_navigationText;

        private string m_adminTeam = " null team";

        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<DominationGameManagement>();
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine( uiRotation( m_navigationText));

            m_gameManagement.ChangePointIocation += resetMemberList;
            Debug.Log("DominationGameManagement.ChangePointIocation += <color=yellow>restresetMemberList</color>");
        }

        private void OnDisable()
        {
            m_gameManagement.ChangePointIocation -= resetMemberList;
            Debug.Log("DominationGameManagement.ChangePointIocation -= <color=yellow>restresetMemberList</color>");
        }

        protected override void OnTriggerStay(Collider other)
        {
            base.OnTriggerStay(other);

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
                    if (hitTeamAMemberList.Count != 0) return;
                    m_gameManagement.SetTeamBPoint(1);
                }
            }
        }
    }
}