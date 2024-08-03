using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string Id;
    [SerializeField] private int    Index;

    public void BindData(string Id, int Index, Sprite sprite)
    {
        this.Id                               = Id;
        this.Index                            = Index;
        GetComponent<SpriteRenderer>().sprite = sprite;

        DestroyImmediate(GetComponent<BoxCollider2D>());
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }
}
