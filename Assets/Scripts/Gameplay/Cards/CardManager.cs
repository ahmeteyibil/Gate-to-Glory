using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardManager : MonoBehaviour
{
    [SerializeField] TMP_Text cardLabelText, cardNameText, cardPriceText;
    [SerializeField] Image cardImage, cardBackground, goldIconImage;
    [SerializeField]
    float pickScaleValue = .8f, pickScaleDuration = 0.2f, pickTransparentDuration = 0.3f,
        showFadeDuration = 0.5f, showAnimationVerticalAddValue = 0.5f;
    [SerializeField] CanvasGroup cardCG;

    CardData cardData;
    public static event Action<CardData> OnCollect;
    public void Setup(CardData _cardData, Sprite backgroundSprite)
    {
        cardData = _cardData;
        cardBackground.sprite = backgroundSprite;
        cardImage.sprite = cardData.icon;
        cardLabelText.text = cardData.label;
        cardNameText.text = cardData.cardName;
        if(cardData.price == 0)
        {
            goldIconImage.gameObject.SetActive(false);
            cardPriceText.text = "Free"; 
        }
        else
        {
            goldIconImage.gameObject.SetActive(true);
            cardPriceText.text = cardData.price.ToString();
        }
        cardCG.alpha = 0f;
    }
    public void ShowAnimation()
    {
        Debug.Log($"{gameObject.name} position before show animation: {transform.position}");
        transform.position = transform.position - Vector3.up * showAnimationVerticalAddValue;
        cardCG.DOFade(1f, showFadeDuration);
        transform.DOMoveY(transform.position.y + showAnimationVerticalAddValue, showFadeDuration);
        Debug.Log($"{gameObject.name} position after show animation: {transform.position}");
    }
    public void CardClickHandler() // Týklanma olayýný dinler
    {
        OnCollect?.Invoke(cardData);
        cardCG.interactable = false;
        Sequence pickSequence = DOTween.Sequence();
        pickSequence.Join(transform.DOScale(pickScaleValue, pickScaleDuration));
        pickSequence.Join(cardCG.DOFade(0.5f, pickTransparentDuration));

        pickSequence.OnComplete(() =>
        {
            // Kartýn ücreti kadar altýn düþ
            CurrencyManager.Instance.AddCoin(-cardData.price);

            // Player'a geçerli buff'ý yap.
            GameManager.Instance.GetPlayerBuffHandler().ApplyBuff(cardData.buffs);
            DeckManager.Instance.AddCardToDeck(cardData);
            // GameState'i Combat yap.
            GameFlowManager.Instance.SetGameState(GameState.Combat);
        });        
    }
}
