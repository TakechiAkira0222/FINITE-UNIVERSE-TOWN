using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TitleScceneTimeLineController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private TimelineAsset[]  m_timelines = new TimelineAsset[2];
    [SerializeField] private PlayableDirector m_playableDirector;

    #endregion

    private void Reset()
    {
        m_playableDirector = 
            this.transform.GetComponent<PlayableDirector>();
    }

    private void Awake()
    {
        m_playableDirector.played += Director_Played;
        m_playableDirector.Play( m_timelines[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnGamePlayStart()
    {
        m_playableDirector.stopped += Director_Stopped;
        m_playableDirector.Play( m_timelines[1]);
    }

    private void Director_Played(PlayableDirector obj)
    {

    }

    private void Director_Stopped(PlayableDirector obj)
    {
        SceneManager.LoadScene(1);
    }
}
