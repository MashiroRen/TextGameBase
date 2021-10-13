using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StoredManager;

public class ActorScript: SceneScript
{
    public enum POUSE { DEFAULT, SMILE2, SMILE3, NULL }
    public enum ACTION { POP, OUT, ACT}

    public ACTOR actor { get; } = 0;
    public ACTION action { get; } = 0;
    public POUSE pouse { get; } = 0;
    public Vector3 position { get; } = new Vector3(0, 0, 0);

    public override bool NeedWaitToNext { get; } = false;

    public ActorScript(ACTOR a, ACTION ac ,Vector3 pos, POUSE p)
    {
        actor = a;
        action = ac;
        position = pos;
        pouse = p;
        NeedWaitToNext = false;
    }
    public ActorScript(ACTOR a, ACTION ac , POUSE p)
    {
        actor = a;
        action = ac;
        pouse = p;
        NeedWaitToNext = false;
    }
    public ActorScript(ACTOR a, ACTION ac)
    {
        actor = a;
        action = ac;
        NeedWaitToNext = false;
    }

}
