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
    
    
    public void BindData(List<LevelItemLog> itemLogs)
    {
        for (int i = 0; i < itemLogs.Count; i++)
        {
            var foundItem = items.Find(x => x.Index == i);
            if (!foundItem)
            {
                Debug.LogError($"Can not find item with index: {i} in Group Item Id: {id}");
                return;
            }
            
            foundItem.BindData(!itemLogs[i].HasPicked);
        }
    }


    [Button("Get Item")]
    public void PopulateItem()
    {
        items.Clear();
        
        Item[] foundItems = GetComponentsInChildren<Item>();
        for (int i = 0; i < foundItems.Length; i++)
        {
            items.Add(foundItems[i]);
            foundItems[i].Index = i;
        }
    }
}
