using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Board Board;
    public Battlefield[] Battlefields = new Battlefield[2];
    public GameObject[] Graveyard = new GameObject[2];

    public void ActivateUnitEffect(Unit unit)
    {
        switch (unit.UnitCardData.Skill)
        {
            case Skills.Draw:
                Draw();
                break;
            case Skills.MultiplyPower:
                MultiplyPower(unit);
                break;
            case Skills.IncreaseRowPower:
                IncreaseRowPower(unit);
                break;
            case Skills.SetWeather:
                SetWeather(unit);
                break;
            case Skills.ClearStrongestUnit:
                ClearStrongestUnit(Board);
                break;
            case Skills.ClearLessStrongUnit:
                ClearLessStrongUnit(Board);
                break;
            case Skills.ClearLeastPopulatedRow:
                ClearLeastPopulatedRow(Board);
                break;
            case Skills.SetAveragePower:
                SetAveragePower(Board);
                break;
        }
    }

    public void ActivateLeaderEffect(Leader leader)
    {
        if (leader.IsUsableLeader)
        {
            switch (leader.Skills)
            {
                case Skills.Draw:
                    Draw();
                    break;
            }
            GameManager.Instance.ChangeTurn();
            leader.IsUsableLeader = false;
        }
    }

    public void Draw()
    {
        if (GameManager.Instance.CurrentPlayer == Player.Player_One)
        {
            Board.PlayerOneSide.Deck.DrawCard();
        }
        else
        {
            Board.PlayerTwoSide.Deck.DrawCard();
        }
    }

    public void MultiplyPower(Unit unit)
    {
        int cardAppearancesInBoard = Board.PlayerOneSide.Battlefield.CardAppearances(unit) + Board.PlayerTwoSide.Battlefield.CardAppearances(unit);
        unit.Power = unit.Power * cardAppearancesInBoard;
    }

    public void SetAveragePower(Board board)
    {
        int battlefieldPowerPlayerOne = board.PlayerOneSide.Battlefield.BattlefieldPower();
        int battlefieldPowerPlayerTwo = board.PlayerTwoSide.Battlefield.BattlefieldPower();
        int numberOfCardsOnTheBattlefieldPlayerOne = board.PlayerOneSide.Battlefield.NumberOfCardsOnTheBattlefield();
        int numberOfCardsOnTheBattlefieldPlayerTwo = board.PlayerTwoSide.Battlefield.NumberOfCardsOnTheBattlefield();
        Row[] battlefieldPlayerOne = board.PlayerOneSide.Battlefield.PlayerBattlefield;
        Row[] battlefieldPlayerTwo = board.PlayerTwoSide.Battlefield.PlayerBattlefield;

        int averagePower = (battlefieldPowerPlayerOne + battlefieldPowerPlayerTwo) / (numberOfCardsOnTheBattlefieldPlayerOne + numberOfCardsOnTheBattlefieldPlayerTwo);

        foreach (Row row in battlefieldPlayerOne)
        {
            foreach (Unit card in row.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power = averagePower;
                }
            }
        }
        foreach (Row row in battlefieldPlayerTwo)
        {
            foreach (Unit card in row.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power = averagePower;
                }
            }
        }
    }

    public void IncreaseRowPower(Unit unit)
    {
        if (GameManager.Instance.CurrentPlayer == Player.Player_One)
        {
            int getPositionCard = Battlefields[0].GetPositionUnit(unit);
            Row increaseRow = Battlefields[0].PlayerBattlefield[getPositionCard];
            foreach (Unit card in increaseRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power += 2;
                }
            }
        }
        else
        {
            int getPositionCard = Battlefields[1].GetPositionUnit(unit);
            Row increaseRow = Battlefields[1].PlayerBattlefield[getPositionCard];
            foreach (Unit card in increaseRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power += 2;
                }
            }
        }
    }

    public void SetWeather(Unit unit)
    {
        if (GameManager.Instance.CurrentPlayer == Player.Player_One)
        {
            int getPositionCard = Battlefields[0].GetPositionUnit(unit);
            Row weatherRow = Battlefields[0].PlayerBattlefield[getPositionCard];
            foreach (Unit card in weatherRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power -= 2;
                }
            }
        }
        else
        {
           int getPositionCard = Battlefields[1].GetPositionUnit(unit);
            Row weatherRow = Battlefields[1].PlayerBattlefield[getPositionCard];
            foreach (Unit card in weatherRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power -= 2;
                }
            }
        }
    }

    public void ClearStrongestUnit(Board board)
    {
        Unit strongestUnitPlayerOne = board.PlayerOneSide.Battlefield.GetStrongestUnitSilver();
        Unit strongestUnitPlayerTwo = board.PlayerTwoSide.Battlefield.GetStrongestUnitSilver();
        Battlefield battlefieldPlayerOne = board.PlayerOneSide.Battlefield;
        Battlefield battlefieldPlayerTwo = board.PlayerTwoSide.Battlefield;
        int positionUnitPlayerOne = battlefieldPlayerOne.GetPositionStrongestUnit();
        int positionUnitPlayerTwo = battlefieldPlayerTwo.GetPositionStrongestUnit();
       
        if ((strongestUnitPlayerOne != null) && (strongestUnitPlayerTwo != null))
        {
            if (strongestUnitPlayerOne.Power > strongestUnitPlayerTwo.Power)
            {
                CardManager.Instance.SendToGraveyard(strongestUnitPlayerOne, Graveyard[0]);
                battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
            }
            else if (strongestUnitPlayerOne.Power < strongestUnitPlayerTwo.Power)
            {
                CardManager.Instance.SendToGraveyard(strongestUnitPlayerTwo, Graveyard[1]);
                battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
            }
            else
            {
                CardManager.Instance.SendToGraveyard(strongestUnitPlayerOne, Graveyard[0]);
                CardManager.Instance.SendToGraveyard(strongestUnitPlayerTwo, Graveyard[1]);
                battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
                battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
            }
        }
        else if ((strongestUnitPlayerOne != null) && (strongestUnitPlayerTwo == null))
        {
            CardManager.Instance.SendToGraveyard(strongestUnitPlayerOne, Graveyard[0]);
            battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
        }
        else if ((strongestUnitPlayerOne == null) && (strongestUnitPlayerTwo != null))
        {
            CardManager.Instance.SendToGraveyard(strongestUnitPlayerTwo, Graveyard[1]);
            battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
        }
    }

    public void ClearLessStrongUnit(Board board)
    {
        Unit lessStrongUnitPlayerOne = board.PlayerOneSide.Battlefield.GetLessStrongUnitSilver();
        Unit lessStrongUnitPlayerTwo = board.PlayerTwoSide.Battlefield.GetLessStrongUnitSilver();
        Battlefield battlefieldPlayerOne = board.PlayerOneSide.Battlefield;
        Battlefield battlefieldPlayerTwo = board.PlayerTwoSide.Battlefield;
        int positionUnitPlayerOne = battlefieldPlayerOne.GetPositionLessStrongUnitSilver();
        int positionUnitPlayerTwo = battlefieldPlayerTwo.GetPositionLessStrongUnitSilver();

        if (GameManager.Instance.CurrentPlayer == Player.Player_One)
        {
            if (lessStrongUnitPlayerTwo != null)
            {
                battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(lessStrongUnitPlayerTwo);
                CardManager.Instance.SendToGraveyard(lessStrongUnitPlayerTwo, Graveyard[1]);
            }
            else if (lessStrongUnitPlayerTwo == null) { }
        }
        else
        {
            if (lessStrongUnitPlayerOne != null)
            {
                battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(lessStrongUnitPlayerOne);
                CardManager.Instance.SendToGraveyard(lessStrongUnitPlayerOne, Graveyard[0]);
            }
            else if (lessStrongUnitPlayerOne == null) { }
        }
    }

    public void ClearLeastPopulatedRow(Board board)
    {
        Row leastPopulateRowPlayerOne = board.PlayerOneSide.Battlefield.GetRowWithLeastUnits();
        Row leastPopulateRowPlayerTwo = board.PlayerTwoSide.Battlefield.GetRowWithLeastUnits();
        int playerOneNumbersCards = board.PlayerOneSide.Battlefield.GetRowWithLeastUnits().CountCardsInRow();
        int playerTwoNumbersCards = board.PlayerTwoSide.Battlefield.GetRowWithLeastUnits().CountCardsInRow();

        if (playerOneNumbersCards < playerTwoNumbersCards)
        {
            CardManager.Instance.SendToGraveyardLeastPopulatedRow(leastPopulateRowPlayerOne, Graveyard[0]);
            leastPopulateRowPlayerOne.RemoveAllUnitCards();
        }
        else if (playerOneNumbersCards > playerTwoNumbersCards)
        {
            CardManager.Instance.SendToGraveyardLeastPopulatedRow(leastPopulateRowPlayerTwo, Graveyard[1]);
            leastPopulateRowPlayerTwo.RemoveAllUnitCards();
        }
        else
        {
            if (GameManager.Instance.CurrentPlayer == Player.Player_One)
            {
                CardManager.Instance.SendToGraveyardLeastPopulatedRow(leastPopulateRowPlayerTwo, Graveyard[1]);
                leastPopulateRowPlayerTwo.RemoveAllUnitCards();
            }
            else
            {
                CardManager.Instance.SendToGraveyardLeastPopulatedRow(leastPopulateRowPlayerOne, Graveyard[0]);
                leastPopulateRowPlayerOne.RemoveAllUnitCards();
            }
        }
    }

}
