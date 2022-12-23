using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.UI.MapSelection
{
    public class MapSelectionManagement : MonoBehaviour
    {
        [SerializeField] 
        private GameObject m_uiFrame;

        [SerializeField]
        private float m_slideSpeed = 2.0f;

        [SerializeField]
        private List<Transform> m_slidePosition =
            new List<Transform>(4);

        private int m_selectionIndex = 0;

        private void Update()
        {
            m_uiFrame.transform.localEulerAngles += new Vector3( 0, 0.3f, 0);
            m_uiFrame.transform.localPosition = 
                Vector3.Lerp( m_uiFrame.transform.localPosition, setXpos( m_slidePosition[m_selectionIndex].localPosition.x), Time.deltaTime * m_slideSpeed);
        }

        private Vector3 setXpos(float x)
        {
            return new Vector3( x, m_uiFrame.transform.localPosition.y, m_uiFrame.transform.localPosition.z);
        }

        public void OnSelect(int Index)
        {
            m_selectionIndex = Index;
        }
    }
}
