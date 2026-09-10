using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectCreaterManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform, topGround, bottomGround, dynamicGameObjects;
    [SerializeField] Renderer bgRenderer;
    [SerializeField] List<GameObject> objectPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> spawnPatternPrefabs = new List<GameObject>();
    [SerializeField] float createRepeatTime, yMargin = 3f;
    List<string[]> spawnPatterns = new List<string[]>()
    {
        new[] {"Bomb","Bomb","Bomb","Bomb"},
        new[] {"Seed","Bomb","Seed","Bomb"},
        new[] {"Seed","Seed","Seed"}
    };
    float bgWidth, spawnOffsetToPlayer;
    private void Start()
    {
        InvokeRepeating(nameof(CreateSpawnPattern), 0f, createRepeatTime);
        bgWidth = bgRenderer.bounds.size.x;
        spawnOffsetToPlayer = bgWidth;
        print("bgWidth: " + bgWidth);
    }
    public void UpdateCreateRepeatTime(float subtractValue)
    {
        createRepeatTime -= subtractValue;
        print("createRepeatTime updated. New value: " + createRepeatTime);
        CancelInvoke(nameof(CreateSpawnPattern));
        // Yeni zamanla tekrar baþlat
        InvokeRepeating(nameof(CreateSpawnPattern), 0f, createRepeatTime);
    }
    void CreateSpawnPattern()
    {
        if (!playerTransform) CancelInvoke(nameof(CreateSpawnPattern));
        int randNum = Random.Range(0, spawnPatternPrefabs.Count);
        float randY = Random.Range(bottomGround.position.y + yMargin, topGround.position.y - yMargin);
        GameObject randomSpawnPattern = spawnPatternPrefabs[randNum];
        Vector3 newSpawnPatternPosition = playerTransform.position +
            new Vector3(spawnOffsetToPlayer, (randY - playerTransform.position.y), 0f);
        //print("newObjectPosition: " + newObjectPosition);

        var newObject = Instantiate(randomSpawnPattern, newSpawnPatternPosition, Quaternion.identity, dynamicGameObjects);
    }
    void CreateObject()
    {
        if (playerTransform != null)
        {
            int randNum = Random.Range(0, objectPrefabs.Count);
            float randY = Random.Range(bottomGround.position.y + yMargin, topGround.position.y - yMargin);
            GameObject randomObject = objectPrefabs[randNum];
            Vector3 newObjectPosition = playerTransform.position +
                new Vector3(bgWidth, (randY - playerTransform.position.y), 0f);
            //print("newObjectPosition: " + newObjectPosition);

            var newObject = Instantiate(randomObject, newObjectPosition, Quaternion.identity, dynamicGameObjects);
        }
    }
}
