using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    
    int coinCount;
    public static event Action<int> OnCoinChanged;
    private void OnEnable()
    {
        BuffEvent.OnBuffApplied += ApplyGateBuff;
    }

    private void OnDisable()
    {
        BuffEvent.OnBuffApplied -= ApplyGateBuff;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void ApplyGateBuff(Gate gate)
    {
        Debug.Log($"gate: {gate},gate.type: {gate.type}, gate.value: {gate.value}");
        switch (gate.type)
        {
            case OperationType.Additive:
                AddCoin((int)gate.value);
                break;
            case OperationType.AdditivePercent:
                AddPercentCoin(gate.value);
                break;
            case OperationType.Multiplier:
                MultiplyCoin(gate.value);
                break;
        }
    }
    public void AddCoin(int value)
    {
        coinCount += value;
        coinCount = Mathf.Max(0,coinCount);
        OnCoinChanged?.Invoke(coinCount);
    }
    public void AddPercentCoin(float value)
    {
        coinCount += Mathf.RoundToInt(coinCount * value); 
        OnCoinChanged?.Invoke(coinCount);
    }
    public void MultiplyCoin(float value)
    {
        coinCount = Mathf.RoundToInt(coinCount * value);
        coinCount = Mathf.Max(0, coinCount);
        OnCoinChanged?.Invoke(coinCount);
    }
    public void SetCoinCount(int value) 
    { 
        coinCount = value;
        coinCount = Mathf.Max(0, coinCount);
        OnCoinChanged?.Invoke(coinCount); 
    }
    public int GetCoinCount() { return coinCount; }
    
}
