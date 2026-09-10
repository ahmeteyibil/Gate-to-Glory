using UnityEngine;

public class LevelStatisticsManager : MonoBehaviour
{
    public static LevelStatisticsManager Instance;
    public bool LevelWon { get; set; }
    public int CurrentCoin { get; set; } = 0;
    public float TotalDamage { get; set; } = 0;
    public int KillCount { get; set; } = 0;
    public int WavesCompleted { get; set; } = 0;
    private void Awake()
    {
        Instance = this;
    }
}
