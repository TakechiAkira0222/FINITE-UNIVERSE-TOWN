using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.UI.SliderContlloer;

using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference.RoomTeamStatusKey;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointSliderGetTeamPointMangement : SliderContlloerManager
    {
        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;

        private float m_aPoint_f  => (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamAPoint];
        private float m_bPoint_f  => (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamBPoint];
        private float m_victory_f => (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey];

        public int GetTeamAPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamAPoint]; }
        public int GetTeamBPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[HardPointStatusKey.teamBPoint]; }
        public int GetVictoryPoint() { return (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]; }

        private void Start()
        {
            setValue(m_teamAslider, GetTeamAPoint(), GetVictoryPoint());
            setValue(m_teamBslider, GetTeamBPoint(), GetVictoryPoint());
        }

        private void Update()
        {
            m_percentageOfTeamAPoints = Mathf.Ceil(( m_aPoint_f / m_victory_f) * 1000);
            m_percentageOfTeamBPoints = Mathf.Ceil(( m_bPoint_f / m_victory_f) * 1000);

            m_percentageOfTeamAPointsText.text = Mathf.Clamp((m_percentageOfTeamAPoints / 10), 0, 100).ToString() + "%";
            m_percentageOfTeamBPointsText.text = Mathf.Clamp((m_percentageOfTeamBPoints / 10), 0, 100).ToString() + "%";

            updateValue(m_teamAslider, GetTeamAPoint());
            updateValue(m_teamBslider, GetTeamBPoint());
        }
    }
}
