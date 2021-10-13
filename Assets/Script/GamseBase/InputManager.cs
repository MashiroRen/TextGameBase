using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    protected override bool dontDestroyOnLoad { get { return false; } }

    private Subject<string> keySubject = new Subject<string>();
    public string MOUSE_RIGHT { get { return "Fire2"; } }
    public string MOUSE_LEFT { get { return "Fire1"; } }

    public IObservable<string> OnKeyDown
    {
        get { return keySubject; }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnKeyDown.Subscribe(x => Debug.Log(x));
    }

    private void Update()
    {
        if (Input.GetButtonDown(MOUSE_LEFT))
        {
            keySubject.OnNext(MOUSE_LEFT);
        }
        if (Input.GetButtonDown(MOUSE_RIGHT))
        {
            keySubject.OnNext(MOUSE_RIGHT);
        }
    }
}
