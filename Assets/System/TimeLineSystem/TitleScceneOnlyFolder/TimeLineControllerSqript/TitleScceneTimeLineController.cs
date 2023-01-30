using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;

public class TitleScceneTimeLineController : MonoBehaviour
{
    #region SerializeField

    #endregion
    private void Director_Played(PlayableDirector obj)
    {

    }

    private void Director_Stopped(PlayableDirector obj)
    {
        
    }

    private void Reset()
    {
       
    }

    public void OnGamePlayStart()
    {
        SceneManager.LoadScene(SceneName.lobbyScene);
    }
}
