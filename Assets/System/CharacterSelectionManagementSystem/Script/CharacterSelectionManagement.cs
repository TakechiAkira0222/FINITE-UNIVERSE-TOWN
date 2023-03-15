using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using Takechi.PlayableCharacter.FadingCanvas;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine.Rendering;

using TakechiEngine.PUN.Information;
using Takechi.CharacterController.Parameters;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.CharacterSelection
{
    /// <summary>
    /// キャラクター選択画面を、管理する。
    /// </summary>
    public class CharacterSelectionManagement : TakechiPunInformationDisclosure
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== Ui setting ===")]
        [SerializeField] private ToFade m_toFade;
        [SerializeField] private Text   m_gameStartTimeCuntText;
        [SerializeField] private float  m_gameStartTimeCunt_seconds = 30;
#if UNITY_EDITOR
        [SerializeField] private int    m_nearTimeToStartTheGame_seconds = 5;
#else
        private int    m_nearTimeToStartTheGame_seconds = 60;
#endif

#endregion

#region private variable
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private bool isSelectedTime => m_gameStartTimeCunt_seconds > 0 ? true : false;
        private Dictionary<string, Action> m_sceneChangeDictionary = new Dictionary<string, Action>();

#endregion

#region private async
        /// <summary>
        /// 同期時間経過後に、実行されます。
        /// </summary>
        /// <param name="s"></param>
        private async void StartAfterSync(int s)
        {
            await Task.Delay(s);

            StartCoroutine(nameof(TimeUpdate));

            RoomInfoAndJoinedPlayerInfoDisplay( RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
        }

#endregion

#region unity event
        private void Awake()
        {
            m_sceneChangeDictionary.Add( RoomStatusName.Map.futureCity, () => { SceneSyncChange(SceneName.futureCityScene);});
            Debug.Log(" m_sceneChangeDictionary.<color=yellow>Add</color>(<color=green>RoomStatusName</color>.futureCity, () => { <color=yellow>SceneSyncChange</color>(<color=green>SceneName</color>.futureCityScene);});");
        }

        private void Start()
        {
            if (!statusManagement.GetIsLocal()) return;

            m_toFade.OnFadeIn("NowLoading...");

            statusManagement.SetCustomPropertiesSelectedCharacterNumber(0);

            StartAfterSync( NetworkSyncSettings.connectionSynchronizationTime);
        }

#endregion

#region IEnumerator Update 
        private IEnumerator TimeUpdate()
        {
            while (this.gameObject.activeSelf)
            {
                if (isSelectedTime)
                {
                    m_gameStartTimeCunt_seconds -= Time.deltaTime;
                    m_gameStartTimeCuntText.text = Mathf.Ceil( m_gameStartTimeCunt_seconds).ToString();

                    if (Mathf.Ceil( m_gameStartTimeCunt_seconds) == m_nearTimeToStartTheGame_seconds) { m_gameStartTimeCuntText.color = new Color( 1, 0, 0); }

                    yield return null;
                }
                else
                {
                    m_toFade.OnFadeOut("NowLoading...");

                    StartCoroutine( DelayMethod(NetworkSyncSettings.fadeProductionTime_Seconds, () =>
                    {
                        m_sceneChangeDictionary[(string)PhotonNetwork.CurrentRoom.CustomProperties[RoomStatusKey.mapKey]]();
                    }));

                    yield return new WaitForSeconds( NetworkSyncSettings.fadeProductionTime_Seconds * 2);
                }
            }
        }

#endregion


#region event system finction
        public void OnSelected(int num)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            statusManagement.SetCustomPropertiesSelectedCharacterNumber(num);

            RoomInfoAndJoinedPlayerInfoDisplay(RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
        }

#endregion

    }
}
