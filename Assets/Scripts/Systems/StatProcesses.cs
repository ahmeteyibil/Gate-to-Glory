using UnityEngine;

public static class StatProcesses
{
    public static string GetDescriptionByType(StatType type)
    {
        switch (type)
        {
            case StatType.Damage:
                return "Düþmana vurulan hasar.";
            case StatType.Armor:
                return "Düþmandan alýnan hasarý engeller.";
            case StatType.Health:
                return "Maksimum saðlýk";
            case StatType.Lifesteal:
                return "Verilen hasarýn iyileþme olarak dönme miktarý";
            case StatType.CritChance:
                return "Hasarýn kritik gitme ihtimali";
            case StatType.CritDamage:
                return "Kritik hasar. Normal hasardan daha kuvvetlidir.";
            default:
                return null;
        }
    }
}
