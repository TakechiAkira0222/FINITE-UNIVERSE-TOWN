using UnityEngine;
using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using Takechi.CharacterController.Parameters;
using System.IO;
using System;

namespace Takechi.CharacterController.AttackAnimationEvent
{
    public class MechanicalWarriorHandOnlyAttackAnimationEventManager : MonoBehaviour
    {
        #region SerializeField

        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PhotonView m_thisPhotonView;
        [SerializeField] private List<string> m_folderName = new List<string>();

        [SerializeField] private float m_force = 10;
        [SerializeField] private float m_destroyTime = 5;
        [SerializeField] private GameObject m_instans;
        [SerializeField] private Transform  m_magazineTransfrom;

        private string m_path = "";

        #endregion

        #region private
        private PhotonView thisPhotonView => m_thisPhotonView;
        private CharacterStatusManagement characterStatusManagement => m_characterStatusManagement;

        #endregion

        private void Awake()
        {
            foreach (string s in m_folderName) { m_path += s + "/"; }
        }

        private void Reset()
        {
            m_thisPhotonView = this.transform.GetComponent<PhotonView>();
        }

        #region UnityAnimatorEvent

        /// <summary>
        /// FirstShot Animation Start
        /// </summary>
        void MechanicalWarriorFirstShot()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            Shooting(m_magazineTransfrom, m_force);
        }

        /// <summary>
        /// SecondShot Animation End
        /// </summary>
        void MechanicalWarriorSecondShot()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;

            Shooting(m_magazineTransfrom, m_force);
        }

        /// <summary>
        /// ThirdShot Animation End
        /// </summary>
        void MechanicalWarriorThirdShot()
        {
            if (!characterStatusManagement.GetMyPhotonView().IsMine) return;
            Shooting(m_magazineTransfrom, m_force);
        }

        #endregion

        #region set Function

        #endregion

        #region Recursive function

        private void Shooting( Transform magazine, float force)
        {
            GameObject instans =
            PhotonNetwork.Instantiate( m_path + m_instans.name, magazine.position, Quaternion.identity);

            instans.GetComponent<Rigidbody>().AddForce( magazine.forward * 100 * force);

            StartCoroutine(DelayMethod( m_destroyTime, () =>
            {
                PhotonNetwork.Destroy(instans);
            }));
        }

        private IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        #endregion
    }
}