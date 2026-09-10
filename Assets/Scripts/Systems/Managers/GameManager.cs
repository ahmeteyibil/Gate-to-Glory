using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject[] managerObjects;
    [Header("References")]
    [SerializeField] GameOverUI gameOverUI;
    int enemyIndex;
    GameObject player;
    Transform lastEnemyFightCollider;
    CharacterData playerData;
    CharacterInfo playerInfo;
    PlayerHealthController playerHealthController;
    PlayerBuffHandler playerBuffHandler;
    List<GameObject> enemyObjects = new List<GameObject>();
    public int WaveIndex { get; private set; } = 0;
    public List<GameObject> EnemyObjects => enemyObjects;
    public GameObject PlayerObject => player;
    public Transform LastEnemyFightCollider => lastEnemyFightCollider;
    private void OnEnable()
    {
        GameFlowManager.OnStateChanged += StateChangeHandler;
    }
    private void OnDisable()
    {
        GameFlowManager.OnStateChanged -= StateChangeHandler;
    }
    void StateChangeHandler(GameState newState) 
    {
        switch (newState)
        {
            case GameState.Reward:
                PauseAllManagers();
                break;
            case GameState.Investment:
                ResumeAllManagers();
                break;
            case GameState.Initialization:
                break;
            case GameState.Combat:
                break;
        }
    }
    private void Awake()
    {
        Instance = this;
    }

    public void PauseAllManagers()
    {
        foreach (var managerObject in managerObjects)
        {
            managerObject.SetActive(false);
        }
    }
    public void ResumeAllManagers()
    {
        foreach (var managerObject in managerObjects)
        {
            managerObject.SetActive(true);
        }
    }
    public void AddEnemy(GameObject enemyObj)
    {
        enemyObjects.Add(enemyObj);
    }
    public Transform GetLastEnemyFightCollider()
    {
        GameObject lastEnemy = enemyObjects[enemyObjects.Count - 1];
        Debug.Log($"Last enemy: {lastEnemy.name}");
        if (!lastEnemy) return null;
        Transform fightColliderTransform = lastEnemy.transform.Find("FightColliderObject");
        if (!fightColliderTransform) return null;
        return fightColliderTransform;
    }
    public void GameOver()
    {
        PauseAllManagers();
        gameOverUI.PanelOpenSequence();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneName.SN_PLAY);
    }
    public void GoToNextWave()
    {
        WaveIndex++;
        enemyIndex++;
        LevelStatisticsManager.Instance.WavesCompleted++;
    }
    public void SetCurrentEnemy(int _enemyIndex)
    {
        enemyIndex = _enemyIndex;
    }
    public void SetPlayer(GameObject _player, CharacterData _playerData)
    {
        // Atamalar.
        player = _player;
        playerData = _playerData;
        playerBuffHandler = player.GetComponent<PlayerBuffHandler>();
    }
    public GameObject GetPlayerObject()
    {
        return player;
    }
    public CharacterData GetPlayerData()
    {
        return playerData;
    }
    public GameObject GetCurrentEnemy()
    {
        return enemyObjects[enemyIndex];
    }
    public PlayerBuffHandler GetPlayerBuffHandler()
    {
        return playerBuffHandler;
    }
}
