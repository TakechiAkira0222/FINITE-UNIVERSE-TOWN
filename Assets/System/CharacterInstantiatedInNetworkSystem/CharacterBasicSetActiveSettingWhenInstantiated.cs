using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using UnityEngine;

namespace Takechi.NetworkInstantiation.CharacterSetActive
{
    public class CharacterBasicSetActiveSettingWhenInstantiated : MonoBehaviour
    {
        [Header("=== CharacterAddressManagement === ")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        /// <summary>
        /// 自分のみ表示
        /// </summary>
        [SerializeField] private List<GameObject> m_showOnlyMyEnvironment;
        /// <summary>
        ///　自分以外表示
        /// </summary>
        [SerializeField] private List<GameObject> m_showOtherThanYourselfEnvironment;

#if UNITY_EDITOR
        [Header("=== Debug ===")]
        [SerializeField] private KeyCode m_debugKey = KeyCode.M;
#endif


        #region private variable
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;

        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private GameObject handOnlyModel => addressManagement.GetHandOnlyModelObject();

        #endregion

        private void Reset()
        {
            m_characterStatusManagement = this.transform.GetComponent<CharacterStatusManagement>();
        }

        void Start()
        {
            if (myPhotonView.IsMine)
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

                handOnlyModel.SetActive(handOnlyModel.activeSelf == false ? true : false);
            }
#endif
        }
    }
}
