using UnityEngine;

public class EnemyDistanceTracker : MonoBehaviour
{
    public static EnemyDistanceTracker Instance;
    private float distanceToCurrent;
    private float distanceToLast;
    float completeRatio;
    float maxDistance;
    Transform currentEnemy;
    Transform playerTransform;
    Transform lastEnemyFightColliderTransform;
    BoxCollider2D lastEnemyFightCollider;
    Vector3 startPos;

    public float CompleteRatio => completeRatio;
    public float MaxDistance => maxDistance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(playerTransform && lastEnemyFightColliderTransform)
        {

            //Vector2 movementFinishPosition = lastEnemyFightColliderTransform.position + new Vector3((1/ lastEnemyFightCollider.size.x) * lastEnemyFightCollider.offset.x, 0f, 0f);
            distanceToLast = Vector2.Distance(playerTransform.position, lastEnemyFightColliderTransform.position);
            completeRatio = 1-(distanceToLast / maxDistance);
            //Debug.Log($"completeRatio: {completeRatio}");
        }
    }

    public void Setup(Transform playerTransform, Transform lastEnemyFightColliderTransform, Vector3 startPos)
    {
        this.lastEnemyFightColliderTransform = lastEnemyFightColliderTransform;
        lastEnemyFightCollider = lastEnemyFightColliderTransform.GetComponent<BoxCollider2D>();
        this.playerTransform = playerTransform;
        this.startPos = startPos;
        //Vector2 movementFinishPosition = lastEnemyFightColliderTransform.position + new Vector3((1 / lastEnemyFightCollider.size.x) * lastEnemyFightCollider.offset.x, 0f, 0f);
        maxDistance = Vector2.Distance(startPos, lastEnemyFightColliderTransform.position);
    }
}
