using UnityEngine.UI;


public class Special : Card 
{
    public new SpecialCardData CardData;
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
   
       public void ReturnDecoyToHand()
        {
            CardIsInHand = true;
        }
}
