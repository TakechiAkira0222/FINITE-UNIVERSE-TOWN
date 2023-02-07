using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ButtonManager.GameExit
{
    public class ButtonManager_GameExit : MonoBehaviour
    {
       public void On_Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
#elif UNITY_ANDROID
            UnityEngine.Application.Quit();
#endif
        }
    }
}
