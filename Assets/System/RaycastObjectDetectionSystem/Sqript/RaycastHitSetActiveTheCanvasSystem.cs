using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Takechi.RaycastObjectDetectionSystem
{
    /// <summary>
    /// �ڕW�ɁA�����������������I�������ꍇ�A�L�����o�X��Active�̏�Ԃ�ύX���܂��B
    /// </summary>
    public class RaycastHitSetActiveTheCanvasSystem : RaycastObjectDetectionSystem
    {
        /// <summary>
        /// �ڕW���̖��O
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<string> m_tagetObjectNameList = new List<string>();

        /// <summary>
        /// Active�̏�Ԃ�ύX������Canvas
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "desk1", "projector" })]
        private List<GameObject> m_setActiveCanvasList = new List<GameObject>();

        /// <summary>
        /// �I��O�ɕ\�������UI
        /// </summary>
        [SerializeField]
        private GameObject m_displayedUiImage;

        /// <summary>
        /// �����ɓ��������I�u�W�F�N�g���Ƃ�Active�̏�Ԃ�ύX����canvas��Ԃ�����
        /// </summary>
        private Dictionary<string, GameObject> m_theCanvasSuitableForTheHitObjectDictionary =
            new Dictionary<string, GameObject>();

        void Awake()
        {
            if (m_tagetObjectNameList.Count == 0)
                return;

            if (m_tagetObjectNameList.Count != m_setActiveCanvasList.Count)
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
                    SetActiveUI(true);
                }
            };

            m_raycastNotHitAction += (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

            m_raycastHitObjectChangeAction += (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

        }

        void Update()
        {
            if (!IsLooking)
                return;

            foreach (string tagetObjectName in m_tagetObjectNameList)
            {
                if ( LookingObject.name == tagetObjectName)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName].SetActive(true);
                        Debug.Log($"{m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName].name}.<color=yellow>SetActive</color>(<color=blue>true</color>)");
                    }
                }
            }
        }

        void SetDictionary()
        {
            for (int i = 0; i < m_tagetObjectNameList.Count; i++)
            {
                m_theCanvasSuitableForTheHitObjectDictionary.Add(m_tagetObjectNameList[i], m_setActiveCanvasList[i]);
                Debug.Log($"<color=green>Dictionary</color>.<color=yellow>Add</color>({m_tagetObjectNameList[i]},{m_setActiveCanvasList[i].name})");
            }
        }

        void SetActiveUI(bool flag)
        {
            m_displayedUiImage.SetActive(flag);
        }
    }
}