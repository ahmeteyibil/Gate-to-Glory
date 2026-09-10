using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] CharacterStats playerStats;
    private void Start()
    {
        //HUDUIManager.Instance.SetHUDValues(playerStats);
    }
    public void IncreaseDamage(int increaseValue)
    {
        playerStats.damage.value += increaseValue;
        HUDUIManager.Instance.SetDamageText(playerStats.damage.value);
    }
    public void IncreaseArmor(int increaseValue)
    {
        playerStats.armor.value += increaseValue;
        HUDUIManager.Instance.SetArmorText(playerStats.armor.value);
    }

    public void IncreaseLifesteal(float increaseValue)
    {
        playerStats.lifesteal.value += increaseValue;
        HUDUIManager.Instance.SetLifestealText(playerStats.lifesteal.value);
    }
    public void IncreaseCritChance(int increaseValue)
    {
        playerStats.critChance.value += increaseValue;
        HUDUIManager.Instance.SetCritChanceText(playerStats.critChance.value);
    }
    public void SetStats(CharacterStats stats)
    {
        playerStats = stats;
    }
    public CharacterStats GetStats()
    {
        return playerStats;
    }
}
