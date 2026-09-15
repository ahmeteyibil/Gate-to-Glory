using System.Linq;
using UnityEngine;

public class LeftWallInteraction : MonoBehaviour
{
    string[] dynamicTags = {Tags.TAG_SEED,Tags.TAG_COIN,Tags.TAG_BOMB, Tags.TAG_BG };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collisionTag = collision.transform.tag;
        //print("collisionTag: " + collisionTag);

        if(dynamicTags.Contains(collisionTag))
        {
            Destroy(collision.gameObject,2f);
        }
    }
}
