using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.UI.SliderContlloer;

using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Domination
{
    public class DominationSliderGetTeamPointMangement : SliderContlloerManager
    {
        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;

        private float m_aPoint => (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint];
        private float m_bPoint => (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint];
        private float m_victory => (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey];

        private void Start()
        {
            setValue(m_teamAslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint],
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]);

            setValue(m_teamBslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint],
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]);
        }

        private void Update()
        {
            m_percentageOfTeamAPoints = Mathf.Ceil((m_aPoint / m_victory) * 1000);
            m_percentageOfTeamBPoints = Mathf.Ceil((m_bPoint / m_victory) * 1000);

            m_percentageOfTeamAPointsText.text = (m_percentageOfTeamAPoints / 10).ToString() + "%";
            m_percentageOfTeamBPointsText.text = (m_percentageOfTeamBPoints / 10).ToString() + "%";

            updateValue(m_teamAslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint]);

            updateValue(m_teamBslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint]);
        }
    }
}