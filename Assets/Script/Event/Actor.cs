using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour
{

    private SpriteRenderer BodyRenderer;
    public Sprite BodyNormal;

    public GameObject FaceObject;
    private SpriteRenderer FaceRenderer;
    public Sprite FaceNormal;
    public Sprite FaceSmile;
    
    private string actorName;


    public void Awake()
    {
        BodyRenderer = this.GetComponent<SpriteRenderer>();
        FaceRenderer = FaceObject.GetComponent<SpriteRenderer>();
        //For invisible, this object can be accessed from StoreManager  
        this.gameObject.SetActive(false);
    }

    public void PlayScript(ActorScript ss)
    {
        if (ss.action == ActorScript.ACTION.POP)
        {
            StandOnStage(ss.position);
        }
        else if (ss.action == ActorScript.ACTION.OUT)
        {
            GetoutFromStage();
        }
        else if (ss.action == ActorScript.ACTION.ACT)
        {
            Act(ss);
        }
    }
    private void StandOnStage(Vector3 postion)
    {
        this.gameObject.SetActive(true);
        Debug.Log("StandOnStage");
        this.gameObject.transform.position = postion;
    }

    private void GetoutFromStage()
    {    
        Debug.Log("OutOnStage");
        this.gameObject.SetActive(false);
    }

    private void Act(ActorScript ss)
    {
        ActorScript.POUSE pouse = ss.pouse;

        switch (pouse)
        {
            case ActorScript.POUSE.DEFAULT:
                BodyRenderer.sprite = BodyNormal;
                FaceRenderer.sprite = FaceNormal;
                break;

            case ActorScript.POUSE.SMILE2:
                FaceRenderer.sprite = FaceSmile;
                break;

            case ActorScript.POUSE.NULL:
                FaceRenderer.sprite = FaceNormal;
                break;

            default:
                ActPersonal(ss);
                break;
        }
    }
    private void ActPersonal(ActorScript ss)
    {
        //TODO:The function for espetial action of each actors. 
    }

}
