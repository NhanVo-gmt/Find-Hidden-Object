using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : SingletonObject<SceneLoader>
{
    public Action OnSceneFinishLoading;
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
    }
    
    private void SceneManager_OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        OnSceneFinishLoading?.Invoke();
    }

    public static void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}

public enum Scene
{
    LevelSelectScene,
    GameScene
}
