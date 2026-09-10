using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    CharacterStats baseStats;
    public CharacterStats RuntimeStats { get; set; }

    public void SetStats(CharacterStats sourceStats)
    {
        baseStats = sourceStats;
        RuntimeStats = new CharacterStats();
        RuntimeStats.maxHealth = sourceStats.maxHealth;
        RuntimeStats.damage = sourceStats.damage;
        RuntimeStats.armor = sourceStats.armor;
        RuntimeStats.lifesteal = sourceStats.lifesteal;
        RuntimeStats.critChance = sourceStats.critChance;
        RuntimeStats.critMultiplier = sourceStats.critMultiplier;
    }
}
