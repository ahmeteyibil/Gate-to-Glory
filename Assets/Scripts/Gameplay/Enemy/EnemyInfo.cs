using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    CharacterStats enemyStats;
    public void SetEnemyStats(CharacterStats stats)
    {
        enemyStats = stats;
    }
    public CharacterStats GetEnemyStats()
    {
        return enemyStats;
    }
}
