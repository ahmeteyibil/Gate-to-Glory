using UnityEngine;
using static UnityEngine.CullingGroup;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class DraftManager : MonoBehaviour
{
    public static DraftManager Instance;
    [SerializeField] RewardUI rewardUI;
    [SerializeField] Transform cardsPanel;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] float cardsPanelFadeDuration = .5f;
    
    [SerializeField] float maxRecreatePrice = 100, maxRecreateCount = 2;
    [SerializeField] Sprite commonCardBackground, rareCardBackground, epicCardBackground, legendaryCardBackground;
    CanvasGroup cardsPanelCG;
    public List<GameObject> CurrentCards { get; set; } = new List<GameObject>();
    public int RecreatePrice { get; set; } = 10;
    int recreateCount = 0;
    private void OnEnable()
    {
        GameFlowManager.OnStateChanged += StateChangeHandler;
    }
    private void OnDisable()
    {
        GameFlowManager.OnStateChanged -= StateChangeHandler;
    }
    void Awake()
    {
        Instance = this;
        cardsPanelCG = cardsPanel.GetComponent<CanvasGroup>();
    }
    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.Reward:
                // Eðer son wavede isek boss market'i aç.
                rewardUI.OpenRewardPanel();
                CreateCards(3);
                break;
            case GameState.Initialization:
                break;
            case GameState.Combat:
                rewardUI.CloseRewardPanel();
                DestroyCards();
                recreateCount = 0; // Recreate sayýsý birdahaki reward phase için sýfýrlanýr.
                break;
        }
    }
    public void RecreateCards()
    {
        if (CurrencyManager.Instance.GetCoinCount() < RecreatePrice)
        {
            Debug.Log("Kartlar yeniden oluþturulmadý çünkü para yetersiz.");
            return;
        }
        recreateCount++;
        if (recreateCount >= maxRecreateCount)
        {
            // Recreate buton mevcut reward phase için disable edilir.
            rewardUI.DisableRecreateButton();
        }
        CurrencyManager.Instance.AddCoin(-RecreatePrice);

        DestroyCards();
        CreateCards(3);
        RecreatePrice = (int)Mathf.Min(maxRecreatePrice, RecreatePrice * 2);

    }
    void CreateCards(int amount)
    {
        List<CardData> randomCards = CardPoolManager.Instance.GetRandomCardsByPrice(amount, CurrencyManager.Instance.GetCoinCount());
        foreach (var card in randomCards)
        {
            GameObject newCardObject = Instantiate(cardPrefab, cardsPanel);
            Sprite cardBackground = GetCardBackgroundByRarity(card.rarity);
            newCardObject.GetComponent<CardManager>().Setup(card, cardBackground);
            LayoutRebuilder.ForceRebuildLayoutImmediate(cardsPanel.GetComponent<RectTransform>());
            CurrentCards.Add(newCardObject);
            Debug.Log($"new card created: {newCardObject.name}. position: {newCardObject.transform.position}");
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(cardsPanel.GetComponent<RectTransform>());
        cardsPanelCG.DOFade(1f, cardsPanelFadeDuration);
        StartCoroutine(rewardUI.ShowCardsCoroutine(CurrentCards));
    }
   
    public Sprite GetCardBackgroundByRarity(CardRarity rarity)
    {
        switch (rarity)
        {
            case CardRarity.Common:
                return commonCardBackground;
            case CardRarity.Rare:
                return rareCardBackground;
            case CardRarity.Epic:
                return epicCardBackground;
            case CardRarity.Legendary:
                return legendaryCardBackground;
            default:
                return commonCardBackground;
        }
    }
    void DestroyCards()
    {
        foreach (var card in CurrentCards)
        {
            Destroy(card);
        }
        CurrentCards.Clear();
    }
}
