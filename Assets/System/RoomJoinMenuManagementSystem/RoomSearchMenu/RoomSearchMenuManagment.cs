using System.Collections;
using System.Collections.Generic;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

using TakechiEngine.PUN.ServerConnect;

namespace Takechi.UI.RoomSerach
{
    public class RoomSearchMenuManagment : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject m_content;
        [SerializeField] private Button     m_instansObject;

        private List<RoomInfo> m_roomInfoList = new List<RoomInfo>();
        private Dictionary<string, RoomInfo> m_roomInfoDictionary = new Dictionary<string, RoomInfo>();

        public override void OnEnable()
        {
            base.OnEnable();
            foreach (RoomInfo info in m_roomInfoList) { Debug.Log(info.Name); }
        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach (RoomInfo info in m_roomInfoList) { Debug.Log(info.Name); }
        }

        public override void OnRoomListUpdate( List<RoomInfo> changedRoomList)
        {
            UpdateRoomInfo( changedRoomList);
            foreach(RoomInfo info in m_roomInfoList) { Debug.Log(info.Name); }
        }

        private void UpdateRoomInfo( List<RoomInfo> changedRoomList)
        {
            foreach( RoomInfo info in changedRoomList)
            {
                if (!m_roomInfoList.Contains(info)) 
                { 
                    m_roomInfoList.Add(info);
                    m_roomInfoDictionary.Add(info.Name, info);
                }
                else 
                { 
                    m_roomInfoList.Remove(info);
                    m_roomInfoDictionary.Remove(info.Name);
                }
            }
        }

        // �w�肵�����[�����̃��[����񂪂���Ύ擾����
        public bool TryGetRoomInfo( string roomName, out RoomInfo roomInfo)
        {
            return m_roomInfoDictionary.TryGetValue(roomName, out roomInfo);
        }

        /// <summary>
        /// ���X�g�̒��ɂ���I�u�W�F�N�g�������ƃ��X�g�̏�����������B
        /// </summary>
        /// <typeparam name="T"> �������������X�g�̌^ </typeparam>
        /// <param name="values">�@���������� �I�u�W�F�N�g���X�g </param>
        private void ErasingWithinAnElementOfList<T>(List<T> values) where T : MonoBehaviour
        {
            foreach (var v in values)
            {
                Destroy(v.gameObject);
            }

            values.Clear();
        }
    }
}
