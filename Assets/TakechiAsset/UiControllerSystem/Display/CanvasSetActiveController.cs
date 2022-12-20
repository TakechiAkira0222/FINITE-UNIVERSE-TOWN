using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Takechi.UI.CanvasMune
{
	public class CanvasSetActiveController : MonoBehaviour
	{
		[SerializeField , Tooltip("ŠÄŽ‹‚·‚éObject")] 
		private List<GameObject> m_monitorObject;

		private void OnEnable()
		{
            foreach ( GameObject o in m_monitorObject) { o.SetActive(false); }
        }

		private void OnDisable()
		{
			foreach ( GameObject o in m_monitorObject) { o.SetActive(true); }
        }
	}
}
