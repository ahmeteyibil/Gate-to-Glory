using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public static GameInitializer Instance;
    [Header("Parents")]
    [SerializeField] Transform enemyParent, gateSetParent, playerParent;
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab, gateSetPrefab;
    [Header("Positioning Settings")]
    [SerializeField] float firstGateOffset = 8f, gateOffset = 1f, lastGateOffset = 5f;
    List<SpawnObject> spawnQueue = new List<SpawnObject>();
    List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
        LevelData levelData = LevelManager.Instance.LevelData;
        if (levelData != null)
        {
            InitializeGame(levelData);
        }
        else
        {
            Debug.LogWarning("levelData null idi. Oyun baþlatýlamadý");
        }

    }
    private void Start()
    {
        // Bu objeler uygun konumlara spawn edilecek.
        StartCoroutine(SpawnObjectsCoroutine(() =>
        {
            Transform player, lastEnemyCollider;
            player = GameManager.Instance.PlayerObject.transform;
            lastEnemyCollider = GameManager.Instance.GetLastEnemyFightCollider();
            EnemyDistanceTracker.Instance.Setup(player,lastEnemyCollider,player.transform.position);
            UIManager.Instance.SetupEnemyDistanceBarMarkers(GameManager.Instance.EnemyObjects, GameManager.Instance.PlayerObject.transform);
        }));
    }
    public void InitializeGame(LevelData levelData)
    {
        // Oyunun baþýnda gold miktarý belirlenecek.
        CurrencyManager.Instance.SetCoinCount(1);
        // Level'in background'u tanýtýlacak
        Debug.Log($"levelData.backgroundID: {levelData.backgroundID}");
        BgCreaterManager.Instance.SetBackground(levelData.backgroundID);
        // Oyunun baþýnda spawn edilecek objeler belirlenecek. Gateler, enemyler.
        int enemyCount = 0;
        float lastEnemyX = 0;

        // Player GameObject'teki player'a set edilecek
        CharacterData playerData = CharacterManager.Instance.GetPickedCharacter();
        SpawnObject playerSO = new SpawnObject(playerData.prefab, playerParent, Vector2.zero);
        playerSO.characterData = playerData;
        spawnQueue.Add(playerSO);

        foreach (var wave in levelData.waves)
        {
            enemyCount++;
            foreach (var gateSet in wave.investmentPhase.gateSets)
            {
                float currentGateSetX = lastEnemyX + gateSet.distanceMark;
                SpawnObject newGateSet = new SpawnObject(gateSetPrefab, gateSetParent, new Vector3(currentGateSetX, 0f, 0f));
                newGateSet.gateSet = gateSet;
                spawnQueue.Add(newGateSet);
            }
            CharacterData enemyData = CharacterManager.Instance.GetCharByID(wave.combatPhase.enemyID);
            SpawnObject newEnemy = new SpawnObject(enemyData.prefab, enemyParent, new Vector3(lastEnemyX + wave.investmentPhase.totalDistance, 0f, 0f));
            newEnemy.characterData = enemyData;
            lastEnemyX += wave.investmentPhase.totalDistance;
            //Debug.Log($"wave.combatPhase.enemy.enemyID: {wave.combatPhase.enemy.enemyID}");
            spawnQueue.Add(newEnemy);
        }
    }
    IEnumerator SpawnObjectsCoroutine(Action onSpawnComplete)
    {
        foreach (var spawnObject in spawnQueue)
        {
            var spawnedObject = Instantiate(spawnObject.objectToSpawn, spawnObject.spawnPosition, Quaternion.identity, spawnObject.parent);
            if (spawnObject.parent == playerParent)
            {
                GameManager.Instance.SetPlayer(spawnedObject, spawnObject.characterData);
                CharacterInfo playerInfo = spawnedObject.GetComponent<CharacterInfo>();
                PlayerHealthController playerHealthController = spawnedObject.GetComponent<PlayerHealthController>();
                // Player'ýn statlarýný CharacterInfo'ya gir. 
                playerInfo.SetStats(spawnObject.characterData.baseStats);
                // HUD UI'ý güncelle.
                HUDUIManager.Instance.SetHUDValues(spawnObject.characterData.baseStats);
                // Player'ýn can sistemini baþlat.
                playerHealthController.InitializeHealth();
            }
            if (spawnObject.parent != playerParent && spawnObject.characterData != null) // CharacterData içeriyorsa ve parent'i playerParent deðilse bu bir enemy'dir.
            {
                spawnedEnemies.Add(spawnedObject);
                GameManager.Instance.AddEnemy(spawnedObject);
                if (spawnedEnemies.Count == 1)
                {
                    GameManager.Instance.SetCurrentEnemy(0);
                }
                CharacterInfo info = spawnedObject.GetComponent<CharacterInfo>();
                info.SetStats(spawnObject.characterData.baseStats);
                spawnedObject.GetComponent<EnemyHealthController>().InitializeHealth();
            }
            if (spawnObject.gateSet != null)
            {
                GateSetManager manager = spawnedObject.GetComponent<GateSetManager>();
                if (manager)
                {
                    manager.InitiazlieGates(spawnObject.gateSet.gates);
                }
                else
                {
                    Debug.Log($"{manager} bulunamadi.");
                }
            }
            if (spawnQueue.IndexOf(spawnObject) % 3 == 0) // 3 spawnda bir frame bekleme.
            {
                yield return null;
            }
        }
        onSpawnComplete?.Invoke();
    }
}

