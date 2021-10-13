using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using EnumNameAttribute;
using Cysharp.Threading.Tasks;

public class StoredManager : MonoBehaviour
{
    public bool NowLoading { get { return nowLoading; } }
    private bool nowLoading { get; set; }

    /// <summary>
    /// StringValue is ResourcePath and enum is NumberInProg
    /// When you add New Resource, the actor prefab/image/sound apply to this list. 
    /// these enum key can be used in  scene scripts.
    /// </summary>
    public enum ACTOR { [StringValue("Actor_Mano")]  MANO, 
                        [StringValue("Actor_Mano")]  MANO_2, 
                        [StringValue("Actor_Mano")]  MANO_3,
                        };
    public enum IMAGE { [StringValue("CG_01")] OFFICE_SUN,
                        [StringValue("CG_02")] OFFICE_RAIN,
                        };
    public enum SOUND { [StringValue("BGM_01")] START,
                        [StringValue("BGM_02")] COLD,
                        };

    //Loaded GameObject List
    public Actor[] StanbyedActorList =new Actor[System.Enum.GetNames(typeof(ACTOR)).Length];
    //not public for bolocking overwrite
    private Sprite[] StanbyCGList = new Sprite[System.Enum.GetNames(typeof(IMAGE)).Length];
    private AudioClip[] StanbySoundList = new AudioClip[System.Enum.GetNames(typeof(SOUND)).Length];

    //ForLoadingReserve
    public AudioClip Sound_Dummy;
    public Actor Actor_Dummy;
    public Sprite CG_Dummy;

    private List<UniTask> loadTasks;

    public async UniTask<bool> LoadSceneResource(List<SceneScript> scripts)
    {
        nowLoading = true;
        loadTasks = new List<UniTask>();
        Debug.Log("LoadScene");
        int i = 0;
        foreach (SceneScript s in scripts)
        {
            i++;
            Debug.Log("Script" + i + ";start");

            //Branch to each ScriptType
            if (s.GetType() == typeof(TextScript))
            {
                Debug.Log("GetTextScript");

            }
            else if (s.GetType() == typeof(AudioScript))
            {
                Debug.Log("GetAudioScript");
                loadTasks.Add(LoadSound((AudioScript)s));
            }
            else if (s.GetType() == typeof(ActorScript))
            {

                Debug.Log("GetActorScript");
                loadTasks.Add(LoadActor((ActorScript)s));

            }
            else if (s.GetType() == typeof(GraphScript))
            {
                Debug.Log("GetGraphScript");
                loadTasks.Add(LoadCG((GraphScript)s));
            }


            Debug.Log("Script" + i + ";finish");
        }
        Debug.Log("EndScene");
        
        //test for bigdata
        //await UniTask.Delay(1000);
        await UniTask.WhenAll(loadTasks);
        
        nowLoading = false;
        return nowLoading;
    }

    public void RestoreLoadObject()
    {
        RestoreActor();
    }

    private async UniTask LoadActor(ActorScript ss)
    {
        Debug.Log("GetHandler_LoadActor");
        if (Object.Equals(StanbyedActorList[(int)(ss.actor)], null))
        {
            StanbyedActorList[(int)(ss.actor)] = Actor_Dummy;
            var result = await Addressables.LoadAssetAsync<GameObject>(ss.actor.GetStringValue());

            Debug.Log("LoadCompleated");
            //ロードしたオブジェクトをリストに登録
            StanbyedActorList[(int)(ss.actor)] = Instantiate(result, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Actor>();
        }
        else
        {
            Debug.Log("AlreadyLoaded"+ ss.actor.ToString());
        }
    }

    private async UniTask LoadSound(AudioScript ss)
    {
        Debug.Log("GetHandler_LoadSound");
        if (Object.Equals(StanbySoundList[(int)(ss.SourceName)], null))
        {
            StanbySoundList[(int)(ss.SourceName)] = Sound_Dummy;
            var result = await Addressables.LoadAssetAsync<AudioClip>(ss.SourceName.GetStringValue());
            Debug.Log("LoadCompleated " + result.ToString());
            //ロードしたオブジェクトをリストに登録
            StanbySoundList[(int)(ss.SourceName)] = result;
        }
        else
        {
            Debug.Log("AlreadyLoaded" + ss.SourceName.ToString());
        }
    }
    private async UniTask LoadCG(GraphScript ss)
    {
        Debug.Log("GetHandler_LoadCG");
        if (Object.Equals(StanbyCGList[(int)(ss.Image)], null))
        {
            StanbyCGList[(int)(ss.Image)] = CG_Dummy;
            var result = await Addressables.LoadAssetAsync<Sprite>(ss.Image.GetStringValue());
            Debug.Log("LoadCompleated " + result.ToString());
            //ロードしたオブジェクトをリストに登録
            StanbyCGList[(int)(ss.Image)] = result;
        }
        else
        {
            Debug.Log("AlreadyLoaded" + ss.Image.ToString());
        }
    }

    private void RestoreActor()
    {
        int i = 0;
        foreach (Actor a in StanbyedActorList)
        { 
            if (!Object.Equals(a, null))
            {
                Destroy(StanbyedActorList[i].gameObject);
                StanbyedActorList[i] = null;
            }
            i++;
        }
    }

    public void  GetGameObject()
    {

    }

    public AudioClip GetLoadedAudioClip(StoredManager.SOUND _s)
    {
        return StanbySoundList[((int)_s)];
    }
    public Sprite GetLoadedImage(StoredManager.IMAGE i)
    {
        return StanbyCGList[((int)i)];
    }
}
