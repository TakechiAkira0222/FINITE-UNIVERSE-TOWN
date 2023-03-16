using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.UI.GameLogTextScrollView
{
    public class GameLogTextInterface : MonoBehaviour
    {
        #region private variable
        [SerializeField] private List<string> textContentList = new List<string>(4);

        #endregion
        public List<string> GetTextContentList()  => textContentList;
        public void OnAddList(string s) => textContentList.Add(s); 
        public void OnRemoveList(string s) => textContentList.Remove(s);
        public void OnRemoveAt(int index = 0) => textContentList.RemoveAt(index);
        public void OnClearList() => textContentList.Clear();
    }
}
