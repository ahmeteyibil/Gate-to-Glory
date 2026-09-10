using DG.Tweening;
using UnityEngine;

public class EnemyInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.TAG_PLAYER))
        {
            Debug.Log("Player Enemy'nin collider'ý ile etkileþime girdi.");
            GameManager.Instance.GetPlayerObject().GetComponent<PlayerController>().AlignPlayerWithEnemy();


            GameFlowManager.Instance.SetGameState(GameState.Reward);
        }
    }
}
