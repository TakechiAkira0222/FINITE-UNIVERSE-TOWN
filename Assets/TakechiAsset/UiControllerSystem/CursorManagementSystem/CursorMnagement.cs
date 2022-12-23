using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.CanvasMune.CursorMnagement
{
    public class CursorMnagement : MonoBehaviour
    {
        #region IEnumerator

        protected IEnumerator uiCursorRotation(GameObject uiCursor)
        {
            while (uiCursor.activeSelf)
            {
                uiCursor.transform.localEulerAngles += new Vector3(0, 0.3f, 0);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        protected IEnumerator slideCursor( GameObject uiCursor, List<Transform> slidePosition, int index, float slideSpeed = 10)
        {
            Vector3 xOnlyPosition = new Vector3(slidePosition[index].localPosition.x, uiCursor.transform.localPosition.y, uiCursor.transform.localPosition.z);
            float dis = Vector3.Distance( uiCursor.transform.localPosition, xOnlyPosition);

            while (dis > 0)
            {
                Debug.Log(dis);
                uiCursor.transform.localPosition =
                Vector3.Lerp( uiCursor.transform.localPosition, xOnlyPosition, Time.deltaTime * slideSpeed);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        #endregion
    }
}
