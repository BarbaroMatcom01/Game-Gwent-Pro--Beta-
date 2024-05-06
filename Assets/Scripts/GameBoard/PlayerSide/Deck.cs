using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    public List<CardData> DeckCards = new List<CardData>();
    [SerializeField] private Silver Silver;
    [SerializeField] private Golden Golden;
    [SerializeField] private Special Special;
    [SerializeField] private GameObject Hand;

    public int CountCardsInDeck => DeckCards.Count;

    void Awake()
    {
        DeckCards = new List<CardData>(deckData.DeckCards);
    }
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            DrawCard();   
        }
    }

    public void AddCard(CardData cardData)
    {
        DeckCards.Add(cardData);
    }
    
    public void DrawCard()
    {
        if (CountCardsInDeck == 0)
            return;

        int random = Random.Range(0, CountCardsInDeck);
        InstantiateCard(DeckCards[random]);
        DeckCards.RemoveAt(random);
    }
    public void InstantiateCard(CardData cardData)
    {
        switch (cardData)
        {
            case SpecialCardData specialCardData:
                var specialCard = Instantiate(Special, Hand.transform);
                specialCard.CardData = specialCardData;
                break;
            case UnitCardData unitCardData when unitCardData.UnitType == UnitType.Silver:
                var silverCard = Instantiate(Silver, Hand.transform);
                silverCard.UnitCardData = unitCardData;
                break;
            case UnitCardData unitCardData when unitCardData.UnitType == UnitType.Golden:
                var goldenCard = Instantiate(Golden, Hand.transform);
                goldenCard.UnitCardData = unitCardData;
                break;
            default:
                Debug.LogError("Tipo no soportado");
                break;
        }
    }
}