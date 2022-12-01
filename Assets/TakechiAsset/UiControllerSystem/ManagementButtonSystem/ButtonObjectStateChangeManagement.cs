using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.ManagmentButton.SrateChange
{
    public class ButtonObjectStateChangeManagement : MonoBehaviour
    {
        [SerializeField] private GameObject m_setStateObject;

        private void Reset()
        {
            m_setStateObject = this.gameObject;
        }

        public void OnDestroyManagement()
        {
            Destroy(m_setStateObject);
            Debug.Log($"<color=yellow>Destroy</color>({m_setStateObject.name})");
        }

        public void OnSetActiveManagemnt(bool flag)
        {
            m_setStateObject.SetActive(flag);
            Debug.Log($"{m_setStateObject.name}.<color=yellow>SetActive</color>(<color=blue>{flag}</color>)");
        }
    }
}
