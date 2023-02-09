using System.Collections;
using System.Collections.Generic;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using System;

namespace Takechi.UI.BattleUiMenu.BattleScene
{
    public class CharacterBattleSceneUserUiMenuController : CharacterBasicUserUiMenuController
    {
        private bool isLeaveRoom = false;

        #region unity event system
        public void OnLeaveRoom()
        {
            toFade.OnFadeOut(" Leave Room...");

            isLeaveRoom = true;

            StartCoroutine(DelayMethod(NetworkSyncSettings.fadeProductionTime_Seconds, () =>
            {
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.LeaveRoom();
                    Debug.Log(" OnLeaveRoom :<color=green> clear </color>", this.gameObject);
                }
            }));
        }

        private void OnDestroy()
        {
            if (!isMine) return;
            if (!isLeaveRoom) return;

            SceneManager.LoadScene(SceneName.lobbyScene);
        }

        #endregion
    }
}
