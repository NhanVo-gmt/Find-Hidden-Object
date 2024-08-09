using System;
using System.Collections;
using System.Collections.Generic;
using Blueprints;
using GameFoundation.Scripts.Utilities.Extension;
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
            if (!currentLevel.levelItemLogs.TryGetValue(groupItem.Id, out List<LevelItemLog> levelItemLogs))
            {
                Debug.LogError($"Can't find {groupItem.Id} in Level Item Log");
                return;
            }
            
            groupItem.BindData(levelItemLogs);
        }
    }
}
