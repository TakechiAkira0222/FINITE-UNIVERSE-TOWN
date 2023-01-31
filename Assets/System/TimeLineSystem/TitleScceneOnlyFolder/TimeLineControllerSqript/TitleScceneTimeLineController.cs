using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;

public class TitleScceneTimeLineController : MonoBehaviour
{
    #region SerializeField

    [SerializeField] PlayableDirector m_playableDirector;

    #endregion

    #region private variable

    PlayableDirector playableDirector => m_playableDirector;

    #endregion
    private void Director_Played(PlayableDirector obj)
    {

    }

    private void Director_Stopped(PlayableDirector obj)
    {
        SceneManager.LoadScene(SceneName.lobbyScene);
    }

    private void Reset()
    {
        m_playableDirector = this.gameObject.GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        playableDirector.paused  += Director_Played;
        playableDirector.stopped += Director_Stopped;
    }

    private void OnDisable()
    {
        playableDirector.paused  -= Director_Played;
        playableDirector.stopped -= Director_Stopped;
    }

    public void OnGamePlayStart()
    {
        playableDirector.Play();
    }
}
