using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public string Name { get; protected set; }
    public string Faction { get; protected set; }
    public string Description { get; protected set; }
    public Sprite CardImage { get; private set; }
    public Image Image;
    protected bool CardIsInHand = true;
    public Deck[] Decks = new Deck[2];

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardIsInHand && GameManager.Instance.CurrentState == GameState.Turn)
        {
            CardManager.Instance.InvokeCard(this);
            CardIsInHand = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        CardPreview.Instance.ShowCardInfo(this);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, Vector3.one, 0.2f);
        CardPreview.Instance.DestroyClone();
    }
}
