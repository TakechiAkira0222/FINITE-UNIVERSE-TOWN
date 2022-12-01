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
        private List<MonoBehaviour> m_serializeSettingClassName =
            new List<MonoBehaviour>(4);

        [SerializeField]
        private List<string> m_findAndChangeObjectName =
            new List<string>(4);

        void OnEnable()
        {
            setSerializeSettingClass(m_OnEnableEnabled);
            setFindsetClass(m_OnEnableEnabled);
        }

        void Start()
        {
            setSerializeSettingClass(m_StartEnabled);
            setFindsetClass(m_StartEnabled);
        }

        void OnDisable()
        {
            setSerializeSettingClass(m_OnDisableEnabled);
            setFindsetClass(m_OnDisableEnabled);
        }

        void OnDestroy()
        {
            setSerializeSettingClass(m_OnDestroyEnabled);
            setFindsetClass(m_OnDestroyEnabled);
        }

        void setSerializeSettingClass(bool flag)
        {
            foreach (MonoBehaviour mono in m_serializeSettingClassName)
            {
               if(mono != null) mono.enabled = flag;
            }
        }

        void setFindsetClass(bool flag)
        {
            foreach (string s in m_findAndChangeObjectName)
            {
                if (GameObject.Find(s) != null) GameObject.Find(s).GetComponent<MonoBehaviour>().enabled = flag;
            }
        }

    }
}
