using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.DontDestroy
{
    public class DontDestroyOnLoadSetting : MonoBehaviour
    {
        [SerializeField] private bool m_settingScript = false;
        [SerializeField] private bool m_settingGameObject = false;

        void Awake()
        {
           if(m_settingScript) DontDestroyOnLoad(this);
           if(m_settingGameObject) DontDestroyOnLoad(this.gameObject);
        }
    }
}