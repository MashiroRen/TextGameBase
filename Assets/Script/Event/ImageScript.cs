using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScript : SceneScript
{
    public enum BOARD { BACK };
    public enum IMAGE { HOME_SUN, 
                        HOME_RAIN };
    public enum EFFECT { POP, 
                         FADE };

    public override bool NeedWaitToNext { get; }
    public BOARD Board { get; }
    public IMAGE Image { get; }
    public EFFECT Effect { get; }


    public ImageScript(BOARD newBoard, IMAGE newImage, EFFECT useEffect)
    {
        Board = newBoard;
        Image = newImage;
        Effect = useEffect;
        NeedWaitToNext = false;
    }
}
