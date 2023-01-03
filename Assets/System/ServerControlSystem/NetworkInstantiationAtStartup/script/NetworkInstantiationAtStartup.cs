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
        private int m_selectedCharacterIndex => (int)PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.selectedCharacterKey];

        [SerializeField] private int m_syncTimeFlame = 1000;

        [SerializeField] private Transform m_respawnPointA;
        [SerializeField] private Transform m_respawnPointB;

        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private List<string> m_folderName = new List<string>();
        [SerializeField] private List<GameObject> m_networkInstancePrefab = new List<GameObject>();

        void Awake()
        {
            if (m_photonView == null) m_photonView = photonView;
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

            string path = "";

            foreach (string s in m_folderName)
            {
                path += s + "/";
            }

            if ((PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamKey] is string value ? value : "null") ==
                CharacterStatusTeamName.teamAName)
            {
                PhotonNetwork.Instantiate(path + m_networkInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointA.position, m_respawnPointA.rotation);
            }
            else
            {
                PhotonNetwork.Instantiate(path + m_networkInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointB.position, m_respawnPointB.rotation);
            }
        }
    }
}