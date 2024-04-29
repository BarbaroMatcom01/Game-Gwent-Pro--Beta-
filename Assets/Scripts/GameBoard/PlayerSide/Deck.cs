using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    public List<Card> DeckCards = new List<Card>();
    [SerializeField] private Silver Silver;
    [SerializeField] private Golden Golden;
    [SerializeField] private Special Special;

    public int CountCardsInDeck => DeckCards.Count;
    public Card DrawCard()
    {
        if (CountCardsInDeck == 0)
        {
            return null;
        }

        int random = Random.Range(0, CountCardsInDeck);
        Card drawnCard = DeckCards[random];
        DeckCards.RemoveAt(random);
        return drawnCard;
    }

    public Card InstantiateCard(CardData cardData)
    {
        Card drawnCard = null;
        switch (cardData.CardType)
        {
            case CardType.Special:
                drawnCard = Instantiate(Special, this.transform);
                break;
            case CardType.Unit:
                if (cardData.Faction == "Silver")
                {
                    drawnCard = Instantiate(Silver, this.transform);
                }
                else if (cardData.Faction == "Golden")
                {
                    drawnCard = Instantiate(Golden, this.transform);
                }
                break;
            default:
                Debug.LogError("Unsupported card type.");
                break;
        }

        if (drawnCard != null)
        {
            drawnCard.SetCardData(cardData);
        }

        return drawnCard;
    }
}