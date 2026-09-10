using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCardManager : MonoBehaviour
{
    [SerializeField] TMP_Text cardLabelText, cardNameText;
    [SerializeField] Image cardImage, cardBackground;
    CardData cardData;
    public void Setup(CardData _cardData, Sprite backgroundSprite)
    {
        cardData = _cardData;
        cardBackground.sprite = backgroundSprite;
        cardImage.sprite = cardData.icon;
        cardLabelText.text = cardData.label;
        cardNameText.text = cardData.cardName;
    }
}
