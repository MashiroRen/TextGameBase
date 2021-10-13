using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using UnityEngine.AddressableAssets;

public class EventDirector : SingletonMonoBehaviour<EventDirector>
{
    //Atcheed thisObject
    public Text MainTextWindow;
    public Text CharaNameWindow;
    public Image TouchPanel;
    public GameObject BackImage;

    //AttachedOtherGameObject
    private AudioManager audioManager;
    private StoredManager storedManager;
    private TouchPanel touchPanelControler;
    private LoadingCanvas loadingImage;
    private BackGroundImage backGroundImage;

    //Using this Objecrt
    private List<SceneScript> book;
    private bool playWait;

    //ForTest
    public AudioClip testClip;

    protected override bool dontDestroyOnLoad { get { return false; } }

    // Start is called before the first frame update
    // This is Main Stream of Event Scene.
    void Start()
    {
        //get component from attach
        touchPanelControler = TouchPanel.GetComponent<TouchPanel>();
        backGroundImage = BackImage.GetComponent<BackGroundImage>();

        //gameBaseSystemLaod
        var _system = GameBaseSystems.GetGameBaseSystem();
        audioManager = _system.AudioManager;
        storedManager = _system.StoredManager;
        loadingImage = _system.LoadingCanvas;

        //initiallize
        MainTextWindow.text = "";
        CharaNameWindow.text = "";

        //prepare start scene
        book = Script1.testScene();
        var _task = RunScene(book);
        Debug.Log("EndDirectorStart");
    }

    public async UniTask RunScene(List<SceneScript> book)
    {
        var task = loadingImage.StartLoading();
        _ = await storedManager.LoadSceneResource(book);
        await UniTask.WhenAll(task);
        loadingImage.EndLoading();
        await PlayScene(book);
        await loadingImage.EndScene();
        GameMaster.GoTitle();
        loadingImage.EndLoading();
    }

    private async UniTask PlayScene(List<SceneScript> scripts)
    { 

        Debug.Log("RunScene");
        int i = 0;
        playWait = false;
        foreach (SceneScript s in scripts)
        {
            i++;
            Debug.Log("Script" + i + ";start");

            if (s.GetType() == typeof(TextScript))
            {
                Debug.Log("GetTextScript");
                PlayTextScript((TextScript)s);

            }
            else if (s.GetType() == typeof(AudioScript))
            {
                Debug.Log("GetAudioScript");
                PlayAudioScript((AudioScript)s);
            }
            else if (s.GetType() == typeof(ActorScript))
            {
                Debug.Log("GetActorScript");
                PlayActorScript((ActorScript)s);

            }
            else if(s.GetType() == typeof(GraphScript))
            {
                Debug.Log("GetCGScript");
                PlayGraphScript((GraphScript)s);
            }

            Debug.Log("Script" + i + ";finish");

            
            await UniTask.WaitWhile(() => playWait);
        }
        Debug.Log("EndScene");
        storedManager.RestoreLoadObject();

    }

    private void PlayTextScript(TextScript ss)
    {
        Debug.Log("PlayTextScript");

        CharaNameWindow.text = ss.actorName;
        MainTextWindow.text = ss.text;

        if (ss.NeedWaitToNext)
        {
            WaitScene();
        }
        Debug.Log("FinishTextScript");
    }

    private void PlayAudioScript(AudioScript ss)
    {
        audioManager.ChengeBGM(ss, storedManager.GetLoadedAudioClip(ss.SourceName));
    }

    private void PlayGraphScript(GraphScript ss)
    {
        _ = backGroundImage.ChengeBackGraund(ss);
    }

    private void PlayActorScript(ActorScript ss)
    {
        storedManager.StanbyedActorList[((int)ss.actor)].PlayScript(ss);
    }

    public void WaitScene()
    {
        Debug.Log("waitClick");

        //wait for any Action
        playWait = true;

        //release actions subscribe
        var disposable = new SingleAssignmentDisposable();
        disposable.Disposable = touchPanelControler.EventTrigger.OnPointerClickAsObservable()
            .Subscribe(_ => ReleaseWaitScene(disposable));
    }

    private void ReleaseWaitScene(IDisposable disposable)
    {
        disposable.Dispose();
        Debug.Log("ReleaseWait");
        playWait = false;
    }

}
