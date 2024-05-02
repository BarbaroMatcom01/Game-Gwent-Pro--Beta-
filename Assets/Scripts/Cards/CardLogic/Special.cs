using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : Card
{
    public SpecialCardData CardData;
    public SpecialType SpecialType;
    public Image TypeIcon;

    void Start()
    {
        this.gameObject.name = CardData.name;
        Image.sprite = CardData.CardImage;
        TypeIcon.sprite = CardData.TypeIcon;
        SpecialType = CardData.SpecialType;
    }


}
