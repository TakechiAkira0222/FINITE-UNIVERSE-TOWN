using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.RaycastObjectDetectionSystem
{
    /// <summary>
    /// �ڕW�ɁA�����������������I�������ꍇ�A�L�����o�X�̃C���X�^���X�����܂��B
    /// </summary>
    public class RaycastHitInstantiateTheCanvasSystem : RaycastObjectDetectionSystem
    {
        /// <summary>
        /// �C���X�^���X������e
        /// </summary>
        [SerializeField]
        private Transform  m_parent;

        /// <summary>
        /// �ڕW���̖��O
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<string> m_tagetObjectNameList = new List<string>();

        /// <summary>
        /// �C���X�^���X��������Canvas
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<GameObject> m_instantiateCanvasList = new List<GameObject>();

        /// <summary>
        /// �I��O�ɕ\�������UI
        /// </summary>
        [SerializeField]
        private GameObject m_displayedUiImage;

        /// <summary>
        /// �����ɓ��������I�u�W�F�N�g���Ƃ̃C���X�^���X������canvas��Ԃ�����
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