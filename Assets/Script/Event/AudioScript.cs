using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : SceneScript
{
    public override bool NeedWaitToNext { get; }

    public enum SOUND_TYPE { BGM, SE }
    public enum PLAY { START, END, FADEIN }
    public enum SOURCE_NAME { COMMMO, RAIN }
    public enum TIME {  };

    public SOUND_TYPE SoundType { get; } = 0;
    public PLAY Action { get; } = 0;
    public StoredManager.SOUND SourceName { get; } = 0;

    public AudioScript(StoredManager.SOUND newSourceName, SOUND_TYPE newSoundType, PLAY newAction) 
    {
        SoundType = newSoundType;
        Action = newAction;
        SourceName = newSourceName;
        NeedWaitToNext = false;
    }
    public AudioScript(SOUND_TYPE newSoundType, PLAY newAction)
    {
        SoundType = newSoundType;
        Action = newAction;
        SourceName = 0;
        NeedWaitToNext = false;
    }


}
