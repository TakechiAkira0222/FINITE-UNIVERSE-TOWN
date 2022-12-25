using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.UI.CanvasMune.CursorMnagement
{
    public class CursorMnagement : MonoBehaviour
    {
        private int selectionIndex = 0;

        protected void setSelectionIndex(int index) 
        { 
            selectionIndex = index;
            Debug.Log($" On Select(int{index}) selectionIndex : <color=blue>{ selectionIndex}</color> <color=green>to set.</color>");
        }

        protected int GetSelectionIndex() { return selectionIndex; }

        #region IEnumerator

        protected IEnumerator uiCursorRotation( GameObject uiCursor)
        {
            while (uiCursor.activeSelf)
            {
                uiCursor.transform.localEulerAngles += new Vector3(0, 0.3f, 0);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }

        protected IEnumerator slideCursor(GameObject uiCursor, List<Transform> slidePosition, float slideSpeed = 10)
        {
            while ( Vector3.Distance( uiCursor.transform.localPosition, setSeeOnlyX(uiCursor, slidePosition[selectionIndex])) > 0.003f)
            {
                Vector3 xOnlyPosition = 
                    setSeeOnlyX(uiCursor, slidePosition[selectionIndex]);

                uiCursor.transform.localPosition =
                      Vector3.Lerp( uiCursor.transform.localPosition, xOnlyPosition, Time.deltaTime * slideSpeed);

                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        #endregion

        private Vector3 setSeeOnlyX(GameObject uiCursor, Transform slidePosition)
        {
            return new Vector3( slidePosition.localPosition.x, uiCursor.transform.localPosition.y, uiCursor.transform.localPosition.z);
        }
    }
}
