using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Takechi.RaycastObjectDetectionSystem;

namespace Takechi.UI.BattleUiMenu.LobbyScene
{
    public class CharacterLobbySceneUserUiMenuController : CharacterBasicUserUiMenuController
    {
        #region serialize field
        [Header("=== RaycastHitSystem ===")]
        [SerializeField] private RaycastHitSetActiveTheCanvasSystem m_raycastHitSetActiveTheCanvasSystem;
        [SerializeField] private RaycastHitInstantiateTheCanvasSystem m_raycastHitInstantiateTheCanvasSystem;

        #endregion
        private RaycastHitSetActiveTheCanvasSystem   raycastHitSetActiveSystem => m_raycastHitSetActiveTheCanvasSystem;
        private RaycastHitInstantiateTheCanvasSystem raycastHitInstantiateSystem => m_raycastHitInstantiateTheCanvasSystem;

        #region unity event

        public override void OnEnable()
        {
            base.OnEnable();

            if (!isMine) return;

            inputStateManagement.InputUserMenu += (statusManagement, addressManagement) =>
            {
                setRaycastHitSystemSetIsOperation(!addressManagement.GetUserUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to add.</color>");
        }

        public override void OnDisable()
        {
            base.OnDisable();

            if (!isMine) return;

            inputStateManagement.InputUserMenu -= (statusManagement, addressManagement) =>
            {
                setRaycastHitSystemSetIsOperation(!addressManagement.GetUserUiMenu().activeSelf);
            };
            Debug.Log($"{PhotonNetwork.LocalPlayer.NickName} : CharacterKeyInputStateManagement.InputUserMenu function <color=green>to remove.</color>");
        }

        #endregion

        #region unity pun callbacks

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            SceneManager.LoadScene(SceneName.titleScnene);
        }

        #endregion

        #region private function
        private void setRaycastHitSystemSetIsOperation(bool state)
        {
            raycastHitSetActiveSystem.SetIsOperation(state);
            raycastHitInstantiateSystem.SetIsOperation(state);
        }

        #endregion

        #region unity evenet system
        public void OnBackTitle()
        {
            raycastHitSetActiveSystem.SetIsOperation(false);
            toFade.OnFadeOut(" Now Loading...");

            StartCoroutine(DelayMethod(NetworkSyncSettings.fadeProductionTime_Seconds, () =>
            {
                PhotonNetwork.Disconnect();
                Debug.Log(" OnDisconnect :<color=green> clear </color>", this.gameObject);
            }));
        }

        #endregion
    }
}