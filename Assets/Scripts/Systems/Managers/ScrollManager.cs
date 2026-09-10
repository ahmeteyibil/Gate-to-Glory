using Unity.VisualScripting;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public static ScrollManager Instance;
    [SerializeField] Transform dynamicGameObjects;
    [SerializeField] Transform player, enemy;
    [SerializeField] float scrollSpeed = 2f;
    bool stopScroll = false;
    float firstDistance;
    float enemyDistance;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        ScrollDynamicObjects();
    }
    void ScrollDynamicObjects()
    {
        if (stopScroll) return;
        dynamicGameObjects.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
        UIManager.Instance.UpdateEnemyDistanceBar();
    }
    public void StopScroll()
    {
        stopScroll = true;
    }
    public void ResumeScroll()
    {
        stopScroll = false;
    }
    public void UpdateScrollSpeed(float multiplier)
    {
        scrollSpeed *= multiplier;
        print("scrollSpeed updated to "+ scrollSpeed);
    }
    public void SetCurrentEnemy(Transform _enemy)
    {
        enemy = _enemy;

        if (player != null && enemy != null)
        {
            enemyDistance = Vector2.Distance(new Vector2(player.position.x, 0f), new Vector2(enemy.position.x, 0f));
            firstDistance = enemyDistance;
        }
    }
    
}
