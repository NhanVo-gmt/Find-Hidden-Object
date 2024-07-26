using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    private LevelManager levelManager;
    
    private void Awake()
    {
        levelManager = (LevelManager)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Load Level"))
        {
            levelManager.LoadCurrentLevel();
        }
    }
}
