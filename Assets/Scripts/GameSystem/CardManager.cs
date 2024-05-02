using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public Battlefield[] Battlefields = new Battlefield[2];
    public Weathers Weathers;
    public EffectManager EffectManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InvokeCard(Card card)
    {
        int numberCurrentPlayer = (int)GameManager.CurrentPlayer;
        float moveDuration = 0.7f;
        Transform newPosition;
        
        switch (card)
        {
            case Unit unit when unit.AttackType == AttackType.Melee:
               
                Battlefields[numberCurrentPlayer].MeleeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].MeleeRow.UnitCardsGrid.transform;
                Debug.Log("MeleeUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Ranged:
                Battlefields[numberCurrentPlayer].RangedRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].RangedRow.UnitCardsGrid.transform;
                Debug.Log("RangedUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Siege:
                Battlefields[numberCurrentPlayer].SiegeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].SiegeRow.UnitCardsGrid.transform;
                Debug.Log("SiegeUnit");
                break;

            case Special special when special.SpecialType == SpecialType.MeleeIncrease:
                Battlefields[numberCurrentPlayer].MeleeRow.ActivateIncrease();
                newPosition = Battlefields[numberCurrentPlayer].MeleeRow.IncreaseSlot.transform;
                Debug.Log("IncreaseMelee");
                break;

            case Special special when special.SpecialType == SpecialType.RangedIncrease:
                Battlefields[numberCurrentPlayer].RangedRow.ActivateIncrease();
                newPosition = Battlefields[numberCurrentPlayer].RangedRow.IncreaseSlot.transform;
                Debug.Log("IncreaseRanged");
                break;

            case Special special when special.SpecialType == SpecialType.SiegeIncrease:
                Battlefields[numberCurrentPlayer].SiegeRow.ActivateIncrease();
                newPosition = Battlefields[numberCurrentPlayer].SiegeRow.IncreaseSlot.transform;
                Debug.Log("IncreaseSiege");
                break;

            case Special special when special.SpecialType == SpecialType.Rain:
                Weathers.ActivateRain();
                newPosition = Weathers.transform;
                Debug.Log("Rain");
                break;

            case Special special when special.SpecialType == SpecialType.Snow:
                Weathers.ActivateSnow();
                newPosition = Weathers.transform;
                Debug.Log("Snow");
                break;

            case Special special when special.SpecialType == SpecialType.Storm:
                Weathers.ActivateStorm();
                newPosition = Weathers.transform;
                Debug.Log("Storm");
                break;

            case Special special when special.SpecialType == SpecialType.Clearing:
                
                Weathers.ActivateClearing();
                newPosition = Weathers.transform;
                break;
            case Special special when special.SpecialType == SpecialType.Decoy:
                newPosition = null;
                break;

            default:
                Debug.Log("No Valid Type Detected");
                newPosition = null;
                break;
        }
        if (newPosition is not null)
            LeanTween.move(card.gameObject, newPosition, moveDuration)
            .setOnComplete(()=>
            card.transform.SetParent(newPosition));
    }


}
