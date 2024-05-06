using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public Battlefield[] Battlefields = new Battlefield[2];
    public GameObject[] Graveyard = new GameObject[2];
    public Weathers Weathers;
    public EffectManager effectManager;
    public List<Card>[] SummonedCards = new List<Card>[2];

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

        SummonedCards[0] = new();
        SummonedCards[1] = new();
    }

    public void InvokeCard(Card card)
    {
        SummonedCards[(int)GameManager.Instance.CurrentPlayer].Add(card);
        int numberCurrentPlayer = (int)GameManager.Instance.CurrentPlayer;
        float moveDuration = 0.7f;
        Transform newPosition;
        switch (card)
        {
            case Unit unit when unit.AttackType == AttackType.Melee:
                Battlefields[numberCurrentPlayer].MeleeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].MeleeRow.UnitCardsGrid.transform;
                effectManager.ActivateUnitEffect(unit);
                Debug.Log("MeleeUnit");
                break;
            case Unit unit when unit.AttackType == AttackType.Ranged:
                Battlefields[numberCurrentPlayer].RangedRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].RangedRow.UnitCardsGrid.transform;
                effectManager.ActivateUnitEffect(unit);
                Debug.Log("RangedUnit");
                break;
            case Unit unit when unit.AttackType == AttackType.Siege:
                Battlefields[numberCurrentPlayer].SiegeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].SiegeRow.UnitCardsGrid.transform;
                effectManager.ActivateUnitEffect(unit);
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
                newPosition = Weathers.weathers[0].transform;
                Debug.Log("Rain");
                break;
            case Special special when special.SpecialType == SpecialType.Storm:
                Weathers.ActivateStorm();
                newPosition = Weathers.weathers[1].transform;
                Debug.Log("Storm");
                break;
            case Special special when special.SpecialType == SpecialType.Snow:
                Weathers.ActivateSnow();
                newPosition = Weathers.weathers[2].transform;
                Debug.Log("Snow");
                break;
            case Special special when special.SpecialType == SpecialType.Clearing:
                Weathers.ActivateClearing();
                newPosition = null;
                CardManager.Instance.SendToGraveyard(special, Graveyard[(int)GameManager.Instance.CurrentPlayer]);
                for (int i = 0; i < Weathers.weathers.Length; i++)
                {
                    for (int j = 0; j < Weathers.weathers[i].transform.childCount; j++)
                    {

                        Destroy(Weathers.weathers[i].transform.GetChild(j).gameObject);
                    }
                }
                Debug.Log("Clearing");
                break;
            case Special special when special.SpecialType == SpecialType.Decoy:
                newPosition = null;
                break;
            default:
                Debug.Log("No Valid Type Detected");
                newPosition = null;
                return;
        }
        if (newPosition is not null)
        {
            LeanTween.move(card.gameObject, newPosition, moveDuration)
            .setOnComplete(() => card.transform.SetParent(newPosition));
        }

        GameManager.Instance.ChangeTurn();
    }

    public void SendToGraveyard(Card card, GameObject graveryard)
    {
        float moveDuration = 0.7f;

        LeanTween.move(card.gameObject, graveryard.transform, moveDuration)
          .setOnComplete(() =>card.transform.SetParent(graveryard.transform));
    }
    public void DestroyCard(Board board)
    {
        for (int i = 0; i < 3; i++)
        {
            board.PlayerOneSide.Battlefield.PlayerBattlefield[i].RemoveAllUnitCards();
            board.PlayerTwoSide.Battlefield.PlayerBattlefield[i].RemoveAllUnitCards();
            board.PlayerOneSide.Battlefield.PlayerBattlefield[i].DeactivateIncrease();
            board.PlayerTwoSide.Battlefield.PlayerBattlefield[i].DeactivateIncrease();
            board.Weathers.ActivateClearing();
        }
        for (int i = 0; i < SummonedCards[0].Count; i++)
        {
            SendToGraveyard(SummonedCards[0][i],Graveyard[0]);
        }
        for (int i = 0; i < SummonedCards[1].Count; i++)
        {
            SendToGraveyard(SummonedCards[1][i],Graveyard[1]);
        }
        SummonedCards[0].Clear();
        SummonedCards[1].Clear();

    }
}
