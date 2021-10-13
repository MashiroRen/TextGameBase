using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class BackGroundImage : MonoBehaviour
{
    public SpriteRenderer BackImageFront;
    public SpriteRenderer BackImageBehind;

    public async UniTask ChengeBackGraund(GraphScript ss)
    {
        BackImageFront.DOKill();
        BackImageBehind.DOKill();

        BackImageBehind.sprite  = BackImageFront.sprite;

        _ = await BackImageBehind.DOFade(endValue: 1f, duration: 0f);
        _ = await BackImageFront.DOFade(endValue: 0f, duration: 0f);

        BackImageFront.sprite = GameBaseSystems.GetGameBaseSystem().StoredManager.GetLoadedImage(ss.Image);

        await BackImageFront.DOFade(endValue: 1.0f, duration: 1f);
    } 


}
