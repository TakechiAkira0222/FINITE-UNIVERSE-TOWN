using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.Mune
{
	public class CanvasController : MonoBehaviour
	{
		[Header("Setting ")]

		[SerializeField , Tooltip("ï`âÊèÛë‘ÇïœçXÇµÇΩÇ¢GameObject")] 
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
