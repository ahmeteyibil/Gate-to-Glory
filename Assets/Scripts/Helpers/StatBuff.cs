using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatBuff
{
    public StatType statType;
    public OperationType opType;
    public float value;
    public StatBuff(StatType _statProperty, OperationType _opType, float _value)
    {
        statType = _statProperty;
        opType = _opType;
        value = _value;
    }
}
public enum StatType
{
    Damage,
    Health,
    Lifesteal,
    Armor,
    CritChance,
    CritDamage,
    EmptyType
}
