using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Takechi.UI.SliderContlloer;
using Takechi.CharacterController.RoomStatus;
using Takechi.CharacterController.Address;

using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.GameManagerSystem.Hardpoint
{
    [RequireComponent(typeof(RoomStatusManagement))]
    public class HardpointSliderGetTeamPointMangement : SliderContlloerManager
    {
        #region SerializeField
        [Header("=== HardpointGameManagerParameter ===")]
        [SerializeField] private HardpointGameManagerParameters m_hardpointGameManagerParameters;
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== RoomStatusManagement ===")]
        [SerializeField] private RoomStatusManagement m_roomStatusManagement;

        [Header("=== Script Setting ===")]
        [SerializeField] private GameObject m_beforeStartIsntans;

        [SerializeField] private Slider m_teamAslider;
        [SerializeField] private Slider m_teamBslider;

        [SerializeField] private Text m_percentageOfTeamAPointsText;
        [SerializeField] private Text m_percentageOfTeamBPointsText;

        #endregion


        #region private variable
        private HardpointGameManagerParameters gameManagerParameters => m_hardpointGameManagerParameters;
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private RoomStatusManagement roomStatusManagement => m_roomStatusManagement;

        private float m_percentageOfTeamAPoints = 0f;
        private float m_percentageOfTeamBPoints = 0f;
        private float m_victory_f => roomStatusManagement.GetVictoryPoint();
        private float m_aPoint_f => roomStatusManagement.GetTeamAPoint_hardPoint();
        private float m_bPoint_f => roomStatusManagement.GetTeamBPoint_hardPoint();
        private Dictionary<string, Action> m_gameMain = new Dictionary<string, Action>();

        #endregion

        #region unity event
        private void Reset()
        {
            m_roomStatusManagement = this.GetComponent<RoomStatusManagement>();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            setupOfOnEnable();
        }
        public override void OnDisable()
        {
            base.OnDisable();
            setupOfOnDisable();
        }

        private void Start()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                setValue(m_teamAslider, roomStatusManagement.GetTeamAPoint_hardPoint(), gameManagerParameters.GetVictoryConditionPoints());
                setValue(m_teamBslider, roomStatusManagement.GetTeamBPoint_hardPoint(), gameManagerParameters.GetVictoryConditionPoints());
            }
            else
            {
                StartCoroutine(DelayMethod(NetworkSyncSettings.clientLatencyCountermeasureTime, () =>
                {
                    setValue(m_teamAslider, roomStatusManagement.GetTeamAPoint_hardPoint(), gameManagerParameters.GetVictoryConditionPoints());
                    setValue(m_teamBslider, roomStatusManagement.GetTeamBPoint_hardPoint(), gameManagerParameters.GetVictoryConditionPoints());
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

            if (!PhotonNetwork.InRoom) return;
            m_gameMain[roomStatusManagement.GetGameState()]();
        }

        #endregion

        #region main game finction
        private void GameState_BeforeStart()
        {
            m_beforeStartIsntans.SetActive(true);
            addressManagement.GetReticleCanvas().gameObject.SetActive(false);
        }

        private void GameState_Running()
        {
            m_beforeStartIsntans.SetActive(false); 
            addressManagement.GetReticleCanvas().gameObject.SetActive(true);
        }

        private void GameState_End()
        {

        }

        private void GameState_Stopped()
        {

        }

        private void GameState_NULL()
        {
            m_beforeStartIsntans.SetActive(false);
            addressManagement.GetReticleCanvas().gameObject.SetActive(true);
        }

        #endregion

        #region set up finction
        private void setupOfOnEnable()
        {
            m_gameMain.Add(RoomStatusName.GameState.beforeStart, GameState_BeforeStart);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart, <color=yellow>GameState_BeforeStart</color>)");
            m_gameMain.Add(RoomStatusName.GameState.running, GameState_Running);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.running, <color=yellow>GameState_Running</color>)");
            m_gameMain.Add(RoomStatusName.GameState.end, GameState_End);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.end, <color=yellow>GameState_End</color>)");
            m_gameMain.Add(RoomStatusName.GameState.stopped, GameState_Stopped);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped, <color=yellow>GameState_Stopped</color>)");
            m_gameMain.Add(RoomStatusName.GameState.NULL, GameState_NULL);
            Debug.Log($"m_gameMain.<color=yellow>Add</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.NULL, <color=yellow>GameState_NULL</color>)");

        }

        private void setupOfOnDisable()
        {
            m_gameMain.Remove(RoomStatusName.GameState.beforeStart);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.beforeStart");
            m_gameMain.Remove(RoomStatusName.GameState.running);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.running");
            m_gameMain.Remove(RoomStatusName.GameState.end);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.end");
            m_gameMain.Remove(RoomStatusName.GameState.stopped);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.stopped");
            m_gameMain.Remove(RoomStatusName.GameState.NULL);
            Debug.Log($"m_gameMain.<color=yellow>Remove</color>( <color=green>RoomStatusName</color>.<color=green>GameState</color>.NULL");
        }

        #endregion
    }
}
