using System.Collections;
using System.Collections.Generic;
using Takechi.CharacterController.Jump;
using Takechi.CharacterController.Movement;
using Takechi.CharacterController.Parameters;
using Takechi.CharacterController.ViewpointOperation;
using UnityEngine;
using UnityEngine.Playables;

namespace Takechi.CharacterController.DeathblowAnimationEvent
{
    public class OfficeWorkerNetworkRendererDeathblowAnimationEventManager : MonoBehaviour
    {
        #region SerializeField
        [Header("=== CharacterStatusManagement ===")]
        [SerializeField] private CharacterStatusManagement m_characterStatusManagement;

        [Header("=== ScriptSetting ===")]
        [SerializeField] private PlayableDirector 　　　　　　　　m_playableDirector;
        [SerializeField] private CharacterBasicMovement 　　　　　m_basicMovement;
        [SerializeField] private CharacterBasicJump 　　　　　　　m_basicJump;
        [SerializeField] private CharacterBasicViewpointOperation m_basicViewpoint;

        #endregion

        #region UnityAnimatorEvent

        private void Awake()
        {
            m_playableDirector.played += Director_Played;
            m_playableDirector.stopped += Director_Stopped;
        }

        /// <summary>
        /// 必殺技animationの開始
        /// </summary>
        void OfficeWorkerDeathblowStart()
        {
            m_playableDirector.Play();
        }

        /// <summary>
        /// 必殺技animationの終了
        /// </summary>
        void OfficeWorkerDeathblowEnd()
        {
          
        }

        #endregion

        private void Director_Stopped(PlayableDirector obj)
        {
            m_basicJump.enabled = true;
            m_basicMovement.enabled = true;
            m_basicViewpoint.enabled = true;
        }

        private void Director_Played(PlayableDirector obj)
        {
            m_basicJump.enabled = false;
            m_basicMovement.enabled = false;
            m_basicViewpoint.enabled = false;
        }
    }
}