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
    public CardData CardData;
    protected bool CardIsInHand = true;

    void Start()
    {
        this.gameObject.name = CardData.name;
        Name = CardData.Name;
        Faction = CardData.Faction;
        Description = CardData.Description;
        Image.sprite = CardData.CardImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardIsInHand && GameManager.Instance.CurrentState == GameState.Turn)
        {
            CardManager.Instance.InvokeCard(this);
            CardIsInHand = false;
        }
        else if (GameManager.Instance.CurrentState == GameState.ChangeCards)
        {
            ChangeCards.Instance.ChangeCard(this);
            CardPreview.Instance.DestroyClone();
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
