using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TakechiEngine.PUN.ServerConnect.Joined;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using UnityEngine.SceneManagement;
using Takechi.CharacterController.Parameters;
using Photon.Pun;
using Takechi.PlayableCharacter.FadingCanvas;

namespace Takechi.UI.ResultMune
{
    public class ResultMuneManagement : TakechiJoinedPunCallbacks
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [SerializeField] private List<GameObject> m_characterPrefabList = new List<GameObject>();
        [SerializeField] private ToFade m_toFade;

        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private List<GameObject> characterPrefabList => m_characterPrefabList;
        private ToFade toFade => m_toFade;

        private void Awake()
        {
            if ( !statusManagement.GetIsLocal()) return;

            toFade.OnFadeIn("NowLoading...");

            setDisplayCharacterPrefab( statusManagement.GetCustomPropertiesSelectedCharacterNumber());
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

        #endregion
    }
}
