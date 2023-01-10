using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using Takechi.UI.CanvasMune.CursorMnagement;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

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
        /// Map選択の名前　Get
        /// </summary>
        /// <returns></returns>
        /// 名前に変更途中
        public string GetMapSelectionName() { return RoomStatusName.mapSelectionNameArray[GetSelectionIndex()]; }

        private void OnEnable()
        {
            StartCoroutine(uiCursorRotation(m_uiCursor));
        }

        public void OnSelect(int Index)
        {
            setSelectionIndex(Index);
            StartCoroutine(slideCursor(m_uiCursor, m_slidePosition));
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
