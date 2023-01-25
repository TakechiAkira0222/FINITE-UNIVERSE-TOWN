using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.RoomStatus;
using UnityEngine;
using UnityEngine.Rendering;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        #region SerializeField
        [Header("=== HardpointGameManagement ===")]
        [SerializeField] private HardpointGameManagement m_gameManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private GameObject m_navigationText;

        #endregion

        #region private variable
        private HardpointGameManagement gameManagement => m_gameManagement;
        private RoomStatusManagement    roomStatusManagement => gameManagement.GetMyRoomStatusManagement();
        private GameObject navigationText => m_navigationText;

        #endregion

        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<HardpointGameManagement>(); 
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine( uiRotation(navigationText));
            //m_gameManagement.ChangePointIocation += resetMemberList;
            //Debug.Log("HardpointAreaLocationPoint.ChangePointIocation += <color=yellow>restresetMemberList</color>");
        }
        private void OnDisable()
        {
            //m_gameManagement.ChangePointIocation -= resetMemberList;
            //Debug.Log("HardpointAreaLocationPoint.ChangePointIocation -= <color=yellow>restresetMemberList</color>");
        }

        protected override void OnTriggerStay( Collider other)
        {
            if ( other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamKey] == CharacterTeamStatusName.teamAName)
                {
                    // if ( hitTeamBMemberList.Count != 0) return;
                    roomStatusManagement.UpdateTeamAPoint_hardPoint(1);
                }
                else
                {
                    // if ( hitTeamAMemberList.Count != 0) return;
                    roomStatusManagement.UpdateTeamBPoint_hardPoint(1);
                }
            }
        }
    }
}
