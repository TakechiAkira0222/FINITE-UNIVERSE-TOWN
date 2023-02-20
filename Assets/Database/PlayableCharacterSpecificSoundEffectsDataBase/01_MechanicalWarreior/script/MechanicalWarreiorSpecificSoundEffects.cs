using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Takechi.CharacterController.SpecificSoundEffects.MechanicalWarreior
{
    [Serializable]
    [CreateAssetMenu(fileName = "MechanicalWarreiorSpecificSoundEffects", menuName = "MechanicalWarreiorData/MechanicalWarreiorSpecificSoundEffects")]
    public class MechanicalWarreiorSpecificSoundEffects : ScriptableObject
    {
        #region SerializeField
        [SerializeField, Header("通常射撃")] private AudioClip m_normalShoot;
        [SerializeField, Range(0f, 1f)] private float m_normalShootVolume = 0.5f;

        [SerializeField, Header("必殺技　コッキング")] private AudioClip m_deathbolwCocking;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwCockingVolume = 0.5f;

        [SerializeField, Header("必殺技　リロード")] private AudioClip m_deathbolwReload;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwReloadVolume = 0.5f;

        [SerializeField, Header("必殺技　ショット")] private AudioClip m_deathbolwShoot;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwShootVolume = 0.5f;

        [SerializeField, Header(" EnemySearchClickButton ")] private AudioClip m_enemySearchClickButton;
        [SerializeField, Range(0f, 1f)] private float m_enemySearchClickButtonVolume = 0.5f;

        [SerializeField, Header(" EnemyOnSearch ")] private AudioClip m_enemyOnSearch;
        [SerializeField, Range(0f, 1f)] private float m_enemyOnSearchVolume = 0.5f;

        [SerializeField, Header(" SmokeExplosion")] private AudioClip m_smokeExplosion;
        [SerializeField, Range(0f, 1f)] private float m_smokeExplosionVolume = 0.5f;


        #endregion

        #region GetAudioClipFunction
        public AudioClip GetNormalShootClip() => m_normalShoot;
        public AudioClip GetDeathbolwShootClip()   => m_deathbolwShoot;
        public AudioClip GetDeathbolwCockingClip() => m_deathbolwCocking;
        public AudioClip GetDeathbolwReloadClip()  => m_deathbolwReload;
        public AudioClip GetEnemySearchClickButtonClip() => m_enemySearchClickButton;  
        public AudioClip GetEnemyOnSearchClip() => m_enemyOnSearch;
        public AudioClip GetSmokeExplosionClip()    => m_smokeExplosion;

        public float GetNormalShootVolume()    => m_normalShootVolume;
        public float GetDeathbolwShootVolume() => m_deathbolwShootVolume;
        public float GetDeathbolwCockingVolume() => m_deathbolwCockingVolume;
        public float GetDeathbolwReloadVolume()  => m_deathbolwReloadVolume;
        public float GetEnemySearchClickButtonVolume() => m_enemySearchClickButtonVolume;
        public float GetEnemyOnSearchVolume()  => m_enemyOnSearchVolume;
        public float GetSmokeExplosionVolume() => m_smokeExplosionVolume;


        #endregion
    }
}
