using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  // LeenTween.move(gameObject, newPosition, Duration);

public class CardManager : MonoBehaviour
{
    public Battlefield Battlefield;
    public Weathers Weathers;
    public Unit Unit;
    public void InvokeCard(Card card)
    {
        switch (card.SpecificTypeCard)
        {
            case SpecificTypeCard.MeleeUnit:
                Battlefield.MeleeRow.AddUnitCard(Unit);
                break;
            case SpecificTypeCard.RangedUnit:
                Battlefield.RangedRow.AddUnitCard(Unit);
                break;
            case SpecificTypeCard.SiegeUnit:
                Battlefield.SiegeRow.AddUnitCard(Unit);
                break;
            case SpecificTypeCard.IncreaseMelee:
                Battlefield.MeleeRow.ActivateIncrease();
                break;
            case SpecificTypeCard.IncreaseRanged:
                Battlefield.RangedRow.ActivateIncrease();
                break;
            case SpecificTypeCard.IncreaseSiege:
                Battlefield.SiegeRow.ActivateIncrease();
                break;
            case SpecificTypeCard.Rain:
                Weathers.ActivateRain();
                break;
            case SpecificTypeCard.Storm:
                Weathers.ActivateStorm();
                break;
            case SpecificTypeCard.Snow:
                Weathers.ActivateSnow();
                break; 
            case SpecificTypeCard.Clearing:
                Weathers.ActivateClearing();
                break;
            case SpecificTypeCard.Decoy:

                break;  
        }

    }
}
