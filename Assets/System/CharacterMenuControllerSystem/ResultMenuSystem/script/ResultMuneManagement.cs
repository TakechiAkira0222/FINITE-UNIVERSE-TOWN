using Photon.Pun;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using TakechiEngine.PUN.ServerConnect.Joined;
using Takechi.CharacterController.Parameters;
using Takechi.PlayableCharacter.FadingCanvas;
using Takechi.CharacterController.RoomStatus;
using Takechi.CharacterController.Address;

using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using static Takechi.ScriptReference.AnimatorControlVariables.ReferencingTheAnimatorControlVariablesName;

namespace Takechi.UI.ResultMune
{
    public class ResultMuneManagement : TakechiJoinedPunCallbacks
    {
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private List<GameObject> m_characterPrefabList = new List<GameObject>();
        [SerializeField] private ToFade m_toFade;

        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private RoomStatusManagement roomStatusManagement => addressManagement.GetMyRoomStatusManagement();
        private List<GameObject> characterPrefabList => m_characterPrefabList;
        private ToFade toFade => m_toFade;

        private void Awake()
        {
            if ( !statusManagement.GetIsLocal()) return;

            toFade.OnFadeIn("NowLoading...");

            setDisplayCharacterPrefab( statusManagement.GetCustomPropertiesSelectedCharacterNumber());

            setAnimatorTrigger();
        }

        public void OnLeaveRoom()
        {
            toFade.OnFadeOut("NowLoading...");

            StartCoroutine(DelayMethod(NetworkSyncSettings.fadeProductionTime_Seconds, () =>
            {
                LeaveRoom();
            }));
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            SceneManager.LoadScene(SceneName.lobbyScene);
        }

        #region set variable
        private void setDisplayCharacterPrefab(int index)
        {
            foreach (GameObject o in characterPrefabList) { o.SetActive(false); }
            characterPrefabList[index].SetActive(true);
        }
        private void setAnimatorTrigger()
        {
            foreach (GameObject o in characterPrefabList)
            {
                Animator animator = o.GetComponent<Animator>();

                if ( statusManagement.GetCustomPropertiesTeamName() == roomStatusManagement.GetVictoryingTeamName())
                {
                    animator.SetBool(AnimatorParameter.VictoryParameterName, true);
                    Debug.Log($" animator.<yellow>SetBool</color>({AnimatorParameter.VictoryParameterName}, true) to set.");
                }
                else 
                {
                    animator.SetBool(AnimatorParameter.LoseParameterName, true);
                    Debug.Log($" animator.<yellow>SetBool</color>({AnimatorParameter.LoseParameterName}, true) to set.");
                }
            }
        }

        #endregion
    }
}
