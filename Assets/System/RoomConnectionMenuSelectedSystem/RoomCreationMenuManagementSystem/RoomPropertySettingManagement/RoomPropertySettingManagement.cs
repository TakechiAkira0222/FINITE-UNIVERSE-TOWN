using System.Collections;
using System.Collections.Generic;
using Takechi.UI.CanvasMune.CursorMnagement;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.RoomPropertySetting
{
    /// <summary>
    /// 部屋のプロパティー設定メニューを、管理する。
    /// </summary>
    public class RoomPropertySettingManagement : CursorMnagement
    {
        [SerializeField] private InputField m_nameSettingField;
        [SerializeField] private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        /// <summary>
        /// 参加人数の指数　Get
        /// </summary>
        /// <returns></returns>
        public int GetParticipantsIndex() { return GetSelectionIndex(); }

        private void OnEnable() 
        { 
            StartCoroutine( uiCursorRotation( m_uiCursor));
        }

        public string GetRoomName() { return m_nameSettingField.text; }

        public void OnSelect(int Index)
        {
            setSelectionIndex(Index);
            StartCoroutine(slideCursor(m_uiCursor, m_slidePosition));
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
