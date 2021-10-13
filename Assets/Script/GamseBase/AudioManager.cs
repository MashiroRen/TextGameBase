using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// One of GameBaseSystem. It can be get GameBaseSystems and exist only this one.
/// AudioManager has two audio source for BGM, and all audio source for SE which make as apropriate. 
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    protected override bool dontDestroyOnLoad { get { return false; }}

    //
    [SerializeField]
    public AudioSource Source0;
    [SerializeField]
    public AudioSource Source1;

    /// Param for BGM  
    public float DefaultVolume;
    public float FadingTime;

    private AudioSource NowPlayingSource
    {
        get
        {
            return Source1.isPlaying ? Source1 :
                Source0;
        }
    }

    public void ChengeBGM(AudioScript ss, AudioClip audioClip)
    {

        AudioScript.PLAY action = ss.Action;

        if (action == AudioScript.PLAY.START)
        {
            Debug.Log("StartBGM");
            NowPlayingSource.clip = audioClip;
            NowPlayingSource.loop = true;
            NowPlayingSource.Play();
        }
        else if(action == AudioScript.PLAY.END)
        {
            Debug.Log("StopBGM");
            NowPlayingSource.Stop();
        }
        else if (action == AudioScript.PLAY.FADEIN)
        {
            CrossFade(audioClip, FadingTime);
        }
    }

    public void CrossFade(AudioClip audioClip, float fadingTime)
    {
        var fadeInSource =
            Source0.isPlaying ?
            Source1 :
            Source0;

        var fadeOutSource =
            Source0.isPlaying ?
            Source0 :
            Source1;

        fadeInSource.clip = audioClip;
        fadeInSource.Play();
        fadeInSource.DOKill();
        fadeInSource.DOFade(DefaultVolume, fadingTime);

        fadeOutSource.DOKill();
        fadeOutSource.DOFade(0, fadingTime);
    }
}
