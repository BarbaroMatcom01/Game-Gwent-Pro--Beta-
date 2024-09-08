using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    public List<CardData> DeckCards = new List<CardData>();
    [SerializeField] private GameObject Hand;
    [SerializeField] private Silver Silver;
    [SerializeField] private Golden Golden;
    [SerializeField] private Special Special;
    public int CountCardsInDeck => DeckCards.Count;
    public static Deck Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DeckCards = new List<CardData>(deckData.DeckCards);
    }

    public void AddCardData(CardData cardData)
    {
        DeckCards.Add(cardData);
    }

    public void AddCard(Card card)
    {
        CardData cardData = null;

        if (card is Special specialCard)
        {
            cardData = specialCard.CardData;
        }
        else if (card is Unit unitCard)
        {
            cardData = unitCard.UnitCardData;
        }
        if (cardData != null)
        {
            DeckCards.Add(cardData);
        }
        else
        {
            Debug.LogError("Card type not recognized or CardData is null.");
        }
    }
    public void DrawCard()
    {
        if (CountCardsInDeck == 0) return;

        int random = Random.Range(0, CountCardsInDeck);
        InstantiateCard(DeckCards[random]);
        DeckCards.RemoveAt(random);
    }

    public void InstantiateCard(CardData cardData)
    {
        switch (cardData)
        {
            case SpecialCardData specialCardData:
                cardData.Owner = (int)GameManager.Instance.CurrentPlayer;
                var specialCard = Instantiate(Special, Hand.transform);
                specialCard.CardData = specialCardData;
                break;
            case UnitCardData unitCardData when unitCardData.UnitType == UnitType.Silver:
                cardData.Owner = (int)GameManager.Instance.CurrentPlayer;
                var silverCard = Instantiate(Silver, Hand.transform);
                silverCard.UnitCardData = unitCardData;
                break;
            case UnitCardData unitCardData when unitCardData.UnitType == UnitType.Golden:
                cardData.Owner = (int)GameManager.Instance.CurrentPlayer;
                var goldenCard = Instantiate(Golden, Hand.transform);
                goldenCard.UnitCardData = unitCardData;
                break;
            default:
                Debug.LogError("No Type");
                break;
        }
    }
    public void AddCardToPlayerHand(Card card, List<Card> playerHand, int player)
    {
        switch (card)
        {
            case Special special:
                var specialCard = Instantiate(Special, Hand.transform);
                specialCard.CardData = special.CardData;
                break;
            case Unit unitCard when unitCard.UnitType == UnitType.Silver:
                Silver silver =(Silver)unitCard;
                var silverCard = Instantiate(silver, Hand.transform);
                silverCard.UnitCardData =silver.UnitCardData;
                break;
            case Unit unitCard when unitCard.UnitType == UnitType.Golden:
                var goldenCard = Instantiate(Golden, Hand.transform);
                goldenCard.UnitCardData = (UnitCardData)unitCard.CardData;
                break;
            default:
                Debug.LogError("No Type");
                break;
        }
        card.Owner = player;
        playerHand.Add(card);
    }
}