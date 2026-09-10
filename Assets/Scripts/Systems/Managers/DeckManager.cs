using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    private List<CardData> collectedCards = new List<CardData>();
    private void Awake()
    {
        Instance = this;
    }
    public void AddCardToDeck(CardData card)
    {
        collectedCards.Add(card);
    }
}
