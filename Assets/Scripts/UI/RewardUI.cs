using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class RewardUI : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeScreenCG, rewardPanelCG;
    [SerializeField] float fadeScreenAlphaValue = .9f, fadeScreenDuration = 1f, rewardPanelFadeDuration = 0.5f, cardShowOffsetDuration = 0.2f;
    [SerializeField] TMP_Text recreatePriceText;
    [SerializeField] GameObject recreateButton;
    void OpenFadeScreen()
    {
        fadeScreenCG.DOFade(fadeScreenAlphaValue, fadeScreenDuration);
        fadeScreenCG.blocksRaycasts = true;
        fadeScreenCG.interactable = true;
    }
    void CloseFadeScreen()
    {
        fadeScreenCG.DOFade(0f, fadeScreenDuration);
        fadeScreenCG.blocksRaycasts = false;
        fadeScreenCG.interactable = false;
    }

    public void OpenRewardPanel()
    {
        OpenFadeScreen();
        rewardPanelCG.DOFade(1f, rewardPanelFadeDuration);
        rewardPanelCG.blocksRaycasts = true;
        rewardPanelCG.interactable = true;
        UpdateRecreateText();
        EnableRecreateButton();
    }
    public void CloseRewardPanel()
    {
        CloseFadeScreen();
        rewardPanelCG.DOFade(0f, rewardPanelFadeDuration);
        rewardPanelCG.blocksRaycasts = false;
        rewardPanelCG.interactable = false;
    }
    public void RecreateCardsButtonHandler()
    {
        DraftManager.Instance.RecreateCards();
        UpdateRecreateText();
    }
    public IEnumerator ShowCardsCoroutine(List<GameObject> cards)
    {
        foreach (var card in cards)
        {
            card.GetComponent<CardManager>().ShowAnimation();
            yield return new WaitForSeconds(cardShowOffsetDuration);
        }
    }
    void UpdateRecreateText()
    {
        recreatePriceText.text = DraftManager.Instance.RecreatePrice.ToString();
        Debug.Log($"recrate price: {DraftManager.Instance.RecreatePrice}, current coin: {CurrencyManager.Instance.GetCoinCount()}");
        if (DraftManager.Instance.RecreatePrice > CurrencyManager.Instance.GetCoinCount())
        {
            recreatePriceText.color = Color.red;
        }
        else
        {
            recreatePriceText.color = Color.white;
        }
    }
    public void DisableRecreateButton()
    {
        recreateButton.SetActive(false);
    }
    public void EnableRecreateButton()
    {
        recreateButton.SetActive(true);
    }
}
