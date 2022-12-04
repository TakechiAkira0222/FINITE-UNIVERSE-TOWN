using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.InputScript.Interface
{
    public class InputScriptInterface : MonoBehaviour
    {
        /// <summary>
        /// このscriptと同時に有効無効を変更したいスクリプトをアタッチしてください。
        /// </summary>
        [SerializeField]
        private List<MonoBehaviour> m_useInputClassName =
            new List<MonoBehaviour>(3);

        void OnEnable()
        {
            setClass(true);
        }

        void OnDisable()
        {
            setClass(false);
        }

        void setClass(bool flag)
        {
            foreach (MonoBehaviour mono in m_useInputClassName)
            {
               if( mono != null) mono.enabled = flag;
            }
        }
    }
}
