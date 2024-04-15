using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Special : Card
{
    public SpecialCardData CardData;
    public Image TypeIcon;
   /*
     El método Start se utiliza para inicializar la carta especial, estableciendo las imágenes de la carta y del tipo de ícono.
   */
    void Start()
    {
        Image.sprite = CardData.CardImage;
        TypeIcon.sprite = CardData.TypeIcon;
    }
}
