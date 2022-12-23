using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using Takechi.UI.CanvasMune.CursorMnagement;

namespace Takechi.UI.MapSelection
{
    public class MapSelectionManagement : CursorMnagement
    {
        [SerializeField] 
        private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        private int m_selectionIndex = 0;

        private void OnEnable() { StartCoroutine( uiCursorRotation( m_uiCursor)); }

        public int GetSelectionIndex() { return m_selectionIndex; }

        public void OnSelect(int Index)
        {
            m_selectionIndex = Index;
            StartCoroutine( slideCursor( m_uiCursor, m_slidePosition, Index));
            Debug.Log($" On Select( int {Index}) selectionIndex : <color=blue>{m_selectionIndex}</color> <color=green>to set.</color>");
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
