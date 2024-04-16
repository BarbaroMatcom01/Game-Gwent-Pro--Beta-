using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] DeckData deckData;
    public List<CardData> DeckCards = new List<CardData>();

    public int CountCardsInDeck()
    {
        return DeckCards.Count;
    }

    public CardData DrawCard()
    {
        if (CountCardsInDeck() == 0)
        {
            return null;
        }

        int random = Random.Range(0, CountCardsInDeck());
        CardData drawCard = DeckCards[random];
        DeckCards.RemoveAt(random);
        return drawCard;
    }
}
 