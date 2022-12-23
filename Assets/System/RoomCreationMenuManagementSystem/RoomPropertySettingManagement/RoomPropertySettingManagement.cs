using System.Collections;
using System.Collections.Generic;
using Takechi.UI.CanvasMune.CursorMnagement;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.RoomPropertySetting
{
    public class RoomPropertySettingManagement : CursorMnagement
    {
        [SerializeField] private InputField m_nameSettingField;
        [SerializeField] private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        private int m_selectionIndex = 0;

        private void OnEnable() { StartCoroutine( uiCursorRotation( m_uiCursor)); }

        public string GetRoomName() { return m_nameSettingField.text; }
        public int GetSelectionIndex() { return m_selectionIndex; }
        public void OnSelect(int Index)
        {
            m_selectionIndex = Index;
            StartCoroutine( slideCursor( m_uiCursor, m_slidePosition, Index));
            Debug.Log($" On Select(int{Index}) selectionIndex : <color=blue>{m_selectionIndex}</color> <color=green>to set.</color>");
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
