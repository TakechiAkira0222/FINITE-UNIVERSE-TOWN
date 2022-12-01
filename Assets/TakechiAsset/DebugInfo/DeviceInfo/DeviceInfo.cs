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

        systemInfo.Add("ユニークID", SystemInfo.deviceUniqueIdentifier);
        systemInfo.Add("OS情報", SystemInfo.operatingSystem);
        systemInfo.Add("CPU情報", SystemInfo.processorType);
        systemInfo.Add("メモリ情報", SystemInfo.systemMemorySize.ToString());

#if UNITY_IPHONE
		systemInfo.Add("iPhoneモデル名", UnityEngine.iOS.Device.generation.ToString());
#endif

        systemInfo.Add("モデル名", SystemInfo.deviceModel);
        systemInfo.Add("端末名", SystemInfo.deviceName);
        systemInfo.Add("端末タイプ", SystemInfo.deviceType.ToString());

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