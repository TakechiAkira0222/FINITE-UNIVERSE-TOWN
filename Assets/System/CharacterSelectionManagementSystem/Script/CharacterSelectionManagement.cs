using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using Takechi.UI.CanvasMune.DisplayListUpdate;
using Takechi.CharacterController.Parameters;

using Photon.Pun;
using Photon.Realtime;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using Takechi.PlayableCharacter.FadingCanvas;

namespace Takechi.CharacterSelection
{
    /// <summary>
    /// キャラクター選択画面を、管理する。
    /// </summary>
    public class CharacterSelectionManagement : DisplayListUpdateManagement
    {
        #region SerializeField
        [Header("=== PlayableCharacterParametersManager ===")]
        [SerializeField] private PlayableCharacterParametersManager m_parametersManager;
        [Header("=== memberList setting ===")]
        [SerializeField] private Image m_instansImage;
        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;
        [Header("=== ui setting ===")]
        [SerializeField] private int    m_nearTimeToStartTheGame_seconds = 5;
        [SerializeField] private float  m_gameStartTimeCunt_seconds = 30;
        [SerializeField] private Text   m_gameStartTimeCuntText;
        [SerializeField] private Text   m_infometionText;
        [SerializeField] private ToFade m_toFade;
        [SerializeField] private List <GameObject> m_characterPrefabList = new List<GameObject>();
        [SerializeField] private List <Image> m_characterSelectionButtonList = new List<Image>();

        #endregion

        #region private variable
        private List<Player> m_teamA_memberList = new List<Player>();
        private List<Player> m_teamB_memberList = new List<Player>();
        private bool m_isSelectedTime => m_gameStartTimeCunt_seconds > 0 ? true : false;

        #endregion

        #region set variable
        private void setInformationText(int num)
        {
            m_infometionText.text = m_parametersManager.GetParameters(num).GetInformation();
        }
        private void setInformationText(string name)
        {
            m_infometionText.text = m_parametersManager.GetParameters(name).GetInformation();
        }
        private void setDisplayCharacterPrefab(int index)
        {
            foreach (GameObject o in m_characterPrefabList) { o.SetActive(false); }
            m_characterPrefabList[index].SetActive(true);
        }
        private void setCharacterSelectionButtonColor(int index)
        {
            foreach (Image n in m_characterSelectionButtonList) { n.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0.5f); }
            m_characterSelectionButtonList[index].color = new Color( Color.red.r, Color.red.g, Color.red.b, 0.9f);
        }

        #endregion

        #region private async
        /// <summary>
        /// 同期時間経過後に、実行されます。
        /// </summary>
        /// <param name="s"></param>
        private async void StartAfterSync(int s)
        {
            await Task.Delay(s);

            setDisplayCharacterPrefab(0);
            setCharacterSelectionButtonColor(0);
            StartCoroutine(nameof(TimeUpdate));
            StartCoroutine(nameof(ListUpdate));

            RoomInfoAndJoinedPlayerInfoDisplay( RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
        }

        #endregion

        #region unity event
        private void Start()
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            m_toFade.OnFadeIn("NowLoading...");
            setInformationText(0);
            setLocalPlayerCustomProperties(CharacterStatusKey.selectedCharacterKey, 0);

            StartAfterSync( NetworkSyncSettings.connectionSynchronizationTime);
        }

        #endregion

        #region IEnumerator Update 
        private IEnumerator TimeUpdate()
        {
            while (this.gameObject.activeSelf)
            {
                if (m_isSelectedTime)
                {
                    m_gameStartTimeCunt_seconds -= Time.deltaTime;
                    m_gameStartTimeCuntText.text = Mathf.Ceil(m_gameStartTimeCunt_seconds).ToString();

                    if (Mathf.Ceil( m_gameStartTimeCunt_seconds) == m_nearTimeToStartTheGame_seconds) { m_gameStartTimeCuntText.color = new Color(1, 0, 0); }

                    yield return null;
                }
                else
                {
                    m_toFade.OnFadeOut("NowLoading...");

                    StartCoroutine( DelayMethod(NetworkSyncSettings.fadeProductionTime_Second, () =>
                    {
                        SceneSyncChange(3);
                    }));

                    yield return new WaitForSeconds(NetworkSyncSettings.fadeProductionTime_Second * 2);
                }
            }
        }
        protected override IEnumerator ListUpdate()
        {
            base.ListUpdate();

            while (this.gameObject.activeSelf)
            {
                m_teamA_memberList = new List<Player>();
                m_teamB_memberList = new List<Player>();

                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if ((player.CustomProperties[CharacterStatusKey.teamKey] is string value ? value : "null") == CharacterTeamStatusName.teamAName)
                    {
                        m_teamA_memberList.Add(player);
                    }
                    else
                    {
                        m_teamB_memberList.Add(player);
                    }
                }

                RefreshTheListViewing();
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        protected override void RefreshTheListViewing()
        {
            base.RefreshTheListViewing();

            DestroyChildObjects(m_teamAContent);
            DestroyChildObjects(m_teamBContent);

            foreach (Player n in m_teamA_memberList)
            { 
                Image image = Instantiate( m_instansImage, m_teamAContent.transform);
                image.sprite =  m_parametersManager.GetParameters((int)n.CustomProperties[CharacterStatusKey.selectedCharacterKey]).GetIcon();
                image.transform.GetChild(0).GetComponent<Text>().text = n.NickName; 
            }

            foreach (Player n in m_teamB_memberList)
            {
                Image image = Instantiate(m_instansImage, m_teamBContent.transform);
                image.sprite = m_parametersManager.GetParameters((int)n.CustomProperties[CharacterStatusKey.selectedCharacterKey]).GetIcon();
                image.transform.GetChild(0).GetComponent<Text>().text = n.NickName;
            }
        }

        #endregion

        #region event system finction
        public void OnSelected(int num)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            setInformationText(num);
            setLocalPlayerCustomProperties( CharacterStatusKey.selectedCharacterKey, num);
            setDisplayCharacterPrefab((int)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.selectedCharacterKey]);
            setCharacterSelectionButtonColor((int)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.selectedCharacterKey]);

            RoomInfoAndJoinedPlayerInfoDisplay(RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
        }

        #endregion

    }
}
