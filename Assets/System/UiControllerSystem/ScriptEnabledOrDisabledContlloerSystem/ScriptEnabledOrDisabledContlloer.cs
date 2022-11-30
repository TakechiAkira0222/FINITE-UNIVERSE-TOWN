using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace Takechi.MouseCursorController
{
    public class ScriptEnabledOrDisabledContlloer : MonoBehaviour
    {
        [SerializeField] private bool m_OnEnableEnabled = true;
        [SerializeField] private bool m_StartEnabled = true;
        [SerializeField] private bool m_OnDisableEnabled = true;
        [SerializeField] private bool m_OnDestroyEnabled = true;

        [SerializeField]
        private List<MonoBehaviour> m_className =
            new List<MonoBehaviour>(3);

        void OnEnable()
        {
            setClass(m_OnEnableEnabled);
        }

        void Start()
        {
            setClass(m_StartEnabled);
        }


        void OnDisable()
        {
            setClass(m_OnDisableEnabled);
        }

        void OnDestroy()
        {
            setClass(m_OnDestroyEnabled);
        }

        void setClass(bool flag)
        {
            foreach (MonoBehaviour mono in m_className)
            {
                mono.enabled = flag;
            }
        }
    }
}
