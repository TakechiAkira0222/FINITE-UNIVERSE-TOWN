using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.CanvasMune
{
    public class CanvasIntaractableController : MonoBehaviour
    {
        [SerializeField, Tooltip("���ɂ���")]
        private List<Button> m_interference;
        [SerializeField, Tooltip("�񊱏ɂ���")]
        private List<Button> m_notInterference;

        private void OnEnable()
        {
            foreach (Button o in m_interference) { o.interactable = true; }
            foreach (Button o in m_notInterference) { o.interactable = false; }
        }
    }
}