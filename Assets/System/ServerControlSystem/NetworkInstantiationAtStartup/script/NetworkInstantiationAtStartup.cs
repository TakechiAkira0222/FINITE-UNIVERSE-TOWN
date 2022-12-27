using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using TakechiEngine.PUN.ServerConnect.Joined;

namespace Takechi.ServerConnect.NetworkInstantiation
{
    public class NetworkInstantiationAtStartup : TakechiJoinedPunCallbacks
    {
        [SerializeField] private int m_syncTimeFlame = 1000;
        [SerializeField] private PhotonView m_photonView;
        [SerializeField] private Transform m_location;
        [SerializeField] private Quaternion m_quaternion = Quaternion.identity;
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

            foreach ( GameObject prfab in m_networkInstancePrefab)
            {
                PhotonNetwork.Instantiate( path + prfab.name, m_location.position, m_quaternion);
            }
        }
    }
}