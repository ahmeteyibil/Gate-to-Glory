using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelUI : MonoBehaviour
{
    [SerializeField] GameObject inventoryCardPrefab;
    [SerializeField] Transform cardsCollected;
    [SerializeField] float panelFadeDuration = 0.2f;
    CanvasGroup pausePanelCG;
    private void OnEnable()
    {
        CardManager.OnCollect += AddToCollectedCards;
    }
    private void OnDisable()
    {
        CardManager.OnCollect -= AddToCollectedCards;
    }
    private void Awake()
    {
        pausePanelCG = GetComponent<CanvasGroup>();
    }
    void AddToCollectedCards(CardData newCardData)
    {
        var newInvCard = Instantiate(inventoryCardPrefab, cardsCollected);
        newInvCard.GetComponent<InventoryCardManager>().
            Setup(newCardData, DraftManager.Instance.GetCardBackgroundByRarity(newCardData.rarity));
    }
    public void ResumeButtonHandler()
    {
        GameManager.Instance.ResumeGame();
        ClosePanel();
    }
    public void MenuButtonHandler()
    {
        SceneManager.LoadSceneAsync(SceneName.SN_MENU);
        GameManager.Instance.ResumeGame();
    }
    public void OpenPanel()
    {
        pausePanelCG.interactable = true;
        pausePanelCG.blocksRaycasts = true;

        pausePanelCG.DOFade(1f, panelFadeDuration).SetUpdate(true);
    }
    public void ClosePanel()
    {
        pausePanelCG.interactable = false;
        pausePanelCG.blocksRaycasts = false;

        pausePanelCG.DOFade(0f, panelFadeDuration).SetUpdate(true);
    }
}
