using UnityEngine;


public class PlayerAnimationController : MonoBehaviour
{
    float protectionTime;
    Animator playerAnimator;
    PlayerHealthController healthController;
    private void Awake()
    {
        healthController = GetComponent<PlayerHealthController>();
        playerAnimator = GetComponent<Animator>();
    }
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
        if(newState == GameState.Reward)
        {
            PauseAnimation();
        }
        else
        {
            ResumeAnimation();
        }
    }
    public void HurtAnimation()
    {
        playerAnimator.SetTrigger(AnimatorVariables.P_DAMAGE);
    }
    public void BackToFly()
    {
        //print("OverHurting() fonksiyonu worked.");
        playerAnimator.SetTrigger(AnimatorVariables.P_BACKTOFLY);
    }
    void PauseAnimation()
    {
        // Animatorde güncel olarak oynanan animasyonu durduracak.
        playerAnimator.speed = 0f;
    }
    void ResumeAnimation()
    {
        playerAnimator.speed = 1f;
    }

}
