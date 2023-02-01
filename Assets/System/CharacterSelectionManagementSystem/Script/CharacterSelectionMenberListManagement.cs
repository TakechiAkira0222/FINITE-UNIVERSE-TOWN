using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

using Takechi.UI.CanvasMune.DisplayListUpdate;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.Information;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.CharacterSelection
{
    public class CharacterSelectionMenberListManagement : DisplayListUpdateManagement
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;
        [Header("=== PlayableCharacterInformationManager ===")]
        [SerializeField] private PlayableCharacterInformationManager m_playableCharacterInformationManager;
        [Header("=== MemberList setting ===")]
        [SerializeField] private Image m_memberListImageInstans;
        [SerializeField] private GameObject m_teamAContent;
        [SerializeField] private GameObject m_teamBContent;
        [SerializeField] private List<GameObject> m_characterPrefabList = new List<GameObject>();
        [SerializeField] private List<Image> m_characterSelectionButtonList = new List<Image>();

        #endregion

        #region private variable
        private CharacterStatusManagement statusManagement => m_characterStatusManagement;
        private PlayableCharacterInformationManager informationManager => m_playableCharacterInformationManager;
        private List<Player> m_teamA_memberList = new List<Player>();
        private List<Player> m_teamB_memberList = new List<Player>();

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
            StartCoroutine(nameof(ListUpdate));
        }
        #endregion

        #region unity event
        private void Start()
        {
            if (!statusManagement.GetIsLocal()) return;

            StartAfterSync(NetworkSyncSettings.connectionSynchronizationTime);
        }

        #endregion

        #region set variable

        private void setDisplayCharacterPrefab(int index)
        {
            foreach (GameObject o in m_characterPrefabList) { o.SetActive(false); }
            m_characterPrefabList[index].SetActive(true);
        }

        private void setCharacterSelectionButtonColor(int index)
        {
            foreach (Image n in m_characterSelectionButtonList) { n.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0.5f); }
            m_characterSelectionButtonList[index].color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.9f);
        }

        #endregion

        #region IEnumerator Update 
        protected override IEnumerator ListUpdate()
        {
            base.ListUpdate();

            while (this.gameObject.activeSelf)
            {
                m_teamA_memberList = new List<Player>();
                m_teamB_memberList = new List<Player>();

                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    if (statusManagement.GetCustomPropertiesTeamName(player) == CharacterTeamStatusName.teamAName)
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
                Image image = Instantiate(m_memberListImageInstans, m_teamAContent.transform);
                image.sprite = informationManager.GetInformation( statusManagement.GetCustomPropertiesSelectedCharacterNumber(n)).GetCharacterIcon();
                image.transform.GetChild(0).GetComponent<Text>().text = n.NickName;
            }

            foreach (Player n in m_teamB_memberList)
            {
                Image image = Instantiate(m_memberListImageInstans, m_teamBContent.transform);
                image.sprite = informationManager.GetInformation(statusManagement.GetCustomPropertiesSelectedCharacterNumber(n)).GetCharacterIcon();
                image.transform.GetChild(0).GetComponent<Text>().text = n.NickName;
            }
        }

        #endregion

        #region event system finction
        public void OnSelected(int num)
        {
            if (!PhotonNetwork.LocalPlayer.IsLocal) return;

            setDisplayCharacterPrefab(num);
            setCharacterSelectionButtonColor(num);
        }

        #endregion
    }
}
