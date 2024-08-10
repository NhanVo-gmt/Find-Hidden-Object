using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public int Index;

    public Action OnClicked;

    public void Init(int Index, Sprite sprite)
    {
        this.Index = Index;
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        Collider2D col = GetComponent<Collider2D>();
        col.enabled = false;
        col.enabled = true;
    }

    public void BindData(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Click()
    {
        OnClicked?.Invoke();
        
        gameObject.SetActive(false);
    }
}
