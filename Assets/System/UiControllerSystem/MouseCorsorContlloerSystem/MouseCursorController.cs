using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace Takechi.MouseCursorController
{
    public class MouseCursorController : MonoBehaviour
    {
        [SerializeField] private CursorLockMode m_startCursorLockMode;
        [SerializeField] private CursorLockMode m_destotryCursorLockMode;

        void Start()
        {
            Cursor.lockState = m_startCursorLockMode;
        }

        void OnDestroy()
        {
            Cursor.lockState = m_destotryCursorLockMode;
        }
    }
}
