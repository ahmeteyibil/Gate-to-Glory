using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HUDUIManager : MonoBehaviour
{
    public static HUDUIManager Instance;
    
    [SerializeField] TMP_Text damageText, armorText, lifestealText, critChanceText, critDamageText;
    private void Awake()
    {
        Instance = this;
    }
    public void SetHUDValues(CharacterStats playerStats)
    {
        SetDamageText(playerStats.damage.value);
        SetArmorText(playerStats.armor.value);
        SetLifestealText(playerStats.lifesteal.value);
        SetCritChanceText(playerStats.critChance.value);
        SetCritDamageText(playerStats.critMultiplier.value * playerStats.damage.value);
    }
    public void SetDamageText(float value)
    {
        damageText.text = Mathf.Round(value).ToString();
    }
    public void SetArmorText(float value)
    {
        armorText.text = Mathf.Round(value).ToString();
    }
    public void SetLifestealText(float value)
    {
        lifestealText.text = $"%{value*100}";
    }
    public void SetCritChanceText(float value)
    {
        critChanceText.text = $"%{value * 100}";
    }
    public void SetCritDamageText(float critDamage)
    {
        critDamageText.text = Mathf.Round(critDamage).ToString();
    }
}
