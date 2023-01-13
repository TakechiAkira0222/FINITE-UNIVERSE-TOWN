using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.UI.SliderContlloer;

using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationSliderGetTeamPointMangement : SliderContlloerManager
    {
        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Slider m_areaLocationAPointsSlider;
        [SerializeField] private Slider m_areaLocationBPointsSlider;
        [SerializeField] private Slider m_areaLocationCPointsSlider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;

        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamAPoint]
        /// </summary>
        private float m_aPoint_f =>  (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamAPoint];
        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamBPoint]
        /// </summary>
        private float m_bPoint_f =>  (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamBPoint];
        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]
        /// </summary>
        private float m_victory_f => (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey];
        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationAPoint]
        /// </summary>
        private float m_AreaLocationAPoint_f => (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationAPoint];
        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationBPoint]
        /// </summary>
        private float m_AreaLocationBPoint_f => (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationBPoint];
        /// <summary>
        /// Get (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationCPoint]
        /// </summary>
        private float m_AreaLocationCPoint_f => (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationCPoint];

        public int GetTeamAPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamAPoint]; }
        public int GetTeamBPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.teamBPoint]; }
        public int GetVictoryPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]; }

        public int GetAreaLocationAPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationAPoint]; }
        public int GetAreaLocationBPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationBPoint]; }
        public int GetAreaLocationCPoint()   { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationCPoint]; }
        public int GetAreaLocationMaxPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[DominationStatusKey.AreaLocationMaxPoint]; }

        private void Start()
        {
            setValue( m_teamAslider, GetTeamAPoint(), GetVictoryPoint());
            setValue( m_teamBslider, GetTeamBPoint(), GetVictoryPoint());
            setValue( m_areaLocationAPointsSlider, GetAreaLocationAPoint(), GetAreaLocationMaxPoint());
            setValue( m_areaLocationBPointsSlider, GetAreaLocationBPoint(), GetAreaLocationMaxPoint());
            setValue( m_areaLocationCPointsSlider, GetAreaLocationCPoint(), GetAreaLocationMaxPoint());
        }

        private void Update()
        {
            m_percentageOfTeamAPoints = Mathf.Ceil( m_aPoint_f / m_victory_f * 1000);
            m_percentageOfTeamBPoints = Mathf.Ceil( m_bPoint_f / m_victory_f * 1000);

            m_percentageOfTeamAPointsText.text = Mathf.Clamp( m_percentageOfTeamAPoints / 10, 0, 100).ToString() + "%";
            m_percentageOfTeamBPointsText.text = Mathf.Clamp( m_percentageOfTeamBPoints / 10, 0, 100).ToString() + "%";

            updateValue( m_teamAslider, GetTeamAPoint());
            updateValue( m_teamBslider, GetTeamBPoint());
            updateValue( m_areaLocationAPointsSlider, GetAreaLocationAPoint());
            updateValue( m_areaLocationBPointsSlider, GetAreaLocationBPoint());
            updateValue( m_areaLocationCPointsSlider, GetAreaLocationCPoint());
        }
    }
}