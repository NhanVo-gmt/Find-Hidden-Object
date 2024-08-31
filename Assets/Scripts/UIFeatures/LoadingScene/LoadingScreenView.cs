using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private RectTransform leftGameObject;
    [SerializeField] private RectTransform rightGameObject;
    [SerializeField] private CanvasGroup[] canvasGroups;

    private Vector3 startLeftPos = new Vector3(-600, 0, 0);
    private Vector3 startRightPos = new Vector3(600, 0, 0);
    
    private void Start()
    {
        Show();
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

    void Show()
    {
        leftGameObject.DOAnchorPos(Vector3.one, 1f).SetEase(Ease.InOutSine);
        
        rightGameObject.DOAnchorPos(Vector3.one, 1f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            foreach (CanvasGroup canvasGroup in canvasGroups)
            {
                canvasGroup.DOFade(1, 0.5f);
            }
        });
    }

    void Hide()
    {
        foreach (CanvasGroup canvasGroup in canvasGroups)
        {
            canvasGroup.DOFade(1, 0.5f);
        }
        
        leftGameObject.DOAnchorPos(startLeftPos, 1f).SetEase(Ease.InOutSine).SetDelay(0.5f);
        
        rightGameObject.DOAnchorPos(startRightPos, 1f).SetEase(Ease.InOutSine).SetDelay(0.5f);
    }
}
