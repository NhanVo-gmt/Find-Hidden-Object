using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    public string Id;
    public int    Index;

    public Action OnClicked;

    public void Init(string Id, int Index, Sprite sprite)
    {
        this.Id    = Id;
        this.Index = Index;

        gameObject.name = $"{Id} {Index}";
        
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        if (TryGetComponent<Collider2D>(out Collider2D col))
        {
            DestroyImmediate(col);
        }

        col           = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    public void BindData(bool active)
    {
        gameObject.SetActive(active);
    }

    public void Click()
    {
        gameObject.SetActive(false);
        
        OnClicked?.Invoke();
    }
}
