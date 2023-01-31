using System;
using System.Collections;
using System.Collections.Generic;
using Takechi.PlayableCharacter.FadingCanvas;
using TakechiEngine.PUN;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using static Takechi.ScriptReference.NetworkEnvironment.ReferencingNetworkEnvironmentDetails;
using static Takechi.ScriptReference.SceneInformation.ReferenceSceneInformation;

public class TitleSceneTimeLineController : TakechiPunCallbacks
{
    #region SerializeField

    [SerializeField] private PlayableDirector m_playableDirector;
    [SerializeField] private PlayableAsset m_gameStartTimeLine;
    [SerializeField] private PlayableAsset m_titleStartTimeLine;
    [SerializeField] private ToFade m_toFade;

    #endregion

    #region private variable

    private PlayableDirector playableDirector => m_playableDirector;
    private PlayableAsset    gameStartTimeLine => m_gameStartTimeLine;
    private PlayableAsset    titelStartTimeLine => m_titleStartTimeLine;
    private Action gameStartAction = delegate { };
    private ToFade toFade => m_toFade;

    #endregion

    private void Reset()
    {
        m_playableDirector = this.gameObject.GetComponent<PlayableDirector>();
    }

    private void Awake()
    {
        toFade.OnFadeIn("");

        playableDirector.playableAsset = titelStartTimeLine;
        playableDirector.Play();

        playableDirector.paused  += titelStartTimeLine_Played;
        playableDirector.stopped += titelStartTimeLine_Stopped;
    }

    public void OnGamePlayStart()
    {
        gameStartAction();
    }

    private void titelStartTimeLine_Played(PlayableDirector obj)
    {

    }

    private void titelStartTimeLine_Stopped(PlayableDirector obj)
    {
        playableDirector.playableAsset = gameStartTimeLine;

        playableDirector.paused  -= titelStartTimeLine_Played;
        playableDirector.stopped -= titelStartTimeLine_Stopped;

        playableDirector.paused  += gameStartTimeLine_Played;
        playableDirector.stopped += gameStartTimeLine_Stopped;

        gameStartAction += () => { playableDirector.Play(); };
    }

    private void gameStartTimeLine_Played(PlayableDirector obj)
    {

    }

    private void gameStartTimeLine_Stopped(PlayableDirector obj)
    {
        toFade.OnFadeOut("NowLoading...");

        StartCoroutine(DelayMethod(NetworkSyncSettings.fadeProductionTime_Seconds, () =>
        {
            SceneManager.LoadScene(SceneName.lobbyScene);
        }));
    }
}
