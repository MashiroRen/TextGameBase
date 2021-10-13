using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class StartButton : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.OnClickAsObservable()
            .Subscribe(_ => GameBaseSystems.GetGameBaseSystem().GameMaster.NextScene())
            .AddTo(gameObject);
    }

}
