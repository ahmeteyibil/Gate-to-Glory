using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    float lastAnimatorSpeed;
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
            case GameState.Initialization:
                break;
            case GameState.Investment:
                break;
            case GameState.Reward:
                PauseAnimation();
                break;
            case GameState.Combat:
                ResumeAnimation();
                break;
            case GameState.LevelEnd:
                break;
        }
    }
    void PauseAnimation()
    {
        lastAnimatorSpeed = animator.speed;
        animator.speed = 0f;
    }
    void ResumeAnimation()
    {
        animator.speed = lastAnimatorSpeed;
    }
}
