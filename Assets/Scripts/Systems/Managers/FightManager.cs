using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    [SerializeField] GameObject fightEffectPrefab;
    [SerializeField] GameObject player; // Temporarily. It will be deleted and be dynamic after creating character pick system.
    [SerializeField] float fightEffectLiveDuration;
    private void OnEnable()
    {
        GameFlowManager.OnStateChanged += StateChangeHandler;
    }
    private void OnDisable()
    {
        GameFlowManager.OnStateChanged -= StateChangeHandler;
    }
    private void Awake()
    {
        Instance = this;
    }
    void StateChangeHandler(GameState newState)
    {
        if (newState == GameState.Combat)
        {
            StartCoroutine(FightCoroutine(GameManager.Instance.GetPlayerObject(), GameManager.Instance.GetCurrentEnemy()));
        }
    }

    public IEnumerator FightCoroutine(GameObject player, GameObject enemy)
    {
        Debug.Log($"{player.name} ile {enemy.name} savaþýyor!");
        CharacterStats playerStats = player.GetComponent<CharacterInfo>().RuntimeStats;
        CharacterStats enemyStats = enemy.GetComponent<CharacterInfo>().RuntimeStats;

        PlayerHealthController playerHealthController = player.GetComponent<PlayerHealthController>();
        EnemyHealthController enemyHealthController = enemy.GetComponent<EnemyHealthController>();
        //// Fight effect instantiate edilecek. (x sn ekranda kalacak. x:1sn falan olabilir.)
        //var newFightEffect = Instantiate(fightEffectPrefab, player.transform.position, Quaternion.identity);
        //yield return new WaitForSeconds(fightEffectLiveDuration);
        //Destroy(newFightEffect);
        bool isPlayerTurn = false;
        Debug.Log($"Before Fight Health Values:\nenemy health: {enemyHealthController.GetHealth()}\nplayer health: {playerHealthController.GetHealth()}");
        while (enemyHealthController.GetHealth() > 0 && playerHealthController.GetHealth() > 0)
        {
            if (isPlayerTurn)
            {
                yield return PerformAttack(playerStats, enemyStats, playerHealthController, enemyHealthController);
            }
            else
            {
                yield return PerformAttack(enemyStats, playerStats, enemyHealthController, playerHealthController);
            }
            isPlayerTurn = !isPlayerTurn;
        }
        bool playerWon = playerHealthController.GetHealth() > enemyHealthController.GetHealth();

        //// Gold karþýlaþtýrmasý yapýlacak. Ona göre kuþ ölecek veya devam edecek.
        //int coinCount = CurrencyManager.Instance.GetCoinCount();
        //int enemyHealth = enemy.GetComponent<EnemyInfo>().GetEnemyHealth();
        //playerWon = coinCount >= enemyHealth;
        Debug.Log($"playerWon: {playerWon}");
        //CurrencyManager.Instance.AddCoin(-enemyHealth);
        if (playerWon)
        {
            Destroy(enemy); // Önce destroy yapýlýyor. Tekrardan etkileþime girilmemesi için!
            LevelStatisticsManager.Instance.KillCount++;
            if (GameManager.Instance.WaveIndex >= LevelManager.Instance.LevelData.waves.Count - 1)
            {
                LevelStatisticsManager.Instance.LevelWon = true;
                GameFlowManager.Instance.SetGameState(GameState.LevelEnd);
                GameManager.Instance.GameOver();
            }
            GameFlowManager.Instance.SetGameState(GameState.Investment);
            GameManager.Instance.GoToNextWave();
        }
        else
        {
            Destroy(player);
            GameFlowManager.Instance.SetGameState(GameState.LevelEnd);
            LevelStatisticsManager.Instance.LevelWon = false;
            GameManager.Instance.GameOver();
        }

    }
    IEnumerator PerformAttack(CharacterStats attackerStats, CharacterStats targetStats, BaseHealthController attackerHealthController, BaseHealthController targetHealthController)
    {
        float baseDamage = attackerStats.damage.value;
        float randValue = Random.Range(0, 1);
        bool isCritic = randValue < attackerStats.critChance.value;
        float finalDamage = (isCritic ? baseDamage * attackerStats.critMultiplier.value : baseDamage) - targetStats.armor.value;
        targetHealthController.TakeDamage(finalDamage);
        LevelStatisticsManager.Instance.TotalDamage += finalDamage;
        float lifeStealValue = finalDamage * attackerStats.lifesteal.value;
        attackerHealthController.AddHealth(lifeStealValue);
        yield return new WaitForSeconds(.5f);
    }
}
