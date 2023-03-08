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
        [SerializeField, Header("通常射撃")] private AudioClip m_normalShootClip;
        [SerializeField, Range(0f, 1f)] private float m_normalShootVolume = 0.5f;

        [SerializeField, Header("必殺技　コッキング")] private AudioClip m_deathbolwCockingClip;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwCockingVolume = 0.5f;

        [SerializeField, Header("必殺技　リロード")] private AudioClip m_deathbolwReloadClip;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwReloadVolume = 0.5f;

        [SerializeField, Header("必殺技　ショット")] private AudioClip m_deathbolwShootClip;
        [SerializeField, Range(0f, 1f)] private float m_deathbolwShootVolume = 0.5f;

        [SerializeField, Header(" EnemySearchClickButton ")] private AudioClip m_enemySearchClickButtonClip;
        [SerializeField, Range(0f, 1f)] private float m_enemySearchClickButtonVolume = 0.5f;

        [SerializeField, Header(" EnemyOnSearch ")] private AudioClip m_enemyOnSearchClip;
        [SerializeField, Range(0f, 1f)] private float m_enemyOnSearchVolume = 0.5f;

        [SerializeField, Header(" SmokeExplosion")] private AudioClip m_smokeExplosionClip;
        [SerializeField, Range(0f, 1f)] private float m_smokeExplosionVolume = 0.5f;

        [SerializeField, Header(" wallSetting")] private AudioClip m_wallSettingSoundClip;
        [SerializeField, Range(0f, 1f)] private float m_wallSettingSoundVolume = 0.5f;

        #endregion

        #region GetAudioClipFunction
        public AudioClip GetNormalShootClip() => m_normalShootClip;
        public AudioClip GetDeathbolwShootClip()   => m_deathbolwShootClip;
        public AudioClip GetDeathbolwCockingClip() => m_deathbolwCockingClip;
        public AudioClip GetDeathbolwReloadClip()  => m_deathbolwReloadClip;
        public AudioClip GetEnemySearchClickButtonClip() => m_enemySearchClickButtonClip;  
        public AudioClip GetEnemyOnSearchClip() => m_enemyOnSearchClip;
        public AudioClip GetSmokeExplosionClip()   => m_smokeExplosionClip;
        public AudioClip GetWallSettingSoundClip() => m_wallSettingSoundClip;  

        public float GetNormalShootVolume()    => m_normalShootVolume;
        public float GetDeathbolwShootVolume() => m_deathbolwShootVolume;
        public float GetDeathbolwCockingVolume() => m_deathbolwCockingVolume;
        public float GetDeathbolwReloadVolume()  => m_deathbolwReloadVolume;
        public float GetEnemySearchClickButtonVolume() => m_enemySearchClickButtonVolume;
        public float GetEnemyOnSearchVolume()  => m_enemyOnSearchVolume;
        public float GetSmokeExplosionVolume() => m_smokeExplosionVolume;
        public float GetWallSettingSoundVolume() => m_wallSettingSoundVolume;

        #endregion
    }
}
