using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class LoadingCanvas : SingletonMonoBehaviour<LoadingCanvas>
{
    public Image LoadingImage;

    protected override bool dontDestroyOnLoad { get { return false; } }

    public async UniTask StartLoading()
    {
        gameObject.SetActive(true);
        LoadingImage.DOKill();
        await LoadingImage.DOFade( endValue: 1.0f, duration: 0f);
    }

    public void EndLoading()
    {
        LoadingImage.DOKill();
        LoadingImage.DOFade(endValue: 0.0f, duration: 1f)
            .OnComplete(() => this.gameObject.SetActive(false));
    }

    public async UniTask EndScene()
    {
        this.gameObject.SetActive(true);
        LoadingImage.DOKill();
        
        await LoadingImage.DOFade(endValue: 1.0f, duration: 1f);
    }
}

