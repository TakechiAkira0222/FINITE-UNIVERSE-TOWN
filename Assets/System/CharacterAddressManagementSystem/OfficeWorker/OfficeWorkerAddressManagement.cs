using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Takechi.CharacterController.SpecificParameters.MechanicalWarreior;
using Takechi.CharacterController.SpecificParameters.OfficeWorker;
using UnityEngine;
using UnityEngine.UIElements;

namespace Takechi.CharacterController.Address
{
    public class OfficeWorkerAddressManagement : CharacterAddressManagement
    {
        [Header("=== OfficeWorkerAddressSetting ===")]
        /// <summary>
        /// handOnlySwordObject GameObject 
        /// </summary>
        [SerializeField] private GameObject   m_handOnlyModelSwordObject;
        /// <summary>
        /// handOnlySwordObject EffectTrail
        /// </summary>
        [SerializeField] private GameObject   m_handOnlySwordEffectTrail;
        /// <summary>
        /// networkModelSwordObject GameObject 
        /// </summary>
        [SerializeField] private GameObject   m_networkModelSwordObject;
        /// <summary>
        /// networkModelSwordObject EffectTrail
        /// </summary>
        [SerializeField] private GameObject   m_networkModelSwordEffectTrail;
        /// <summary>
        /// throwPoint Transfrom
        /// </summary>
        [SerializeField] private Transform    m_throwTransform;
        /// <summary>
        /// throwInstans Path
        /// </summary>
        [SerializeField] private List<string> m_throwInstansFolderName = new List<string>();
        /// <summary>
        /// throwInstans
        /// </summary>
        [SerializeField] private GameObject   m_throwInstans;

        public GameObject GetNetworkModelSwordObject(){ return m_networkModelSwordObject; }
        public GameObject GetNetworkModelSwordEffectTrail(){ return m_networkModelSwordEffectTrail; }
        public GameObject GetHandOnlyModelSwordObject(){ return m_handOnlyModelSwordObject; }
        public GameObject GetHandOnlySwordEffectTrail(){ return m_handOnlySwordEffectTrail; }
        public GameObject GetThrowInstans(){ return m_throwInstans;}
        public Transform  GetThrowTransform(){ return m_throwTransform; }
        public string     GetThrowInstansFolderNamePath()
        {
            string path = "";
            foreach (string s in m_throwInstansFolderName) { path += s + "/"; }
            return path;
        }
    }
}

