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
    [SerializeField] private Silver Silver;
    [SerializeField] private Golden Golden;
    [SerializeField] private Special Special;
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
        card.Owner = (int)GameManager.Instance.CurrentPlayer;
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

    public void SendToHand(Card card, GameObject hand)
    {
        float moveDuration = 0.7f;

        LeanTween.move(card.gameObject, hand.transform, moveDuration)
        .setOnComplete(() => card.transform.SetParent(hand.transform));
    }

    public void DestroyCards(Board board)
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

    bool AreListsEqual<T>(List<T> list1, List<T> list2)
    {
        if (list1 == null || list2 == null)
            return false;

        return list1.Count == list2.Count && list1.SequenceEqual(list2);
    }
   
    public void PushCardList(List<Card> cardList, object card)
    {
        List<Card> fieldPlayerOne = GameManager.Instance.FieldOfPlayer(0);
        List<Card> fieldPlayerTwo = GameManager.Instance.FieldOfPlayer(1);
        List<Card> handPlayerOne = GameManager.Instance.HandOfPlayer(0);
        List<Card> handPlayerTwo = GameManager.Instance.HandOfPlayer(1);

        if (card is CardData)
        {
            CardData cardData = (CardData)card;
            if (AreListsEqual(cardList, handPlayerOne))
            {
                board.PlayerOneSide.Deck.InstantiateCard(cardData);
            }
            else if (AreListsEqual(cardList, handPlayerTwo))
            {
                board.PlayerTwoSide.Deck.InstantiateCard(cardData);
            }
        }
        else
        {
            Card cardPush = (Card)card;
            Debug.Log(cardPush.Name.ToString() + $"Remove card list ");

            if (AreListsEqual(cardList, handPlayerOne))
            {
               board.PlayerOneSide.Deck.AddCardToPlayerHand(cardPush,cardList,0);
            }
            else if (AreListsEqual(cardList, handPlayerTwo))
            {
                board.PlayerTwoSide.Deck.AddCardToPlayerHand(cardPush,cardList,1);
            }
            else if (AreListsEqual(cardList, fieldPlayerOne))
            {
                InvokeCardPush(cardPush, 0);
            }
            else if (AreListsEqual(cardList, fieldPlayerTwo))
            {
                InvokeCardPush(cardPush, 1);
            }
        }
    }
    public void InvokeCardPush(Card card, int player)
    {

        InvokedCards[player].Add(card);
        card.Owner = player;
        float moveDuration = 0.7f;
        Transform newPosition;

        switch (card)
        {
            case Unit unit when unit.AttackType == AttackType.Melee:
                Battlefields[player].MeleeRow.AddUnitCard(unit);
                newPosition = Battlefields[player].MeleeRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
                Debug.Log("MeleeUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Ranged:
                Battlefields[player].RangedRow.AddUnitCard(unit);
                newPosition = Battlefields[player].RangedRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
                Debug.Log("RangedUnit");
                break;

            case Unit unit when unit.AttackType == AttackType.Siege:
                Battlefields[player].SiegeRow.AddUnitCard(unit);
                newPosition = Battlefields[player].SiegeRow.CardsGrid.transform;
                EffectManager.ActivateUnitEffect(unit);
                Debug.Log("SiegeUnit");
                break;

            case Special special when special.SpecialType == SpecialType.MeleeIncrease:
                Battlefields[player].MeleeRow.ActivateIncrease();
                newPosition = Battlefields[player].MeleeRow.IncreaseSlot.transform;
                Debug.Log("IncreaseMelee");
                break;

            case Special special when special.SpecialType == SpecialType.RangedIncrease:
                Battlefields[player].RangedRow.ActivateIncrease();
                newPosition = Battlefields[player].RangedRow.IncreaseSlot.transform;
                Debug.Log("IncreaseRanged");
                break;

            case Special special when special.SpecialType == SpecialType.SiegeIncrease:
                Battlefields[player].SiegeRow.ActivateIncrease();
                newPosition = Battlefields[player].SiegeRow.IncreaseSlot.transform;
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
                newPosition = Graveyard[player].transform;
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
    }
    public void RemoveCardList(List<Card> cardList, object card)
    {
        Card cardRemove = (Card)card;

        Debug.Log(cardRemove.Name.ToString() + $"Remove card list ");

        List<Card> boardCards = GameManager.Instance.BoardCards();
        List<Card> fieldPlayerOne = GameManager.Instance.FieldOfPlayer(0);
        List<Card> fieldPlayerTwo = GameManager.Instance.FieldOfPlayer(1);
        List<Card> handPlayerOne = GameManager.Instance.HandOfPlayer(0);
        List<Card> handPlayerTwo = GameManager.Instance.HandOfPlayer(1);

        if (AreListsEqual(cardList, boardCards))
        {
            RemoveCardBoar(card);
        }
        else if (AreListsEqual(cardList, fieldPlayerOne))
        {
            RemoveCardFieldPlayerOne(card);
        }
        else if (AreListsEqual(cardList, fieldPlayerTwo))
        {
            RemoveCardFieldPlayerTwo(card);
        }
        else if (AreListsEqual(cardList, handPlayerOne))
        {
            RemoveCardHandPlayerOne(card);
            Debug.Log(cardRemove.Name.ToString() + $"Remove card de la mano jugador uno ");
        }
        else if (AreListsEqual(cardList, handPlayerTwo))
        {
            Debug.Log(cardRemove.Name.ToString() + $"Remove card de la mano de jugador dos ");
            RemoveCardHandPlayerTwo(card);
        }
        else
        {
            Debug.Log($"La carta no se encuentra en el juego ");
        }
    }
    public void RemoveCardBoar(object card)
    {
        Card cardRemove = (Card)card;
        if (GameManager.Instance.FieldOfPlayer(0).Contains(cardRemove))
        {
            RemoveCardFieldPlayerOne(card);
        }
        else if (GameManager.Instance.FieldOfPlayer(1).Contains(cardRemove))
        {
            RemoveCardFieldPlayerTwo(card);
        }
        else
        {
            Debug.Log($"La carta no se encuentra en el tablero");
        }
    }
    public void RemoveCardFieldPlayerOne(object card)
    {
        Card cardRemove = (Card)card;
        if (InvokedCards[0].Contains(cardRemove))
            InvokedCards[0].Remove(cardRemove);

        foreach (var row in Battlefields[0].PlayerBattlefield)
        {
            Unit unitToRemove = row.UnitCards.FirstOrDefault(u => u.IdCard == cardRemove.IdCard);
            if (unitToRemove != null)
            {
                row.RemoveUnitCard(unitToRemove);
                break;
            }
        }
        Destroy(cardRemove.gameObject);
    }
    public void RemoveCardFieldPlayerTwo(object card)
    {
        Card cardRemove = (Card)card;
        if (InvokedCards[1].Contains(cardRemove))
            InvokedCards[1].Remove(cardRemove);

        foreach (var row in Battlefields[1].PlayerBattlefield)
        {
            Unit unitToRemove = row.UnitCards.FirstOrDefault(u => u.IdCard == cardRemove.IdCard);
            if (unitToRemove != null)
            {
                row.RemoveUnitCard(unitToRemove);
                break;
            }
        }
        Destroy(cardRemove.gameObject);
    }
    public void RemoveCardHandPlayerOne(object card)
    {
        Card cardRemove = (Card)card;

        List<Card> cardsInHand = Hands[0].GetComponentsInChildren<Card>().ToList();
        if (cardsInHand.Contains(cardRemove))
        {
            foreach (Card car in cardsInHand)
            {
                Card cardToRemove = cardsInHand.FirstOrDefault(u => u.IdCard == cardRemove.IdCard);
                if (cardToRemove != null)
                {
                    cardsInHand.Remove(cardRemove);
                    break;
                }
            }
            Destroy(cardRemove.gameObject);
        }
    }
    public void RemoveCardHandPlayerTwo(object card)
    {
        Card cardRemove = (Card)card;
        List<Card> cardsInHand = Hands[1].GetComponentsInChildren<Card>().ToList();
        if (cardsInHand.Contains(cardRemove))
        {
            foreach (Card cardInHand in cardsInHand)
            {
                Card cardToRemove = cardsInHand.FirstOrDefault(u => u.IdCard == cardRemove.IdCard);
                if (cardToRemove != null)
                {
                    cardsInHand.Remove(cardRemove);
                    break;
                }
            }
            Destroy(cardRemove.gameObject);
        }
    }
}