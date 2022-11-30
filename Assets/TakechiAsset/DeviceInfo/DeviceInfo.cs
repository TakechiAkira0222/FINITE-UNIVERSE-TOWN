using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeviceInfo : MonoBehaviour
{
    [SerializeField] private KeyCode m_deviceInfoButton = KeyCode.F2;
    private string m_deviceInfo = "";

    void Start()
    {
        Dictionary<string, string> systemInfo = new Dictionary<string, string>();

        systemInfo.Add("���j�[�NID", SystemInfo.deviceUniqueIdentifier);
        systemInfo.Add("OS���", SystemInfo.operatingSystem);
        systemInfo.Add("CPU���", SystemInfo.processorType);
        systemInfo.Add("���������", SystemInfo.systemMemorySize.ToString());

#if UNITY_IPHONE
		systemInfo.Add("iPhone���f����", UnityEngine.iOS.Device.generation.ToString());
#endif

        systemInfo.Add("���f����", SystemInfo.deviceModel);
        systemInfo.Add("�[����", SystemInfo.deviceName);
        systemInfo.Add("�[���^�C�v", SystemInfo.deviceType.ToString());

        foreach (string key in systemInfo.Keys)
        {
            m_deviceInfo += key + " = " + systemInfo[key] + "\n";
        }
    }

    private void OnGUI()
    {
       if(Input.GetKey( m_deviceInfoButton)) GUILayout.Box( m_deviceInfo);
    }
}