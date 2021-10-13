using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UniRx.Triggers;
using System;

public class TouchPanel : MonoBehaviour
{
    public ObservableEventTrigger EventTrigger { get { return eventTrigger; } }
    private ObservableEventTrigger eventTrigger { get; set; }

    private void Awake()
    {
        eventTrigger = gameObject.AddComponent<ObservableEventTrigger>();
        eventTrigger.OnPointerClickAsObservable().
        Subscribe(_ => Debug.Log(this.gameObject.name + "clicked"));
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }


}
