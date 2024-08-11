using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UserData.Model;

public class GroupItem : MonoBehaviour
{
    [SerializeField] private string id;
    
    public string Id { get => id; }

    [SerializeField] private List<Item> items;
    [SerializeField] private Sprite     sprite;
    
    
    public void BindData(LevelItemLog itemLog)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].BindData(!itemLog.PickedDict[i]);
        }
    }


    [Button("Get Item")]
    public void PopulateItem()
    {
        items.Clear();

        id              = id.Trim();
        gameObject.name = id;
        
        Item[] foundItems = GetComponentsInChildren<Item>();
        for (int i = 0; i < foundItems.Length; i++)
        {
            items.Add(foundItems[i]);
            foundItems[i].Init(id, i, sprite);
        }
    }
}
