using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonObject<LevelManager>
{
    [SerializeField] private LevelData currentLevel;

    public Action<LevelData> OnLoadCurrentLevel;
    
    private void Start()
    {
        SceneLoader.Instance.OnSceneFinishLoading += SceneLoader_OnSceneFinishLoading;
    }
    
    // Select level
    // Save load
    // Load scene
    // Load level to scene
    private void SceneLoader_OnSceneFinishLoading()
    {
        LoadCurrentLevel();
    }
    
    public void LoadCurrentLevel()
    {
        OnLoadCurrentLevel?.Invoke(currentLevel); // todo remove
    }
}
