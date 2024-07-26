using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializedDictionary("Id", "Item")]
    public SerializedDictionary<string, ItemData> itemDict;

    private void OnValidate()
    {
        foreach (var item in itemDict)
        {
            item.Value.Id = item.Key;
        }
    }
}

[Serializable]
public class ItemData
{
    public string Id;
    public string Name;
    public Sprite sprite;
    [TextArea(5,10)] public string Description;
}
