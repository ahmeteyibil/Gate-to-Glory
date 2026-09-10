using System;
using System.Collections.Generic;
using System.Linq; // LINQ iþlemleri için gerekli
using Unity.Mathematics;
using UnityEngine;

public class CardPoolManager : MonoBehaviour
{
    public static CardPoolManager Instance;

    [Header("Rarity Pools")]
    [SerializeField] List<CardData> freePool;
    [SerializeField] List<CardData> commonPool;
    [SerializeField] List<CardData> rarePool;
    [SerializeField] List<CardData> epicPool;
    [SerializeField] List<CardData> legendaryPool;

    // Tüm kartlarýn birleþtiði ana liste (Otomatik dolacak)
    private List<CardData> allCards = new List<CardData>();

    private void Awake()
    {
        Instance = this;
        InitializeAllCards();
    }

    private void InitializeAllCards()
    {
        // Tüm havuzlarý tek bir listede topluyoruz.
        // FreePool'u genellikle markette istemediðin için dahil etmiyoruz, 
        // ancak istersen onu da ekleyebilirsin.
        allCards.Clear();
        allCards.AddRange(commonPool);
        allCards.AddRange(rarePool);
        allCards.AddRange(epicPool);
        allCards.AddRange(legendaryPool);
    }

    public List<CardData> GetRandomCardsByPrice(int amount,int currentCoin)
    {
        // Alt eþik belirle: Paranýn %40'ýndan ucuz kartlarý gösterme
        // Formül: $minThreshold = currentCoin \times 0.4$
        float minThreshold = currentCoin * 0.4f;

        // 1. Filtreleme Uygula (IEnumerable döner, henüz listeye çevrilmez)
        var affordableCards = allCards.Where(card =>
            card.price <= currentCoin &&
            card.price >= minThreshold
        ).ToList();

        // 2. Güvenlik Kontrolü (Fallback): 
        // Eðer filtrelenmiþ havuzda 3'ten az kart kaldýysa alt sýnýrý kaldýr
        if (affordableCards.Count < amount)
        {
            affordableCards = allCards.Where(card => card.price <= currentCoin).Distinct().ToList();
        }

        // 3. Merhamet Sistemi (Pity System): 
        // Eðer parasý hiçbir þeye yetmiyorsa Free kartlar ekle
        while (affordableCards.Count < amount)
        {
            var shuffledFree = freePool.Distinct().OrderBy(x => UnityEngine.Random.value).ToList();

            foreach (var freeCard in shuffledFree)
            {
                if (!affordableCards.Contains(freeCard))
                {
                    affordableCards.Add(freeCard);
                }

                if (affordableCards.Count >= amount) break;
            }
        }

        // 4. Karýþtýr ve amount tane seç
        return affordableCards
            .OrderBy(x => UnityEngine.Random.value)
            .Take(amount)
            .ToList();
    }
}