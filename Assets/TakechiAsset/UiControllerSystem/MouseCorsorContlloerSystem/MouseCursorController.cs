using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace Takechi.MouseCursorController
{
    public class MouseCursorController : MonoBehaviour
    {
        [SerializeField] private CursorLockMode m_startCursorLockMode;
        [SerializeField] private CursorLockMode m_enableCursorLockMode;
        [SerializeField] private CursorLockMode m_disableCursorLockMode;
        [SerializeField] private CursorLockMode m_destotryCursorLockMode;

        void Start()
        {
            Cursor.lockState = m_startCursorLockMode;
        }

        void OnEnable()
        {
            Cursor.lockState = m_enableCursorLockMode;
        }

        void OnDisable()
        {
            Cursor.lockState = m_disableCursorLockMode;
        }

        void OnDestroy()
        {
            Cursor.lockState = m_destotryCursorLockMode;
        }
    }
}
