using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildItem : MonoBehaviour
{
    [SerializeField] private string Id;
    [SerializeField] private Sprite Sprite;

#if UNITY_EDITOR
    [ContextMenu("SetChildItemData")]
    void SetChildItemData()
    {
        Item[] items = GetComponentsInChildren<Item>();
        for (int i = 0; i < items.Length; i++)
        {
            Item item = items[i];
            item.BindData(Id, i, Sprite);
        }
    }
#endif
}
