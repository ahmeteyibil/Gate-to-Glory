using System;
using System.Collections.Generic;
public class BuffEvent
{
    public static event Action<Gate> OnBuffApplied;

    public static void Trigger(Gate data)
    {
        OnBuffApplied?.Invoke(data);
    }
}
[Serializable]
public class Gate
{
    public OperationType type;
    public float value;
    public bool isHidden;
    public string label;
}

public enum OperationType { Multiplier, Additive, AdditivePercent }
