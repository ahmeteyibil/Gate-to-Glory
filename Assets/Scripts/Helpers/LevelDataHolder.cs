using System.Collections.Generic;
using UnityEngine;

public static class LevelDataHolder 
{
    //public static int levelID;
    //public static string levelName;
    //public static string backgroundID;
    //public static List<Wave> waves;
    public static LevelData levelData = new LevelData();
    public static void SetLevelData(LevelData data)
    {
        //levelID = levelData.levelID;
        //levelName = levelData.levelName;
        //backgroundID = levelData.backgroundID;
        //waves = levelData.waves;
        levelData = data;
    }
}
