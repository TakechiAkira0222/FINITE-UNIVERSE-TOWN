using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;
using UnityEngine;

namespace Takechi.CharacterController.Parameters
{
    public class MechanicalWarriorStatusManagement : CharacterStatusManagement
    {
        [Header("=== MechanicalWarreiorStatus Setting===")]
        [SerializeField] private MechanicalWarreiorSpecificParameters m_mechanicalWarreiorSpecificParameters;

        private float m_shootingForce = 3.0f;
        private float m_durationOfBullet = 5;

        private List<string> m_bulletsFolderName = new List<string>();

        protected override void Awake()
        {
            base.Awake();

            if (!base.thisPhotonView.IsMine) return;

            SetupMechanicalWarreiorSpecificParameters();
        }

      
        #region set up function

        /// <summary>
        /// SpecificStatus を、データベースの変数で設定します。
        /// </summary>
        private void SetupMechanicalWarreiorSpecificParameters()
        {
            SetShootingForce( m_mechanicalWarreiorSpecificParameters.GetShootingForce());
            SetDurationOfBullet( m_mechanicalWarreiorSpecificParameters.GetDurationOfBullet());

            Debug.Log($"<color=green> setupMechanicalWarriorSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" NickName : { PhotonNetwork.LocalPlayer.NickName} \n" +
                      $" m_shootingForce = {m_shootingForce}\n" +
                      $" m_durationOfBullet    = {m_durationOfBullet}\n"
                      );
        }

        #endregion

        #region SetFunction

        public void SetShootingForce(float changeValue)
        {
            Debug.Log($" shootingForce {m_shootingForce} = {changeValue}");
            m_shootingForce = changeValue;
        }

        public void SetDurationOfBullet(float changeValue)
        {
            Debug.Log($" durationOfBullet {m_durationOfBullet} = {changeValue}");
            m_durationOfBullet = changeValue;
        }

        #endregion

        #region GetStatusFunction

        public float  GetShootingForce() { return m_mechanicalWarreiorSpecificParameters.GetShootingForce(); }
        public float  GetDurationOfBullet() { return m_mechanicalWarreiorSpecificParameters.GetDurationOfBullet(); }
        public string GetBulletsPath()
        {
            string path = "";
            foreach (string s in m_mechanicalWarreiorSpecificParameters.GetBulletsPath()) { path += s + "/"; }

            return path;
        }

        #endregion
    }
}

