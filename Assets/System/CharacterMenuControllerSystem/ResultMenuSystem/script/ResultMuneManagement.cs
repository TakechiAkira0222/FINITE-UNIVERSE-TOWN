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
using System;

namespace Takechi.UI.ResultMune
{
    public class ResultMuneManagement : TakechiJoinedPunCallbacks
    {
        #region serializeField
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== Script Setting ===")]
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_victoryBgm;
        [SerializeField] private AudioClip m_loseBgm;
        [SerializeField] private List<GameObject> m_characterPrefabList = new List<GameObject>();
        [SerializeField] private ToFade m_toFade;

        #endregion

        #region private function
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private RoomStatusManagement roomStatusManagement => addressManagement.GetMyRoomStatusManagement();
        private AudioSource audioSource => m_audioSource;
        private List<GameObject> characterPrefabList => m_characterPrefabList;
        private ToFade toFade => m_toFade;
        private Action<Animator> victorySetAction = delegate{ };
        private Action<Animator> loseSetAction = delegate{ };

        #endregion

        #region unity event
        private void Awake()
        {
            if ( !statusManagement.GetIsLocal()) return;

            victorySetAction += (animator) => 
            {
                animator.SetBool(AnimatorParameter.VictoryParameterName, true);
                Debug.Log($" animator.<yellow>SetBool</color>({AnimatorParameter.VictoryParameterName}, true) to set.");

                audioSource.clip = m_victoryBgm;
                audioSource.Play();
            };

            Debug.Log(" victoySetAction <color=green>to add</color>.");

            loseSetAction += (animator) =>
            {
                animator.SetBool(AnimatorParameter.LoseParameterName, true);
                Debug.Log($" animator.<yellow>SetBool</color>({AnimatorParameter.LoseParameterName}, true) to set.");

                audioSource.clip = m_loseBgm;
                audioSource.Play();
            };

            Debug.Log(" loseSetAction <color=green>to add</color>.");

            toFade.OnFadeIn("NowLoading...");

            setDisplayCharacterPrefab( statusManagement.GetCustomPropertiesSelectedCharacterNumber());

            setAnimatorTrigger();
        }

        #endregion

        #region unity pun collBacks 
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

        #endregion

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
                    victorySetAction(animator);
                }
                else 
                {
                    loseSetAction(animator);
                }
            }
        }

        #endregion
    }
}
