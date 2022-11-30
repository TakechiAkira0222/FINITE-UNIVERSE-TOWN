using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.Mune
{
	public class CanvasController : MonoBehaviour
	{
		[Header(" setting ")]

		[SerializeField ,Tooltip("ï`âÊèÛë‘ÇïœçXÇµÇΩÇ¢GameObject")] private List<GameObject> TheObjectWhoseDrawingStateYouWantToChange;

		private void Start()
		{
			foreach (GameObject _o in TheObjectWhoseDrawingStateYouWantToChange)
			{
				_o.SetActive(false);
			}
		}

		private void OnDestroy()
		{
			foreach (GameObject _o in TheObjectWhoseDrawingStateYouWantToChange)
			{
				_o.SetActive(true);
			}
		}
	}
}
