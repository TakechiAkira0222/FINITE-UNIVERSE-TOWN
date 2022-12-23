using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.UI.CanvasMune
{
	public class CanvasSetActiveController : MonoBehaviour
	{
		[SerializeField , Tooltip("“¯Žž‚É•\Ž¦‚·‚é")]
        private List<GameObject> m_displaySettings;
		[SerializeField , Tooltip("”ñ•\Ž¦‚É‚·‚é")]
        private List<GameObject> m_hideSetting;

        private void OnEnable()
		{
            foreach ( GameObject o in m_displaySettings) { o.SetActive(true); }
            foreach ( GameObject o in m_hideSetting) { o.SetActive(false); }
        }
	}
}
