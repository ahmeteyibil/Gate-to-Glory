using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    GameObject Player { get; set; }
    public GameObject Enemy { get; set; }
    public float DistanceToEnemy { get; private set; }
    private void Update()
    {
        if (ScrollManager.Instance.enabled && Player != null && Enemy != null)
        {
            DistanceToEnemy = Vector2.Distance(Player.transform.position, Enemy.transform.position);
            //UIManager.Instance.UpdateEnemyDistanceBar(Distance);
        }
    }
}
