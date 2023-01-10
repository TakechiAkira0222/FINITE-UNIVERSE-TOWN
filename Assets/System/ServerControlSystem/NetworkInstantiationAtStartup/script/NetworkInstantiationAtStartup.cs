using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using TakechiEngine.PUN.ServerConnect.Joined;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.ServerConnect.NetworkInstantiation
{
    public class NetworkInstantiationAtStartup : TakechiJoinedPunCallbacks
    {
        private int m_selectedCharacterIndex =>  (int)PhotonNetwork.LocalPlayer.CustomProperties[ CharacterStatusKey.selectedCharacterKey];
        private string m_selectedGameTypeName => (string)PhotonNetwork.LocalPlayer.CustomProperties[ RoomStatusKey.gameTypeKey];

        [SerializeField] private int m_syncTimeFlame = 1000;

        [SerializeField] private PhotonView   m_photonView;

        [SerializeField] private Transform    m_respawnPointA;
        [SerializeField] private Transform    m_respawnPointB;

        [SerializeField] private List<string>     m_playableCharacterFolderName = new List<string>();
        [SerializeField] private List<GameObject> m_playableCharacterInstancePrefab = new List<GameObject>();

        [SerializeField] private GameObject m_gameSystemInstansHardpointPrefab;
        [SerializeField] private GameObject m_gameSystemInstansDominationPrefab;

        void Awake()
        {
            if ( m_photonView == null) m_photonView = photonView;
        }

        void Start()
        {
            if (PhotonNetwork.InRoom)
            {
                InstantiationSetting();
            }
        }

        private async void InstantiationSetting()
        {
            await Task.Delay(m_syncTimeFlame);

            string playableCharacterFolderPath = "";

            foreach (string s in m_playableCharacterFolderName) { playableCharacterFolderPath += s + "/"; }

            if ((PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] is string value ? value : "null") ==
                CharacterTeamStatusName.teamAName)
            {
                PhotonNetwork.Instantiate(playableCharacterFolderPath + m_playableCharacterInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointA.position, m_respawnPointA.rotation);
            }
            else
            {
                PhotonNetwork.Instantiate(playableCharacterFolderPath + m_playableCharacterInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointB.position, m_respawnPointB.rotation);
            }

            if ( m_selectedGameTypeName == RoomStatusName.domination)
            {
                m_gameSystemInstansDominationPrefab.gameObject.SetActive(true);
            }
            else
            {
                m_gameSystemInstansHardpointPrefab.gameObject.SetActive(true);
            }
        }
    }
}