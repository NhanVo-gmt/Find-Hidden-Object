using System.Collections;
using System.Collections.Generic;
using Blueprints;
using UnityEngine;

public class ItemManager
{
    #region Inject

    private readonly ItemBlueprint itemBlueprint;

    #endregion

    public ItemManager(ItemBlueprint itemBlueprint)
    {
        this.itemBlueprint = itemBlueprint;
    }

    public ItemRecord GetItemRecord(string Id)
    {
        if (!itemBlueprint.TryGetValue(Id, out ItemRecord itemRecord))
        {
            Debug.LogError($"There is no {Id} in Item Blueprint");
            return null;
        }

        return itemRecord;
    }
}
