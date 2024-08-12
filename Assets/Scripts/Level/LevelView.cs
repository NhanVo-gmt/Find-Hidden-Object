using System;
using System.Collections;
using System.Collections.Generic;
using Blueprints;
using GameFoundation.Scripts.Utilities.Extension;
using Sirenix.OdinInspector;
using UnityEngine;
using UserData.Controller;
using UserData.Model;
using Zenject;

public class LevelView : MonoBehaviour
{
    [SerializeField] private List<GroupItem> groupItems;
    
    #region Inject
    
    [Inject] private LevelManager levelManager;

    #endregion
    
    private void Start()
    {
        this.GetCurrentContainer().Inject(this);

        LevelLog currentLevel = levelManager.GetCurrentLevelLog();
        foreach (GroupItem groupItem in groupItems)
        {
            if (!currentLevel.LevelItemLogs.TryGetValue(groupItem.Id, out LevelItemLog levelItemLog))
            {
                Debug.LogError($"Can't find {groupItem.Id} in Level Item Log");
                return;
            }
            
            groupItem.BindData(levelItemLog);
        }
    }
    
    [Button("Get Group")]
    public void PopulateItem()
    {
        groupItems.Clear();
        
        GroupItem[] foundGroup = GetComponentsInChildren<GroupItem>();
        for (int i = 0; i < foundGroup.Length; i++)
        {
            groupItems.Add(foundGroup[i]);
            foundGroup[i].PopulateItem();
        }
    }
}
