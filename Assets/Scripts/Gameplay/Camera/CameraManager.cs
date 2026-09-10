using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Combat Align Settings")]
    [SerializeField] float alignMoveDuration = 1f;
    private void OnEnable()
    {
        GameFlowManager.OnStateChanged += StateChangeHandler;
    }
    private void OnDisable()
    {
        GameFlowManager.OnStateChanged -= StateChangeHandler;
    }
    public void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.Reward:
                AlignCameraWithCombat();
                break;
            case GameState.Investment:
                AlignCameraWithCombatEnd();
                break;
        }
    }
    private void AlignCameraWithCombat()
    {
        float enemyX = GameManager.Instance.GetCurrentEnemy().transform.position.x;
        float playerX = GameManager.Instance.GetPlayerObject().transform.position.x;
        float newX = playerX + (Mathf.Abs(enemyX - playerX) / 2);
        transform.DOMoveX(newX, alignMoveDuration);
    }
    private void AlignCameraWithCombatEnd()
    {
        transform.DOMoveX(0f, alignMoveDuration);
    }

}
