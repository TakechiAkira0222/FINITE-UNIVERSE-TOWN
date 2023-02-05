using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.KeyInputStete;
using Takechi.CharacterController.Parameters;
using UnityEngine;
using UnityEngine.UI;

namespace Takechi.RaycastObjectDetectionSystem
{
    /// <summary>
    /// �ڕW�ɁA�����������������I�������ꍇ�A�L�����o�X�̃C���X�^���X�����܂��B
    /// </summary>
    public class RaycastHitInstantiateTheCanvasSystem : RaycastObjectDetectionSystem
    {
        #region serialize field
        /// <summary>
        /// �ڕW���̖��O
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "TrainingGroundConsole", "desk2" })]
        private List<string> m_tagetObjectNameList = new List<string>();
        /// <summary>
        /// �C���X�^���X��������Canvas
        /// </summary>
        [SerializeField, NamedArrayAttribute(new string[] { "TrainingGroundConsole", "desk2" })]
        private List<GameObject> m_instantiateCanvasList = new List<GameObject>();
        /// <summary>
        /// Focus���ɕ\�������UI
        /// </summary>
        [SerializeField]
        private List<GameObject> m_uiDisplayedOnFocusList;
        /// <summary>
        /// Focus���ɔ�\���ɂȂ�UI
        /// </summary>
        [SerializeField]
        private List<GameObject> m_uiHiddenOnFocusList;
        /// <summary>
        /// �C���X�^���X������e
        /// </summary>
        [SerializeField]
        private Transform  m_parent;

        #endregion

        #region private variable
        /// <summary>
        /// �����ɓ��������I�u�W�F�N�g���Ƃ̃C���X�^���X������canvas��Ԃ�����
        /// </summary>
        private Dictionary<string, GameObject> m_theCanvasSuitableForTheHitObjectDictionary = 
            new Dictionary<string, GameObject>();

       
        #endregion

        #region unity event
        private void Awake()
        {
            SetDictionary();
        }
        private void OnEnable()
        {
            if (m_tagetObjectNameList.Count == 0)
                return;

            if (m_tagetObjectNameList.Count != m_instantiateCanvasList.Count)
            {
                Debug.LogError(" At this rate, the variables inside the Dictionary will collapse.");
                return;
            }

            settingRaycastAction();
        }
        private void OnDisable()
        {
            if (m_tagetObjectNameList.Count == 0)
                return;

            if (m_tagetObjectNameList.Count != m_instantiateCanvasList.Count)
            {
                Debug.LogError(" At this rate, the variables inside the Dictionary will collapse.");
                return;
            }

            removeRaycastAction();
        }
        private void Update()
        {
            if (!isOperation) return;
            if (!isLooking) return;

            foreach (string tagetObjectName in m_tagetObjectNameList)
            {
                if (lookingObject.name == tagetObjectName)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (GameObject.Find(m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName].name + "(Clone)") == null)
                        {
                            audioSource.PlayOneShot(basicUiSound.GetDecisionSoundClip(), basicUiSound.GettDecisionSoundVolume());
                            Debug.Log(" <color=green>RaycastHitSetActiveTheCanvasSystem</color>.<color=yellow>OnDecisionSound_OneShot</color>()");

                            Instantiate(m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName], m_parent);
                            Debug.Log($"<color=yellow>Instantiate</color>({m_theCanvasSuitableForTheHitObjectDictionary[tagetObjectName].name}, {m_parent.name})");
                        }
                    }
                }
            }
        }

        #endregion

        #region private function

        void settingRaycastAction()
        {
            m_raycastHitAction += (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.green;
                    SetActiveUI(true);
                }
            };

            Debug.Log($" m_raycastHitAction(loolingobject) <color=green>to add.</color>");

            m_raycastNotHitAction += (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

            Debug.Log($" m_raycastNotHitAction(loolingobject) <color=green>to add.</color>");

            m_raycastHitObjectChangeAction += (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

            Debug.Log($"  m_raycastHitObjectChangeAction(loolingobject) <color=green>to add.</color>");
        }

        void removeRaycastAction()
        {
            m_raycastHitAction -= (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.green;
                    SetActiveUI(true);
                }
            };

            Debug.Log($" m_raycastHitAction(loolingobject) <color=green>to remove.</color>");

            m_raycastNotHitAction -= (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

            Debug.Log($" m_raycastNotHitAction(loolingobject) <color=green>to remove.</color>");

            m_raycastHitObjectChangeAction -= (lookingObject) =>
            {
                if (lookingObject.GetComponent<Outline>() != null)
                {
                    lookingObject.GetComponent<Outline>().OutlineColor = Color.red;
                    SetActiveUI(false);
                }
            };

            Debug.Log($"  m_raycastHitObjectChangeAction(loolingobject) <color=green>to remove.</color>");
        }

        void SetDictionary()
        {
            for ( int i = 0; i < m_tagetObjectNameList.Count; i++)
            {
                m_theCanvasSuitableForTheHitObjectDictionary.Add( m_tagetObjectNameList[i], m_instantiateCanvasList[i]);
                Debug.Log($"<color=green>Dictionary</color>.<color=yellow>Add</color>({m_tagetObjectNameList[i]},{m_instantiateCanvasList[i].name})");
            }
        }

        void SetActiveUI(bool flag)
        {
            foreach (GameObject o in m_uiDisplayedOnFocusList)
            {
                o.SetActive(flag);
            }

            foreach (GameObject o in m_uiHiddenOnFocusList)
            {
                o.SetActive(!flag);
            }
        }

        #endregion
    }
}