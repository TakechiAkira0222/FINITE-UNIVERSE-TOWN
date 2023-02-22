using Photon.Pun;
using Photon.Voice;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Address;
using Takechi.CharacterController.Parameters;
using TakechiEngine.PUN;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Takechi.ScriptReference.DamagesThePlayerObject.ReferencingObjectWithContactDetectionThePlayer;

namespace Takechi.UI.BattleCanvasUi
{
    public class CharacterBasicAttackedDirectionDisplayController : TakechiPunCallbacks
    {
        #region serializeField
        [Header("=== CharacterAddressManagement ===")]
        [SerializeField] private CharacterAddressManagement m_characterAddressManagement;
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement  m_characterStatusManagement;
        [Header("=== Script Setting ===")]
        [SerializeField] private Image m_attackedDirectionDisplayImage;
        [SerializeField] private float m_displayTime_seconds = 0.5f; 

        #endregion

        #region private function
        private CharacterAddressManagement addressManagement => m_characterAddressManagement;
        private CharacterStatusManagement  statusManagement => m_characterStatusManagement;
        private PhotonView myPhotonView => addressManagement.GetMyPhotonView();
        private Image attackedDirectionDisplayImage => m_attackedDirectionDisplayImage;
        private float displayTime_seconds => m_displayTime_seconds;
        private bool  m_interfere = true;

        #endregion

        public bool GetInterfere() { return m_interfere; }
        public void SetInterfere(bool value) { m_interfere = value; }

        private void OnCollisionEnter(Collision collision)
        {
            if (!m_interfere) return;
            if (!myPhotonView.IsMine) return;

            foreach (string tag in DamageFromPlayerToPlayer.ColliderTag.allTag)
            {
                if (collision.gameObject.tag == tag)
                {
                    int number =
                        collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                    if (checkTeammember(number)) return;

                    OnAttackedDirectionDisplay(collision);
                }
            }

            foreach (string name in DamageFromPlayerToPlayer.ColliderName.allName)
            {
                if (collision.gameObject.name == name)
                {
                    int number =
                        collision.transform.root.GetComponent<PhotonView>().ControllerActorNr;

                    if (checkTeammember(number)) return;

                    OnAttackedDirectionDisplay(collision);
                }
            }

            foreach (string s in DamageFromObjectToPlayer.objectNameList)
            {
                if (collision.gameObject.name == s)
                {
                    OnAttackedDirectionDisplay(collision);
                }
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (!m_interfere) return;
            if (!myPhotonView.IsMine) return;
            if (!PhotonNetwork.InRoom) return;

            OffAttackedDirectionDisplay();
        }

        /// <summary>
        /// check Teammember
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool checkTeammember(int number)
        {
            return
                statusManagement.GetCustomPropertiesTeamName(number) == statusManagement.GetCustomPropertiesTeamName();
        }

        private void OnAttackedDirectionDisplay(Collision collision)
        {
            Vector3 dir = ConfirmationOfCollisionDirection_vector3(collision);
            
            attackedDirectionDisplayImage.gameObject.SetActive(true);
            attackedDirectionDisplayImage.rectTransform.anchoredPosition = new Vector2( dir.x * 200, dir.z * 200);
        }

        private void OffAttackedDirectionDisplay()
        {
            StartCoroutine( DelayMethod( displayTime_seconds, () => { attackedDirectionDisplayImage.gameObject.SetActive(false); }));
        }
    }
}
