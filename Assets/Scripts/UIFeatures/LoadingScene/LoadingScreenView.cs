using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameFoundation.Scripts.Utilities.Extension;
using GameFoundationBridge;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private RectTransform leftGameObject;
    [SerializeField] private RectTransform rightGameObject;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Lerp")]
    [SerializeField] private float easeInTime = 1f;
    [SerializeField] private float wordFadeTime = 0.5f;

    private Vector3 startLeftPos = new Vector3(-600, 0, 0);
    private Vector3 startRightPos = new Vector3(600, 0, 0);

    private bool isAnimate = false;

    public Action OnFinishShow;

    private void Awake()
    {
        canvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_OnSceneLoaded;
    }

    private void SceneManager_OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Hide();
    }

    public async UniTask Show()
    {
        isAnimate = true;
        
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() =>
        {
            leftGameObject.DOAnchorPos(Vector3.one, easeInTime).SetEase(Ease.InOutSine);
            rightGameObject.DOAnchorPos(Vector3.one, easeInTime).SetEase(Ease.InOutSine);
        });
        sequence.AppendInterval(easeInTime);
        sequence.Append(canvasGroup.DOFade(1, wordFadeTime));
        sequence.AppendInterval(0.2f);

        sequence.OnComplete(() =>
        {
            isAnimate = false;
        });

        await sequence.AsyncWaitForCompletion();
    }

    async void Hide()
    {
        await UniTask.WaitUntil(() => !isAnimate);
        canvasGroup.DOFade(0, wordFadeTime).OnComplete(() =>
        {
            leftGameObject.DOAnchorPos(startLeftPos, easeInTime).SetEase(Ease.InOutSine);
            rightGameObject.DOAnchorPos(startRightPos, easeInTime).SetEase(Ease.InOutSine);
        });
        
        
    }
}
