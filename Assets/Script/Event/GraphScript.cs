using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : SceneScript
{
    public override bool NeedWaitToNext { get; }

    public enum POSITION { FRONT, BACK , }
    public enum EFFECT { POP, FADEIN ,  }

    public StoredManager.IMAGE Image; 
    public POSITION Position;
    public EFFECT Effect;

    public GraphScript(StoredManager.IMAGE image,  POSITION position, EFFECT effect)
    {
        Image = image;
        Position = position;
        Effect = effect;
        NeedWaitToNext = false;
    }
}
