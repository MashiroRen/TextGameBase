using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;


/// <summary>
/// Game system start-up here.
/// This class is hub for GameBaseSystems.
/// </summary>
public class GameBaseSystems : SingletonMonoBehaviour<GameBaseSystems>
{
    protected override bool dontDestroyOnLoad { get { return true; } }

    /// <summary>
    /// Keep game progress and split each game scene. 
    /// </summary>
    public GameMaster GameMaster { get { return gameMaster; } }
    /// <summary>
    /// Loading resorce and manage resourece status.
    /// </summary>
    public StoredManager StoredManager { get { return storedManager; } }
    /// <summary>
    /// Start and stop sound. 
    /// </summary>
    public AudioManager AudioManager { get { return audioManager; } }
    /// <summary>
    /// keyborad and controller input catch this instance.
    /// </summary>
    public InputManager InputManager { get { return inputManager; } }
    /// <summary>
    /// In loading time, this board will be activate.
    /// </summary>
    public LoadingCanvas LoadingCanvas { get { return loadingCanvas; } }

    //private for upper variables
    private GameMaster gameMaster;
    private StoredManager storedManager;
    private AudioManager audioManager;
    private InputManager inputManager;
    private LoadingCanvas loadingCanvas;

    private void Start()
    {
        Screen.SetResolution(960, 540, false, 60);
        //Check GameBaseSystem
        loadingCanvas = GetComponentInChildren<LoadingCanvas>();

        var _task =　loadingCanvas.StartLoading();

        gameMaster = GetComponentInChildren<GameMaster>();
        storedManager = GetComponentInChildren<StoredManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        inputManager = GetComponentInChildren<InputManager>();

        UniTask.WhenAll(_task);
        loadingCanvas.EndLoading();

        Debug.Log("GameSystem Setuped");
        //Start Game
        gameMaster.GameStart();
    }

    public static GameBaseSystems GetGameBaseSystem()
    {
        return Instance;
    }

}
