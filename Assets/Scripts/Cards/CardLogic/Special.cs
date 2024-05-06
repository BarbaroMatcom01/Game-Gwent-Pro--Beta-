using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Special : Card 
{
    public SpecialCardData CardData;
    public SpecialType SpecialType;
    public Image TypeIcon;

    void Start()
    {
        this.gameObject.name = CardData.name;
        Name = CardData.Name;
        Faction = CardData.Faction;
        Description = CardData.Description;
        Image.sprite = CardData.CardImage;
        TypeIcon.sprite = CardData.TypeIcon;
        SpecialType = CardData.SpecialType;
    }
   

}
