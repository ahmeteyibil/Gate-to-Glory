using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public LevelData LevelData { get; set; }
    int currentWaveIndex = -1;
    int currentEnemyIndex = -1;
    GameObject enemyObject;
    GameObject playerObject;
    
    private void Awake()
    {
        Instance = this;
        LevelData = LevelDataHolder.levelData;
    }

}
