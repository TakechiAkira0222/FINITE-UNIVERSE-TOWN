using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TakechiEngine.PUN.ServerConnect.Joined;

using static Takechi.ScriptReference.CustomPropertyKey.CustomPropertyKeyReference;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;
using UnityEngine.SceneManagement;

namespace Takechi.UI.ResultMune
{
    public class ResultMuneManagement : TakechiJoinedPunCallbacks
    {
        public void OnLeaveRoom() 
        {
            LeaveRoom(); 
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            SceneManager.LoadScene(SceneName.lobbyScene);
        }
    }
}
