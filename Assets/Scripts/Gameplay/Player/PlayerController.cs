using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 5f;
    [Header("Align Move Settings")]
    [SerializeField] float alignMoveDuration = 1f;
    Rigidbody2D playerRb;
    Collider2D playerCollider;
    bool onGround, canJump = true;
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
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }
    private void Update()
    {
        SetInputs();
    }
    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.Initialization:
                break;
            case GameState.Investment:
                playerRb.bodyType = RigidbodyType2D.Dynamic;
                playerCollider.enabled = true;
                canJump = true;
                break;
            case GameState.Reward:
                playerRb.bodyType = RigidbodyType2D.Static;
                playerCollider.enabled = false;
                canJump = false;
                break;
            case GameState.Combat:
                break;
            case GameState.LevelEnd:
                break;
        }
    }
    public void AlignPlayerWithEnemy()
    {
        canJump = false;
        Transform enemyTransform = GameManager.Instance.GetCurrentEnemy().transform;
        transform.DOMoveY(enemyTransform.position.y, alignMoveDuration);
    }
    void SetInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            //print("space basýldý");
            playerRb.linearVelocity = Vector2.zero; // önce hýzý sýfýrla
            playerRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
