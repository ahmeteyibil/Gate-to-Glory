using System.Collections.Generic;
using System;

[Serializable]
public class Chapter 
{
    public int chapterIndex;
    public int totalDistance;
    public string enemyID;
    public List<GateSet> gateSets;
    public int chestCount;
    public int difficultyScale;
}
