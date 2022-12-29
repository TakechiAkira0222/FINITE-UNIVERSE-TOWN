using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.ManagmentButton.SrateChange
{
    public class ButtonObjectStateChangeManagement : MonoBehaviour
    {
        public void OnDestroyManagement(GameObject obj)
        {
            Destroy(obj);
            Debug.Log($" <color=yellow>Destroy</color>({obj.name})");
        }

        public void OnSetActiveManagemnt_ture(GameObject obj)
        {
            obj.SetActive(true);
            Debug.Log($" {obj.name}.<color=yellow>SetActive</color>(<color=blue>true</color>)");
        }

        public void OnSetActiveManagemnt_false(GameObject obj)
        {
            obj.SetActive(false);
            Debug.Log($" {obj.name}.<color=yellow>SetActive</color>(<color=blue>false</color>)");
        }

        public void OnIntaractableManagemnt_ture(Button button)
        {
            button.interactable = true;
            Debug.Log($" {button.name}.<color=yellow>Intaractable</color>(<color=blue>true</color>)");
        }

        public void OnIntaractableManagemnt_false(Button button)
        {
            button.interactable = false;
            Debug.Log($" {button.name}.<color=yellow>Intaractable</color>(<color=blue>false</color>)");
        }
    }
}
