using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.Mune
{
	public class CanvasController : MonoBehaviour
	{
		[Header("Setting ")]

		[SerializeField , Tooltip("描画状態を変更したいGameObject")] 
		private List<GameObject> TheObjectWhoseDrawingStateYouWantToChange;

		private void Start()
		{
			foreach (GameObject o in TheObjectWhoseDrawingStateYouWantToChange)
			{
				o.SetActive(false);
			}
		}

		private void OnDestroy()
		{
			foreach (GameObject o in TheObjectWhoseDrawingStateYouWantToChange)
			{
				o.SetActive(true);
			}
		}
	}
}
