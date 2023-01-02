using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using Takechi.UI.CanvasMune.CursorMnagement;

namespace Takechi.UI.MapSelection
{
    /// <summary>
    /// マップ選択メニューを、管理する。
    /// </summary>
    public class MapSelectionManagement : CursorMnagement
    {
        [SerializeField] 
        private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        /// <summary>
        /// Map選択の指数　Get
        /// </summary>
        /// <returns></returns>
        public int GetMapSelectionIndex() { return GetSelectionIndex(); }

        private void OnEnable() 
        {
            StartCoroutine( uiCursorRotation( m_uiCursor));
        }

        public void OnSelect(int Index)
        {
            setSelectionIndex(Index);
            StartCoroutine(slideCursor(m_uiCursor, m_slidePosition));
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
