using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BgCreaterManager : MonoBehaviour
{
    public static BgCreaterManager Instance;
    [Header("Settings")]
    [SerializeField] int bgStartCount = 2;
    [Header("Background Sprites")]
    [SerializeField] List<BackgroundData> backgroundSprites;
    [Header("References")]
    [SerializeField] GameObject bgPrefab;
    [SerializeField] Transform bgParent;
    BackgroundData currentBackground;
    int bgCount = 0;
    float bgWidth;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for(int i = 0; i < bgStartCount; i++)
        {
            CreateBg();
        }
    }
    public void SetBackground(string backgroundID)
    {
        currentBackground = backgroundSprites.FirstOrDefault(background => background.backgroundID == backgroundID);
        if(currentBackground == null)
        {
            Debug.LogWarning("Background bulunamadi.");
        }
        bgWidth = currentBackground.backgroundSprite.bounds.size.x;
    }
    public void CreateBg()
    {
        Vector3 bgPos = new Vector3(bgCount*bgWidth,0f,0f);
        var newBg = Instantiate(bgPrefab, bgParent);
        newBg.transform.localPosition = bgPos;
        newBg.GetComponent<SpriteRenderer>().sprite = currentBackground.backgroundSprite;
        bgCount++;
    }
}