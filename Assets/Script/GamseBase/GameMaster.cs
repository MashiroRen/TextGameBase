using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
    protected override bool dontDestroyOnLoad { get { return false; } }

    // Start is called before the first frame update
    public void GameStart()
    {
       
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Event");
    }

    public static void GoTitle()
    {
        SceneManager.LoadScene("Title");
    } 

    public void EndGame()
    {
        Debug.Log("EndGame");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
