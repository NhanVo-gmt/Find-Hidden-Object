using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Data/Level")]
public class LevelData : ScriptableObject
{
    public int             Level;
    public List<LevelItem> levelItems;
}

[Serializable]
public class LevelItem
{
    public string Id;
    public int    Number;
}
