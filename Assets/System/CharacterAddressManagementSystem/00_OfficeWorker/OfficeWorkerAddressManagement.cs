using JetBrains.Annotations;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
        [SerializeField] private GameObject    m_throwInstans;
        /// <summary>
        /// deathblowAuraEffect4 effect
        /// </summary
        [SerializeField] private GameObject    m_deathblowAuraEffect4;
        /// <summary>
        /// deathblow Timeline
        /// </summary>
        [SerializeField] private PlayableAsset m_deathblowTimeline;
        /// <summary>
        /// praying Timeline
        /// </summary>
        [SerializeField] private PlayableAsset m_prayingTimeline;

        public PlayableAsset GetDeathblowTimeline() { return m_deathblowTimeline; }
        public PlayableAsset GetPrayingTimeline() { return m_prayingTimeline; }
        public GameObject GetNetworkModelSwordObject(){ return m_networkModelSwordObject; }
        public GameObject GetNetworkModelSwordEffectTrail(){ return m_networkModelSwordEffectTrail; }
        public GameObject GetHandOnlyModelSwordObject(){ return m_handOnlyModelSwordObject; }
        public GameObject GetHandOnlySwordEffectTrail(){ return m_handOnlySwordEffectTrail; }
        public GameObject GetDeathblowAuraEffect4() { return m_deathblowAuraEffect4; }
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

