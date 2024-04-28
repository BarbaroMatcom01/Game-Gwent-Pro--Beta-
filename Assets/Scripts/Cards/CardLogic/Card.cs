using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image Image;
    public string Name { get; private set; }
    public CardType CardType { get; private set; }
    public SpecificTypeCard SpecificTypeCard {get; private set;}
    public string Faction { get; private set; }
    public string Description { get; private set; }
    public Sprite CardImage { get; private set; }
     public UnitType UnitType;

    public void SetCardData(CardData cardData)
    {
        Name = cardData.Name;
        CardType = cardData.CardType;
        SpecificTypeCard = cardData.SpecificTypeCard;
        Faction = cardData.Faction;
        Description = cardData.Description;
        CardImage = cardData.CardImage;
    }

}
