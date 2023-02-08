using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Takechi.MouseCursorController
{
    public class MouseCursorImageSettingController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Texture2D m_defaultHandCursor;


        private void Awake()
        {
            DontDestroyOnLoad( this.gameObject);
            Cursor.SetCursor(m_defaultHandCursor, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(m_defaultHandCursor, new Vector2(m_defaultHandCursor.width / 2, m_defaultHandCursor.height / 2), CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}