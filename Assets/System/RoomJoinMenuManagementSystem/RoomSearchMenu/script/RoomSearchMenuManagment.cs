using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

using TakechiEngine.PUN.ServerConnect;
using Takechi.ServerConnect.ToJoinRoom;
using UnityEngine.Rendering;

namespace Takechi.UI.RoomSerach
{
    public class RoomSearchMenuManagment : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject m_content;
        [SerializeField] private Button     m_instansObject;
        [SerializeField] private ConnectToJoinRoomManagment m_connectToJoinRoomManagment;

        private ConnectToJoinRoomManagment connectToJoinRoomManagment => m_connectToJoinRoomManagment;
        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();
        private Dictionary<string, RoomInfo> m_roomInfoDictionary = new Dictionary<string, RoomInfo>();

        public override void OnEnable()
        {
            base.OnEnable();

            RoomInfoButtonInstantiation( m_roomInfoList, m_content.transform);
        }
        public override void OnDisable()
        {
            base.OnDisable();

            DestroyChildObjects( m_content);
        }

        public override void OnRoomListUpdate( List<RoomInfo> changedRoomList)
        {
            base.OnRoomListUpdate(changedRoomList);

            // DestroyChildObjects( m_content);

            UpdateRoomInfo( changedRoomList);

            RoomInfoButtonInstantiation( m_roomInfoList, m_content.transform);
        }

        private void RoomInfoButtonInstantiation(List<RoomInfo> list, Transform parent)
        {
            foreach (RoomInfo info in list)
            {
                Button button = Instantiate( m_instansObject, parent);

                button.name = info.Name;
                button.onClick.AddListener(() => connectToJoinRoomManagment.OnNameReferenceJoinRoom((button)));
            }
        }

        private void DestroyChildObjects(GameObject parent) 
        { 
            foreach (Transform n in parent.transform) 
            { 
               GameObject.Destroy( n.gameObject); 
            } 
        }

        private void UpdateRoomInfo( List<RoomInfo> changedRoomList)
        {
            foreach( RoomInfo info in changedRoomList)
            {
                if (!m_roomInfoList.Contains(info)) 
                { 
                    m_roomInfoList.Add(info);

                    Debug.Log("add : " + info.Name);

                    m_roomInfoDictionary.Add(info.Name, info);
                }
                else 
                { 
                    m_roomInfoList.Remove(info);

                    Debug.Log("Remove : " + info.Name);

                    m_roomInfoDictionary.Remove(info.Name);
                }
            }
        }

        // 指定したルーム名のルーム情報があれば取得する
        public bool TryGetRoomInfo( string roomName, out RoomInfo roomInfo)
        {
            return m_roomInfoDictionary.TryGetValue( roomName, out roomInfo);
        }

        public void OnSearch(Text text)
        {
            RoomInfo roomInfo;

            if ( TryGetRoomInfo( text.text, out roomInfo))
            {
                Debug.Log( roomInfo.Name);
            }
            else
            {
                Debug.Log("存在しません。");
            }
        }
    }
}
