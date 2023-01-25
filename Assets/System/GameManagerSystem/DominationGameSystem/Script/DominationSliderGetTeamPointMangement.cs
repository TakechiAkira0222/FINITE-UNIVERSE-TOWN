using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.RoomStatus;
using Takechi.UI.SliderContlloer;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.GameManagerSystem.Domination
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class DominationSliderGetTeamPointMangement : SliderContlloerManager
    {
        #region SerializeField
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;

        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Slider m_areaLocationAPointsSlider;
        [SerializeField] private Slider m_areaLocationBPointsSlider;
        [SerializeField] private Slider m_areaLocationCPointsSlider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        #endregion

        #region private variable
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;
        private float m_victory_f => roomStatusManagement.GetVictoryPoint();
        private float m_aPoint_f =>  roomStatusManagement.GetTeamAPoint_domination();
        private float m_bPoint_f =>  roomStatusManagement.GetTeamBPoint_domination();

        #endregion 

        private void Reset()
        {
            m_roomStatusManagement = this.GetComponent<RoomStatusManagement>();
        }

        private void Start()
        {
            setValue(m_teamAslider, roomStatusManagement.GetTeamAPoint_domination(), roomStatusManagement.GetVictoryPoint());
            setValue(m_teamBslider, roomStatusManagement.GetTeamBPoint_domination(), roomStatusManagement.GetVictoryPoint());
            setValue(m_areaLocationAPointsSlider, roomStatusManagement.GetAreaLocationAPoint_domination(), roomStatusManagement.GetAreaLocationMaxPoint_domination());
            setValue(m_areaLocationBPointsSlider, roomStatusManagement.GetAreaLocationBPoint_domination(), roomStatusManagement.GetAreaLocationMaxPoint_domination());
            setValue(m_areaLocationCPointsSlider, roomStatusManagement.GetAreaLocationCPoint_domination(), roomStatusManagement.GetAreaLocationMaxPoint_domination());
        }

        private void Update()
        {
            m_percentageOfTeamAPoints = Mathf.Ceil( m_aPoint_f / m_victory_f * 1000);
            m_percentageOfTeamBPoints = Mathf.Ceil( m_bPoint_f / m_victory_f * 1000);

            m_percentageOfTeamAPointsText.text = Mathf.Clamp( m_percentageOfTeamAPoints / 10, 0, 100).ToString() + "%";
            m_percentageOfTeamBPointsText.text = Mathf.Clamp( m_percentageOfTeamBPoints / 10, 0, 100).ToString() + "%";

            updateValue( m_teamAslider, roomStatusManagement.GetTeamAPoint_domination());
            updateValue( m_teamBslider, roomStatusManagement.GetTeamBPoint_domination());
            updateValue( m_areaLocationAPointsSlider, roomStatusManagement.GetAreaLocationAPoint_domination());
            updateValue( m_areaLocationBPointsSlider, roomStatusManagement.GetAreaLocationBPoint_domination());
            updateValue( m_areaLocationCPointsSlider, roomStatusManagement.GetAreaLocationCPoint_domination());
        }
    }
}