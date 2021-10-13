using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioScript;
using static ActorScript;
using static GraphScript;
using static StoredManager;

public class Script1
{

    private static string mano = "真乃";
    private static Vector3 pos1 = new Vector3(-5, -3);
    private static Vector3 pos2 = new Vector3(0, -3);
    private static Vector3 pos3 = new Vector3(5, -3);

    public static List<SceneScript> testScene()
    {
        List<SceneScript> scene = new List<SceneScript>
        {
            //シーン開始
            new AudioScript(SOUND.START, SOUND_TYPE.BGM, PLAY.FADEIN),
            new GraphScript(IMAGE.OFFICE_SUN, POSITION.BACK, EFFECT.POP),

            //真乃1登場
            new ActorScript(ACTOR.MANO, ACTION.POP, pos1, POUSE.DEFAULT),
            new TextScript(mano, "はじめまして、真乃です"),
            new TextScript(mano, "こちらはシステムのチュートリアルになります"),
            new TextScript(mano, "現在実装されている機能は以下の通りです"),

            //選択肢
            new TextScript(mano, "文字の表示、背景の変更、BGMの変更、キャラクターの登場、退場"),

            new TextScript(mano, "では背景の変更を紹介しますね"),
            new TextScript(mano, "説明は2号にやってもらいましょう。\r\n2号～～～～～～～～～～"),

            //真乃2登場
            new ActorScript(ACTOR.MANO_2, ACTION.POP, pos2, POUSE.DEFAULT),
            new TextScript(mano + "2", "はじめまして、真乃２号です"),
            new TextScript(mano + "2", "背景について。背景は登録された背景入れ替えることができます"),
            new TextScript(mano + "2", "むん！！"),
            
            //背景変更
            new GraphScript(IMAGE.OFFICE_RAIN, POSITION.BACK, EFFECT.FADEIN),
            new TextScript(mano + "2", "はい、赤になりました。"),
            new TextScript(mano, "ありがとうございます2号"),
            new TextScript(mano, "お疲れ様でした～～～"),

            new ActorScript(ACTOR.MANO_2, ACTION.OUT),

            //真乃3登場
            new TextScript(mano, "では次は音楽を変えましょう"),
            new TextScript(mano, "3号にやってもらいましょう。\r\n3号～～～～～～～～～～"),
            new ActorScript(ACTOR.MANO_3, ACTION.POP, pos3, POUSE.DEFAULT),
            new TextScript(mano + "3", "はじめまして、真乃３です"),
            new TextScript(mano + "3", "むん！"),

            new AudioScript(SOUND.COLD, SOUND_TYPE.BGM, PLAY.FADEIN),

            new TextScript(mano + "s", "\"\"\"現在の機能はここまでです\"\"\""),

            new ActorScript(ACTOR.MANO, ACTION.ACT, POUSE.SMILE2),
            new ActorScript(ACTOR.MANO_2, ACTION.ACT, POUSE.SMILE2),
            new ActorScript(ACTOR.MANO_3, ACTION.ACT, POUSE.SMILE2),

            new TextScript(mano + "s", "\"\"\"むん！！！！！！\"\"\""),

            //シーン終了
            new ActorScript(ACTOR.MANO, ACTION.OUT),
            new AudioScript(SOUND_TYPE.BGM, PLAY.END)
        };

        return scene;
    }
}
