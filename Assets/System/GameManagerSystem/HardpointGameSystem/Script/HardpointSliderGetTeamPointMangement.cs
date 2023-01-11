using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.UI.SliderContlloer;

using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    public class HardpointSliderGetTeamPointMangement : SliderContlloerManager
    {
        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        private void Start()
        {
            setValue( m_teamAslider,
              (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint],
              (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]);

            setValue( m_teamBslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint],
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.victoryPointKey]);
        }

        private void Update()
        {
            updateValue( m_teamAslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamAPoint]);

            updateValue( m_teamBslider,
                (int)PhotonNetwork.CurrentRoom.CustomProperties[RoomTeamStatusKey.teamBPoint]);
        }
    }
}
