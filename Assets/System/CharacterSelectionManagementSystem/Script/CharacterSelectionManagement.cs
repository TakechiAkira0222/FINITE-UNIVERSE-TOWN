using Photon.Voice.PUN.UtilityScripts;

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

namespace Takechi.CharacterSelection
{
    public class CharacterSelectionManagement : DisplayListUpdateManagement
    {
        [SerializeField] private PlayableCharacterParametersManager m_parametersManager;

        [SerializeField] private Text m_infometionText;
        [SerializeField] private Text m_gameStartTimeCuntText;
        [SerializeField] private float  m_gameStartTimeCunt_seconds = 30;

        [SerializeField] private Image m_instansImage;
        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;
        [SerializeField] private List <GameObject> m_characterPrefabList = new List<GameObject>();

        private List<Player> m_teamA_memberList = new List<Player>();
        private List<Player> m_teamB_memberList = new List<Player>();

        private bool m_isSelectedTime => m_gameStartTimeCunt_seconds > 0 ? true : false;

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
            StartCoroutine(nameof(TimeUpdate));
            StartCoroutine(nameof(ListUpdate));
        }

        #endregion

        #region unity event
        private void Start()
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            setInformationText(0);
            setLocalPlayerCustomProperties(CharacterStatusKey.selectedCharacterKey, 0);
            

            RoomInfoAndJoinedPlayerInfoDisplay(RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
            StartAfterSync(NetworkSyncSettings.connectionSynchronizationTime);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            /// フェイド処理などを入れても良き
            
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
                    yield return null;
                }
                else
                {
                    SceneSyncChange(3);
                    this.gameObject.gameObject.SetActive(false);
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
                    if ((player.CustomProperties[CharacterStatusKey.teamKey] is string value ? value : "null") == CharacterStatusTeamName.teamAName)
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
            }

            foreach (Player n in m_teamB_memberList)
            {
                Image image = Instantiate(m_instansImage, m_teamBContent.transform);
                image.sprite = m_parametersManager.GetParameters((int) n.CustomProperties[CharacterStatusKey.selectedCharacterKey]).GetIcon();
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

            RoomInfoAndJoinedPlayerInfoDisplay(RoomStatusKey.allKeys, CharacterStatusKey.allKeys);
        }

        #endregion

    }
}
