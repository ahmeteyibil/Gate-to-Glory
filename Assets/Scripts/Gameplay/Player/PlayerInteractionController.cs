using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] int seedCollectPoint = 100, bombCollectPoint = -50, thornTouchPoint = -200;
    PlayerAnimationController playerAnimationController;
    PlayerHealthController playerHealthController;
    BgCreaterManager bgCreaterManager;
    private void Awake()
    {
        playerAnimationController = GetComponent<PlayerAnimationController>();
        playerHealthController = this.transform.GetComponent<PlayerHealthController>();
        bgCreaterManager = GameObject.FindWithTag("BgCreaterManager").GetComponent<BgCreaterManager>();
        // Bu yöntem bana çok saðlýksýz geliyor ama burada mecbur kaldým. deneme
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Tags.TAG_BG1))
        {
            bgCreaterManager.CreateBg();
        }
    }


}
