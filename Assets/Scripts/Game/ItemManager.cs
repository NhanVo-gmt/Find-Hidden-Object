using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonObject<ItemManager>
{
    [SerializeField] private ItemDatabase itemDatabase;

    public ItemData GetItem(string id)
    {
        if (itemDatabase.itemDict.TryGetValue(id, out ItemData itemData))
        {
            return itemData;
        }

        Debug.LogError($"Can't not find this item with id: {id}");
        return null;
    }
}
