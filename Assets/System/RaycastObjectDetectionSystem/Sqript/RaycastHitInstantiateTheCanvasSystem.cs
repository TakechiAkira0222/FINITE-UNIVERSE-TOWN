using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.RaycastObjectDetectionSystem
{
    /// <summary>
    /// 目標に、光線が当たった時選択した場合、キャンバスのインスタンス化します。
    /// </summary>
    public class RaycastHitInstantiateTheCanvasSystem : RaycastObjectDetectionSystem
    {
        /// <summary>
        /// インスタンス化する親
        /// </summary>
        [SerializeField]
        private Transform  m_parent;

        /// <summary>
        /// 目標物の名前
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<string> m_tagetObjectNameList = new List<string>();

        /// <summary>
        /// インスタンス化したいCanvas
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<GameObject> m_instantiateCanvasList = new List<GameObject>();

        /// <summary>
        /// 選択前に表示されるUI
        /// </summary>
        [SerializeField]
        private GameObject m_displayedUiImage;

        /// <summary>
        /// 光線に当たったオブジェクトごとのインスタンス化するcanvasを返す辞書
        /// </summary>
        private Dictionary<string, GameObject> m_theCanvasSuitableForTheHitObjectDictionary = 
            new Dictionary<string, GameObject>();

        void Awake()
        {
            if ( m_tagetObjectNameList.Count != m_instantiateCanvasList.Count)
            {
                Debug.LogError(" At this rate, the variables inside the Dictionary will collapse.");
                return;
            }

            SetDictionary();

            m_raycastHitAction += (lookingObject) => 
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.green;
                }
            };

            m_raycastNotHitAction += (lookingObject) => 
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                }
            };

        }

        void Update()
        { 
            if (!IsLooking)
            {
                SetActiveUI(false);
                return;
            }

            foreach ( string tagetObjectName in m_tagetObjectNameList)
            {
                if ( LookingObject.name == tagetObjectName)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (GameObject.Find( m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName].name +"(Clone)") == null)
                        {
                            Instantiate( m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName], m_parent);
                        }

                        SetActiveUI(false);
                    }
                    else
                    {
                        SetActiveUI(true);
                    }
                }
            }
        }

        void SetDictionary()
        {
            for ( int i = 0; i < m_tagetObjectNameList.Count; i++)
            {
                m_theCanvasSuitableForTheHitObjectDictionary.Add( m_tagetObjectNameList[i], m_instantiateCanvasList[i]);
            }
        }

        void SetActiveUI(bool flag)
        {
            m_displayedUiImage.SetActive(flag);
        }
    }
}