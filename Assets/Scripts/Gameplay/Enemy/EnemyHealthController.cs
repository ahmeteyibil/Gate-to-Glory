using UnityEngine;

public class EnemyHealthController : BaseHealthController
{
    protected override void Die()
    {
        Destroy(gameObject);
        Debug.Log($"{this.gameObject} Died");
        
    }
}
