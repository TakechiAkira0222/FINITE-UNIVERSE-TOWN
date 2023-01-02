using System.Collections;
using System.Collections.Generic;
using Takechi.UI.CanvasMune.CursorMnagement;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.RoomPropertySetting
{
    /// <summary>
    /// �����̃v���p�e�B�[�ݒ胁�j���[���A�Ǘ�����B
    /// </summary>
    public class RoomPropertySettingManagement : CursorMnagement
    {
        [SerializeField] private InputField m_nameSettingField;
        [SerializeField] private GameObject m_uiCursor;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        /// <summary>
        /// �Q���l���̎w���@Get
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
