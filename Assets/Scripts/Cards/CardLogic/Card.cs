using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public string Name { get; private set; }
    public string Faction { get; private set; }
    public string Description { get; private set; }
    public Sprite CardImage { get; private set; }
    public Image Image;

    public void OnPointerClick(PointerEventData eventData)
    {
        CardManager.Instance.InvokeCard(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject,new Vector3(1.2f,1.2f,1.2f),0.2f );
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject,Vector3.one, 0.2f );
    }

    public void SetCardData(CardData cardData)
    {
        Name = cardData.Name;
        Faction = cardData.Faction;
        Description = cardData.Description;
        CardImage = cardData.CardImage;
    }

}
