using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using Takechi.CharacterController.SpecificParameters.Slime;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.Parameters
{
    public class SlimeStatusManagement : CharacterStatusManagement
    {
        [Header("=== MechanicalWarreiorStatus Setting===")]
        [SerializeField] private SlimeSpecificParameters m_slimeSpecificParameters;

        #region private variable

        private float m_attackLaserEffectDsuration_Seconds;
        private float m_amountOfSpeedChange; 
        private int m_aiSlimeDsuration_Seconds;
        private int m_trnadoEffectDsuration_Seconds;
        private int m_transparencyDsuration_Seconds;
        private int m_stanDsuration_Seconds;

        #endregion

        #region unity evetn
        protected override void Awake()
        {
            base.Awake();

            SetupSlimeSpecificParameters();

            if (!base.thisPhotonView.IsMine) return;
        }

        #endregion

        #region set up function
        /// <summary>
        /// SpecificStatus を、データベースの変数で設定します。
        /// </summary>
        private void SetupSlimeSpecificParameters()
        {
            SetAttackLaserEffectDsuration_Seconds(m_slimeSpecificParameters.GetAttackLaserEffectDsuration_seconds());
            SetAmountOfSpeedChange(m_slimeSpecificParameters.GetAmountOfSpeedChange());
            SetAiSlimeDsuration_Seconds(m_slimeSpecificParameters.GetAiSlimeDsuration_seconds());
            SetTrnadoEffectDsuration_Seconds( m_slimeSpecificParameters.GetTrnadoEffectDsuration_seconds());
            SetTransparencyDsuration_Seconds( m_slimeSpecificParameters.GetTransparencyDsuration_seconds());
            SetStanDsuration_Seconds( m_slimeSpecificParameters.GetStanDsuration_seconds());

            Debug.Log($"<color=green> setupSlimeSpecificParameters </color>\n" +
                      $"<color=blue> info</color>\n" +
                      $" m_attackLaserEffectDsuration_Seconds : { m_attackLaserEffectDsuration_Seconds} \n"+
                      $" m_amountOfSpeedChange : { m_amountOfSpeedChange} \n"+
                      $" m_aiSlimeDsuration_Seconds : {m_aiSlimeDsuration_Seconds} \n"+
                      $" m_trnadoEffectDsuration_Seconds : { m_trnadoEffectDsuration_Seconds} \n"+
                      $" m_transparencyDsuration_Seconds : { m_transparencyDsuration_Seconds} \n"+
                      $" m_stanDsuration_Seconds : { m_stanDsuration_Seconds} \n"
                      );
        }

        #endregion

        #region set function
        public void SetAttackLaserEffectDsuration_Seconds(float value) { m_attackLaserEffectDsuration_Seconds = value; }
        public void SetAmountOfSpeedChange(float value) { m_amountOfSpeedChange = value; }
        public void SetAiSlimeDsuration_Seconds(int value) { m_aiSlimeDsuration_Seconds = value; }
        public void SetTrnadoEffectDsuration_Seconds(int value) { m_trnadoEffectDsuration_Seconds = value; }
        public void SetTransparencyDsuration_Seconds(int value) { m_transparencyDsuration_Seconds = value; }
        public void SetStanDsuration_Seconds(int value) { m_stanDsuration_Seconds = value; }

        #endregion

        #region get function

        public float GetAttackLaserEffectDsuration_Seconds() => m_attackLaserEffectDsuration_Seconds;
        public float GetAmountOfSpeedChange() => m_amountOfSpeedChange;
        public int GetAiSlimeDsuration_Seconds() => m_aiSlimeDsuration_Seconds;
        public int GetTrnadoEffectDsuration_Seconds() => m_trnadoEffectDsuration_Seconds; 
        public int GetTransparencyDsuration_Swconds() => m_transparencyDsuration_Seconds; 
        public int GetStanDsuration_Seconds() => m_stanDsuration_Seconds; 

        #endregion

    }
}
