using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : SceneScript
{
    public string actorName { get; }
    public string text { get; }
    public override bool NeedWaitToNext { get; }

    public TextScript(string newActorName, string newText)
    {
        actorName = newActorName;
        text = newText;
        NeedWaitToNext = true;
    }

    public TextScript(string newActorName, string newText, bool needWait)
    {
        actorName = newActorName;
        text = newText;
        NeedWaitToNext = needWait;
    }

    public TextScript(StoredManager.ACTOR actor, string newText)
    {
        Actor actorObject = GameBaseSystems.GetGameBaseSystem().StoredManager.StanbyedActorList[((int)actor)];

        actorName = actorObject.name;
        text = newText;
        NeedWaitToNext = true;
    }

    public TextScript(StoredManager.ACTOR actor, string newText, bool needWait)
    {
        Actor actorObject = GameBaseSystems.GetGameBaseSystem().StoredManager.StanbyedActorList[((int)actor)];

        actorName = actorObject.name;
        text = newText;
        NeedWaitToNext = needWait;
    }

}
