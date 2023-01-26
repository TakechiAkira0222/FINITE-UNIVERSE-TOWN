using Photon.Pun;
using Photon.Realtime;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;

using TakechiEngine.PUN.ServerConnect.Joined;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;

namespace Takechi.ServerConnect.NetworkInstantiation
{
    public class NetworkInstantiationAtStartup : TakechiJoinedPunCallbacks
    {
        /// <summary>
        /// Player CustomProperty
        /// </summary>
        private int   m_selectedCharacterIndex =>  (int)PhotonNetwork.LocalPlayer.CustomProperties[ CharacterStatusKey.selectedCharacterNumberKey];

        /// <summary>
        /// Room CustomProperty
        /// </summary>
        private string m_selectedGameTypeName => (string)PhotonNetwork.CurrentRoom.CustomProperties[ RoomStatusKey.gameTypeKey];

        [SerializeField] private PhotonView   m_photonView;

        [SerializeField] private Transform    m_respawnPointA;
        [SerializeField] private Transform    m_respawnPointB;

        [SerializeField] private List<string>     m_playableCharacterFolderName = new List<string>();
        [SerializeField] private List<GameObject> m_playableCharacterInstancePrefab = new List<GameObject>();

        [SerializeField] private GameObject m_gameSystemInstansHardpointPrefab;
        [SerializeField] private GameObject m_gameSystemInstansDominationPrefab;

        private Dictionary<string, Action>  m_storeActionDictionary = new Dictionary<string, Action>();

        public class StoreAction
        {
            public Action selectedDomination = delegate { };
            public Action selectedHardpoint = delegate { };

            public StoreAction( GameObject InstansDomination, GameObject InstansHardpoint) 
            {
                selectedDomination += () => 
                {
                    InstansHardpoint.gameObject.SetActive(false);
                    InstansDomination.gameObject.SetActive(true);
                };

                selectedHardpoint += () => 
                {
                    InstansDomination.gameObject.SetActive(false);
                    InstansHardpoint.gameObject.SetActive(true);
                };
            }

            ~StoreAction() 
            {
                selectedDomination = delegate { };
                selectedHardpoint = delegate { };
            }
        }

        void Awake()
        {
            if ( m_photonView == null) m_photonView = photonView;

            StoreAction storeAction = 
                new StoreAction( m_gameSystemInstansDominationPrefab, m_gameSystemInstansHardpointPrefab);

            m_storeActionDictionary.Add( RoomStatusName.GameType.domination, storeAction.selectedDomination);
            Debug.Log($" m_storeActionDictionary.<color=yellow>Add</color>( {RoomStatusName.GameType.domination}, storeAction.selectedDomination)");

            m_storeActionDictionary.Add( RoomStatusName.GameType.hardpoint,  storeAction.selectedHardpoint);
            Debug.Log($" m_storeActionDictionary.<color=yellow>Add</color>( {RoomStatusName.GameType.hardpoint}, storeAction.selectedHardpoint)");
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
            await Task.Delay( NetworkSyncSettings.instantiationSettingSynchronizationTime);

            string playableCharacterFolderPath = "";

            foreach (string s in m_playableCharacterFolderName) { playableCharacterFolderPath += s + "/"; }

            if ((PhotonNetwork.LocalPlayer.CustomProperties[CharacterStatusKey.teamNameKey] is string value ? value : "null") ==
                CharacterTeamStatusName.teamAName)
            {
                PhotonNetwork.Instantiate(playableCharacterFolderPath + m_playableCharacterInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointA.position, m_respawnPointA.rotation);
            }
            else
            {
                PhotonNetwork.Instantiate(playableCharacterFolderPath + m_playableCharacterInstancePrefab[m_selectedCharacterIndex].name, m_respawnPointB.position, m_respawnPointB.rotation);
            }

            m_storeActionDictionary[m_selectedGameTypeName]();
        }
    }
}