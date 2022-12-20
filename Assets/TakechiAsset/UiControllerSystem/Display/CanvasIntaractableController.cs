using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.CanvasMune
{
    public class CanvasIntaractableController : MonoBehaviour
    {
        [SerializeField, Tooltip("ŠÄŽ‹‚·‚éButton")]
        private List<Button> m_monitorButton;

        private void OnEnable()
        {
            foreach (Button o in m_monitorButton) { o.interactable = false; }
        }

        private void OnDisable()
        {
            foreach (Button o in m_monitorButton) { o.interactable = true; }
        }
    }
}