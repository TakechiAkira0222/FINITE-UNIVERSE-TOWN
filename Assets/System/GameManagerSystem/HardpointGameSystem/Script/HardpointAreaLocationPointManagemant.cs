using Photon.Pun;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.RoomStatus;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointAreaLocationPointManagemant : AreaLocationPointManagemant
    {
        #region SerializeField
        [Header("=== HardpointGameManagement ===")]
        [SerializeField] private HardpointGameManagement m_gameManagement;
        [Header("=== HardpointSoundEffectsManagement ===")]
        [SerializeField] private HardpointSoundEffectsManagement m_hardpointSoundEffectsManagement;
        [Header("=== ScriptSetting ===")]
        [SerializeField] private GameObject m_navigationText;

        #endregion

        #region private variable
        private HardpointGameManagement gameManagement => m_gameManagement;
        private HardpointSoundEffectsManagement soundEffectsManagement => m_hardpointSoundEffectsManagement;
        private RoomStatusManagement roomStatusManagement => gameManagement.GetMyRoomStatusManagement();
        private GameObject navigationText => m_navigationText;
        private bool localGameEnd => gameManagement.GetLocalGameEnd();

        public event Action RisingTeamPoints = delegate { };

        #endregion

        #region unity event 
        private void Reset()
        {
            m_gameManagement = this.transform.root.GetComponent<HardpointGameManagement>();
            m_navigationText = this.transform.GetChild(0).gameObject;
        }

        private void OnEnable()
        {
            StartCoroutine( uiRotation(navigationText));
            RisingTeamPoints += soundEffectsManagement.RisingPointsSound;
            Debug.Log("RisingTeamPoints += soundEffectsManagement.<color=yellow>RisingPointsSound</color> <color=green> to add</color>.");
        }
        private void OnDisable()
        {
            RisingTeamPoints -= soundEffectsManagement.RisingPointsSound;
            Debug.Log("RisingTeamPoints += soundEffectsManagement.<color=yellow>RisingPointsSound</color> <color=green> to remove</color>.");
        }

        protected override void OnTriggerStay(Collider other)
        {
            if (roomStatusManagement.IsGameRunning() && !localGameEnd) GameState_Running(other);
        }

        #endregion

        #region main game function

        private void GameState_Running(Collider other)
        {
            if (other.gameObject.tag == m_gameManagement.GetJudgmentTagName())
            {
                int num = other.gameObject.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                if ((string)PhotonNetwork.LocalPlayer.Get(num).CustomProperties[CharacterStatusKey.teamNameKey] == CharacterTeamStatusName.teamAName)
                {
                    RisingTeamPoints();
                    roomStatusManagement.UpdateTeamAPoint_hardPoint(1);
                }
                else
                {
                    RisingTeamPoints();
                    roomStatusManagement.UpdateTeamBPoint_hardPoint(1);
                }
            }
        }

        #endregion
    }
}
