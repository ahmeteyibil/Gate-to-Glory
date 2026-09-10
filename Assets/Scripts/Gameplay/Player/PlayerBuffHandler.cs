using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffHandler : MonoBehaviour
{
    PlayerHealthController playerHealthController;
    CharacterStats playerStats;
    CharacterInfo characterInfo;
    public static event Action<StatType> OnBuffApply;
    private void Awake()
    {
        playerHealthController = GetComponent<PlayerHealthController>();
        characterInfo = GetComponent<CharacterInfo>();
    }
    public void ApplyBuff(List<StatBuff> buffs)
    {
        foreach(var buff in buffs)
        {
            OnBuffApply?.Invoke(buff.statType);
            playerStats = characterInfo.RuntimeStats;
            switch (buff.statType)
            {
                case StatType.Damage:
                    playerStats.damage.value = CalculateNewValue(playerStats.damage.value, buff);
                    break;
                case StatType.Armor:
                    playerStats.armor.value = CalculateNewValue(playerStats.armor.value, buff);
                    break;
                case StatType.Health:
                    float oldMaxHealth = playerStats.maxHealth.value;
                    float newMaxHealth = CalculateNewValue(playerStats.maxHealth.value, buff);
                    playerStats.maxHealth.value = newMaxHealth;
                    playerHealthController.AddHealth(newMaxHealth - oldMaxHealth);
                    break;
                case StatType.Lifesteal:
                    playerStats.lifesteal.value = CalculateNewValue(playerStats.lifesteal.value, buff);
                    break;
                case StatType.CritChance:
                    playerStats.critChance.value = CalculateNewValue(playerStats.critChance.value, buff);
                    break;
                case StatType.CritDamage:
                    playerStats.critMultiplier.value = CalculateNewValue(playerStats.critMultiplier.value, buff);
                    break;
            }
        }
        HUDUIManager.Instance.SetHUDValues(playerStats);
    }
    float CalculateNewValue(float currentValue, StatBuff buff)
    {
        float finalValue = currentValue;
        switch (buff.opType)
        {
            case OperationType.Additive:
                finalValue += buff.value;
                break;
            case OperationType.Multiplier:
                finalValue *= buff.value;
                break;
            case OperationType.AdditivePercent:
                finalValue += currentValue * buff.value;
                break;
        }
        return finalValue;
    }
}
