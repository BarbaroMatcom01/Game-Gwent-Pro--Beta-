using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    public Board board;
    Player currentPlayer => GameManager.Instance.CurrentPlayer;
    public Weathers Weathers;
    public Battlefield[] Battlefields = new Battlefield[2];
    public GameObject[] Graveyard = new GameObject[2];
    public GameObject[] Hands = new GameObject[2];
    public List<Card>[] InvokedCards = new List<Card>[2];
    public EffectManager EffectManager;
    private Special Decoy;
    public static CardManager Instance;

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
        InvokedCards[0] = new();
        InvokedCards[1] = new();
    }

    public void InvokeCard(Card card)
    {
        InvokedCards[(int)GameManager.Instance.CurrentPlayer].Add(card);
        int numberCurrentPlayer = (int)GameManager.Instance.CurrentPlayer;
        float moveDuration = 0.7f;
        Transform newPosition;

        switch (card)
        {
            case Unit unit when unit.AttackType == AttackType.Melee:
                Battlefields[numberCurrentPlayer].MeleeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].MeleeRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
                Debug.Log("MeleeUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Ranged:
                Battlefields[numberCurrentPlayer].RangedRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].RangedRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
                Debug.Log("RangedUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Siege:
                Battlefields[numberCurrentPlayer].SiegeRow.AddUnitCard(unit);
                newPosition = Battlefields[numberCurrentPlayer].SiegeRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
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
                newPosition = Graveyard[(int)GameManager.Instance.CurrentPlayer].transform;
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
                GameManager.Instance.ChangeState(GameState.DecoyState);
                newPosition = null;
                Decoy = special;
                break;

            case Leader leader:
                newPosition = null;
                InvokedCards[(int)GameManager.Instance.CurrentPlayer].Remove(leader);
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

            GameManager.Instance.ChangeTurn();
        }
    }

    public void SendToGraveyard(Card card, GameObject graveryard)
    {
        float moveDuration = 0.7f;

        LeanTween.move(card.gameObject, graveryard.transform, moveDuration)
        .setOnComplete(() => card.transform.SetParent(graveryard.transform));
    }

    public void SendToGraveyardLeastPopulatedRow(Row row, GameObject graveryard)
    {
        float moveDuration = 0.7f;
       
        foreach (Unit unit in row.UnitCards)
        {
            LeanTween.move(unit.gameObject, graveryard.transform, moveDuration)
            .setOnComplete(() => unit.transform.SetParent(graveryard.transform));
        }
    }

    public void SendToHand(Silver card, GameObject hand)
    {
        float moveDuration = 0.7f;
      
        LeanTween.move(card.gameObject, hand.transform, moveDuration)
        .setOnComplete(() => card.transform.SetParent(hand.transform));
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
        
        for (int i = 0; i < InvokedCards[0].Count; i++)
        {
            SendToGraveyard(InvokedCards[0][i], Graveyard[0]);
        }
        
        for (int i = 0; i < InvokedCards[1].Count; i++)
        {
            SendToGraveyard(InvokedCards[1][i], Graveyard[1]);
        }

        InvokedCards[0].Clear();
        InvokedCards[1].Clear();
    }

    public void InvokeDecoy(Silver card)
    {
        Debug.Log(card.name);
        if (!(InvokedCards[(int)currentPlayer].Contains(card)
        && InvokedCards[(int)currentPlayer].Any(card => card is Silver)))
        {
            Decoy.ReturnDecoyToHand();
            GameManager.Instance.ChangeState(GameState.Turn);
            return;
        }
       
        card.ReturnToHand();

        if (GameManager.Instance.CurrentPlayer == Player.Player_One)
        {
            int rowAttackType = (int)card.AttackType;
            board.PlayerOneSide.Battlefield.PlayerBattlefield[rowAttackType].RemoveUnitCard(card);
            InvokedCards[0].Remove(card);
            SendToHand(card, Hands[0]);
            var newPosition = board.PlayerOneSide.Battlefield.PlayerBattlefield[rowAttackType].CardsGrid;
           
            LeanTween.move(Decoy.gameObject, newPosition.transform, 0.7f)
           .setOnComplete(() => Decoy.transform.SetParent(newPosition.transform));
        }
        else
        {
            int rowAttackType = (int)card.AttackType;
            board.PlayerTwoSide.Battlefield.PlayerBattlefield[rowAttackType].RemoveUnitCard(card);
            InvokedCards[1].Remove(card);
            SendToHand(card, Hands[1]);
            var newPosition = board.PlayerTwoSide.Battlefield.PlayerBattlefield[rowAttackType].CardsGrid;
           
            LeanTween.move(Decoy.gameObject, newPosition.transform, 0.7f)
           .setOnComplete(() => Decoy.transform.SetParent(newPosition.transform));
        }

        LeanTween.delayedCall(0.7f, () => GameManager.Instance.ChangeTurn());
        LeanTween.delayedCall(0.7f, () => GameManager.Instance.ChangeState(GameState.Turn));
    }

}