using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    public CardData CardData;
    public UnitCardData UnitCardData;
    public SpecialCardData SpecialCardData;
    public Battlefield battlefield;
    public Unit unit;
    public void InvocarCard(Card card)
    {
        switch (card.CardType)
        {
            case CardType.Unit:
                string attackType = UnitCardData.AttackTypes[0].ToString();
                switch (attackType)
                {
                    case "Melee":
                        battlefield.MeeleRow.AddUnitCard(unit);
                        break;
                    case "Ranged":
                        battlefield.RangedRow.AddUnitCard(unit);
                        break;
                    case "Siege":
                        battlefield.SiegeRow.AddUnitCard(unit);
                        break;
                }
                break;
            case CardType.Special:
                string specialType = SpecialCardData.SpecialType.ToString();
                switch(specialType)
                {
                    case "Rain":

                    break;

                    case "Storm":

                    break;

                    case "Snow":

                    break;
                    

                }
                
            break;
        }



    }
}
