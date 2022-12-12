using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.NetworkInstantiation.Character
{
    public class CharacterBasicSettingWhenInstantiated : MonoBehaviour
    {
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]

        /// <summary>
        /// 自分のみ表示
        /// </summary>
        [SerializeField] private List<GameObject> m_showOnlyMyEnvironment;

        /// <summary>
        ///　自分以外表示
        /// </summary>
        [SerializeField] private List<GameObject> m_showOtherThanYourselfEnvironment;

        [Header("=== Debug ===")]
        [SerializeField] private KeyCode m_debugKey;

        private void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
        }

        void Start()
        {
            if (m_characterStatusManagement.PhotonView.IsMine)
            {
                foreach (GameObject obj in m_showOnlyMyEnvironment)
                {
                    obj.SetActive(true);
                }

                foreach (GameObject obj in m_showOtherThanYourselfEnvironment)
                {
                    obj.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject obj in m_showOnlyMyEnvironment)
                {
                    obj.SetActive(false);
                }

                foreach (GameObject obj in m_showOtherThanYourselfEnvironment)
                {
                    obj.SetActive(true);
                }
            }
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(m_debugKey))
            {
                foreach (GameObject obj in m_showOtherThanYourselfEnvironment)
                {
                   obj.SetActive(obj.activeSelf == false ? true : false);
                }
            }
#endif

        }
    }
}
