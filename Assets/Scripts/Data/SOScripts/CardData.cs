using System.Collections.Generic;
using UnityEngine;

public enum CardRarity { Free, Common, Rare, Epic, Legendary }

[CreateAssetMenu(menuName = "ScriptableObjects/Card")]
public class CardData : ScriptableObject
{
    public string cardID;
    public string cardName;
    public string description;
    public string label;
    public Sprite icon;
    public int price; // Market sistemi veya kart deðeri için
    public CardRarity rarity;
    public List<StatBuff> buffs;
}