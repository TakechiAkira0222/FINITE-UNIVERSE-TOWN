using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Takechi.ManagmentButton.SrateChange
{
    public class ButtonSceneStateChangeManagement : MonoBehaviour
    {
        private void Reset()
        {
        }

        public void OnSceneChange(int number)
        {
            SceneManager.LoadScene(number);
            Debug.Log($"<color=yellow> SceneChange </color>({number})");
        }

        public void OnSceneChange(string name)
        {
            SceneManager.LoadScene(name);
            Debug.Log($"<color=yellow> SceneChange </color>({name})");
        }
    }
}