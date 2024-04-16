using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : Card
{
    public SpecialCardData CardData;
    public Image TypeIcon;
    
    void Start()
    {
        Image.sprite = CardData.CardImage;
        TypeIcon.sprite = CardData.TypeIcon;
    }
}
