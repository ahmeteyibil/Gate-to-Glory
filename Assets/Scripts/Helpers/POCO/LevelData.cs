using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public int levelID;
    public string levelName;
    public string backgroundID;
    public List<Wave> waves;
}
