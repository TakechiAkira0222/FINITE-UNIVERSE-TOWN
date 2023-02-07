using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.RoomStatus;
using Takechi.UI.SliderContlloer;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.GameManagerSystem.Hardpoint
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class HardpointSliderGetTeamPointMangement : SliderContlloerManager
    {
        #region SerializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;

        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        #endregion


        #region private variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;
        private float m_victory_f => roomStatusManagement.GetVictoryPoint();
        private float m_aPoint_f  => roomStatusManagement.GetTeamAPoint_hardPoint();
        private float m_bPoint_f  => roomStatusManagement.GetTeamBPoint_hardPoint();

        #endregion
        private void Reset()
        {
            m_roomStatusManagement = this.GetComponent<RoomStatusManagement>();
        }

        private void Start()
        {
            if ( PhotonNetwork.IsMasterClient)
            {
                setValue( m_teamAslider, roomStatusManagement.GetTeamAPoint_hardPoint(), roomStatusManagement.GetVictoryPoint());
                setValue( m_teamBslider, roomStatusManagement.GetTeamBPoint_hardPoint(), roomStatusManagement.GetVictoryPoint());
            }
            else
            {
                StartCoroutine(DelayMethod( NetworkSyncSettings.clientLatencyCountermeasureTime, () => 
                {
                    setValue(m_teamAslider, roomStatusManagement.GetTeamAPoint_hardPoint(), roomStatusManagement.GetVictoryPoint());
                    setValue(m_teamBslider, roomStatusManagement.GetTeamBPoint_hardPoint(), roomStatusManagement.GetVictoryPoint());
                }));
            }
        }

        private void Update()
        {
            m_percentageOfTeamAPoints = Mathf.Ceil((m_aPoint_f / m_victory_f) * 1000);
            m_percentageOfTeamBPoints = Mathf.Ceil((m_bPoint_f / m_victory_f) * 1000);

            m_percentageOfTeamAPointsText.text = Mathf.Clamp((m_percentageOfTeamAPoints / 10), 0, 100).ToString() + "%";
            m_percentageOfTeamBPointsText.text = Mathf.Clamp((m_percentageOfTeamBPoints / 10), 0, 100).ToString() + "%";

            updateValue(m_teamAslider, roomStatusManagement.GetTeamAPoint_hardPoint());
            updateValue(m_teamBslider, roomStatusManagement.GetTeamBPoint_hardPoint());
        }
    }
}
