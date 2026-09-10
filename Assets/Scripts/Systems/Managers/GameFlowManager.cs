using System;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;
    public static event Action<GameState> OnStateChanged;
    GameState currentGameState;
    
    private void Awake()
    {
        Instance = this;
    }
    public void SetGameState(GameState newState)
    {
        currentGameState = newState;
        OnStateChanged?.Invoke(currentGameState);
    }
}