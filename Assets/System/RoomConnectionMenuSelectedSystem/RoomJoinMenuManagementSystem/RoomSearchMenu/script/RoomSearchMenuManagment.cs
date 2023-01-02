using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using Photon.Pun;
using Photon.Realtime;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

using Takechi.ServerConnect.ToJoinRoom;
using Takechi.ScriptReference.CustomPropertyKey;

namespace Takechi.UI.RoomSerach
{
    public class RoomSearchMenuManagment : MonoBehaviourPunCallbacks
    {
        [SerializeField] private ConnectToJoinRoomManagment m_connectToJoinRoomManagment;
        
        [Header(" System Setting")]
        [SerializeField] private GameObject m_content;
        [SerializeField] private Button     m_instansObject;
        [SerializeField] private int        m_updateRate = 60;

        private ConnectToJoinRoomManagment connectToJoinRoomManagment =>
            m_connectToJoinRoomManagment;

        private List<RoomInfo> m_roomInfoList =>
            m_connectToJoinRoomManagment.GetRoomInfoList();

        private Dictionary<string, RoomInfo> m_roomInfoDictionary =>
            m_connectToJoinRoomManagment.GetRoomInfoDictionary();

        #region Unity Event
        public override void OnEnable()
        {
            base.OnEnable();

            RoomInfoButtonInstantiation( m_roomInfoList);

            StartCoroutine(nameof( AutomaticUpdating));

            Debug.Log("<color=yellow> RoomSearchMenuManagment.OnEnable </color> : " +
                "connectToJoinRoomManagment.OnRoomListUpdateAction : <color=green>to add</color>");
        }

        public override void OnDisable()
        {
            base.OnDisable();

            DestroyChildObjects( m_content);

            Debug.Log("<color=yellow> RoomSearchMenuManagment.OnDisable </color> : " +
                "connectToJoinRoomManagment.OnRoomListUpdateAction : <color=green>to remove</color>");
        }

        #endregion

        #region event system function

        public void OnRoomInfoListUpdate()
        {
            RoomInfoListUpdate();
        }

        public void OnSearch(Text text)
        {
            RoomInfo roomInfo;

            if (TryGetRoomInfo(text.text, out roomInfo))
            {
                Debug.Log(roomInfo.Name);
            }
            else
            {
                Debug.Log("存在しません。");
            }
        }

        #endregion

        #region IEnumerator
        private IEnumerator AutomaticUpdating()
        {
            while (this.gameObject.activeSelf)
            {
                RoomInfoListUpdate();
                yield return new WaitForSeconds(Time.deltaTime * m_updateRate);
            }
        }

        #endregion

        #region private function
        private void RoomInfoButtonInstantiation( List<RoomInfo> list)
        {
            foreach ( RoomInfo info in list)
            {
                Button button = Instantiate( m_instansObject, m_content.transform);

                button.name = info.Name;
                button.transform.GetChild(0).gameObject.GetComponent<Text>().text = WhatTheTextDisplays(info);
                button.onClick.AddListener(() => connectToJoinRoomManagment.OnNameReferenceJoinRoom((button)));
            }
        }

        private void DestroyChildObjects( GameObject parent)
        { 
            foreach ( Transform n in parent.transform) 
            { 
               GameObject.Destroy( n.gameObject); 
            }
        }

        private void RoomInfoListUpdate()
        {
            DestroyChildObjects( m_content);
            RoomInfoButtonInstantiation( m_roomInfoList);

            Debug.Log(" Room Info <color=green>ListUpdate</color> ");
        }

        private string WhatTheTextDisplays(RoomInfo roomInfo)
        {
            return ($"{roomInfo.Name} \n " +
                    $"{roomInfo.PlayerCount}/{roomInfo.MaxPlayers}\n" +
                    $"{roomInfo.CustomProperties[CustomPropertyKeyReference.s_RoomSatusMap]}\n" +
                    $"{roomInfo.CustomProperties[CustomPropertyKeyReference.s_RoomStatusGameType]}");
        }

        // 指定したルーム名のルーム情報があれば取得する
        private bool TryGetRoomInfo(string roomName, out RoomInfo roomInfo)
        {
            return m_roomInfoDictionary.TryGetValue(roomName, out roomInfo);
        }

        #endregion
      
    }
}
