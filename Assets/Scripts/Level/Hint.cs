using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hint : MonoBehaviour
{
    [SerializeField] private float scale       = 1.1f;
    [SerializeField] private float cycleLength = 1f;

    private Item           target;
    private Tweener        tweener;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer         = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    [Button]
    public void ChooseRandomTarget()
    {
        Item[] items = GameObject.FindObjectsOfType<Item>(false);
        SetTarget(items[Random.Range(0, items.Length)]);
    }
    
    public void SetTarget(Item target)
    {
        this.target = target;
        
        target.OnClicked += RemoveTarget;
        
        spriteRenderer.enabled = true;

        transform.position = target.transform.position;
        
        tweener = transform.DOScale(scale, cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    void RemoveTarget()
    {
        target.OnClicked -= RemoveTarget;
        
        spriteRenderer.enabled =  false;
        tweener.Kill();
    }
}
