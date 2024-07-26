using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HiddenItemListUI : MonoBehaviour
{
    [SerializeField] private HiddenItemUI[] itemUI;
    
    private void Start()
    {
        LevelManager.Instance.OnLoadCurrentLevel += LevelManager_OnLoadCurrentLevel;
        
        Hide();
    }

    void Hide()
    {
        foreach (HiddenItemUI itemUI in itemUI)
        {
            itemUI.gameObject.SetActive(false);    
        }
    }

    private void OnDestroy()
    {
        if (LevelManager.Instance == null) return;
        
        LevelManager.Instance.OnLoadCurrentLevel -= LevelManager_OnLoadCurrentLevel;
    }

    private void LevelManager_OnLoadCurrentLevel(LevelData levelData)
    {
        BindData(levelData);
    }

    void BindData(LevelData levelData)
    {
        for (int i = 0; i < levelData.levelItems.Count; i++)
        {
            itemUI[i].BindData(levelData.levelItems[i]);
            itemUI[i].gameObject.SetActive(true);
        }
    }
}
