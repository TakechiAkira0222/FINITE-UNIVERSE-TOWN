using System.Collections;
using System.Collections.Generic;
using Takechi.UI.CanvasMune.CursorMnagement;
using UnityEngine;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;

namespace Takechi.UI.GameTypeSelection
{
    /// <summary>
    /// �Q�[����ނ̑I�����j���[���Ǘ�����B
    /// </summary>
    public class GameTypeSelectionManagement : CursorMnagement
    {
        [SerializeField]
        private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        /// <summary>
        /// �Q�[���^�C�v�̖��O�@Get
        /// </summary>
        /// <returns></returns>
        public string GetGameTypeSelectionName() { return RoomStatusName.gameTypeSelectionNameArray[GetSelectionIndex()]; }

        private void OnEnable()
        {
            StartCoroutine( uiCursorRotation( m_uiCursor));
        }

        public void OnSelect(int Index)
        {
            setSelectionIndex(Index);
            StartCoroutine(slideCursor( m_uiCursor, m_slidePosition));
            Debug.Log($" <color=yellow>StartCoroutine</color>( <color=blue>nameof</color>(slideCursor))");
        }
    }
}
