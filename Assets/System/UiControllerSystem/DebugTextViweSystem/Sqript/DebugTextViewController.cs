using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.DebugView
{
    public class DebugTextViewController : MonoBehaviour
    {
        [SerializeField] private KeyCode m_keyCode;
        [SerializeField] private GameObject m_debugCanvas;

        void Update()
        {
            if (Input.GetKeyDown( m_keyCode))
            {
                m_debugCanvas.SetActive( m_debugCanvas.activeSelf == true ? false : true);
            }
        }
    }
}