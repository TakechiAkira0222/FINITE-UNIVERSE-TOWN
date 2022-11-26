using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDebugView : MonoBehaviour
{
    [SerializeField]
    Text m_addTextprefab;

    [SerializeField]
    Transform m_perentViweContent;

    [SerializeField]
    bool m_showDebug = true;

    private void Awake()
    {
        if (!m_showDebug) 
        {
            this.gameObject.SetActive(false);
            return;
        }

        Application.logMessageReceived += LoggedCb;
    }

    private void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        Text text = Instantiate(m_addTextprefab, m_perentViweContent);
        text.text = logstr +"<color=red>.</color>";
    }
}
