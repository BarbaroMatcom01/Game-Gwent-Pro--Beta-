using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    public Board board;
    public Battlefield[] battlefields = new Battlefield[2];
    public GameObject[] Graveyard = new GameObject[2];
    public GameManager GameManager;
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
            case Skills.SetAveragePower:
                SetAveragePower(board);
                break;
            case Skills.IncreaseRowPower:
                IncreaseRowPower(unit);
                break;
            case Skills.SetWeather:
                SetWeather(unit);
                break;
            case Skills.ClearStrongestUnit:
                ClearStrongestUnit(board);
                break;
            case Skills.ClearLessStrongUnit:
                ClearLessStrongUnit(board);
                break;
            case Skills.ClearLeastPopulatedRow:
                ClearLeastPopulatedRow(board);
                break;
        }
    }
    public void ActivateLeaderEffect(Leader leader)
    {
        if (leader.IsUsableLeader){
            switch (leader.Skills)
            {
                case Skills.Draw:
                    Draw();
                    break;
            }
        GameManager.ChangeTurn();
        leader.IsUsableLeader = false;
        }
    }
    public void Draw()
    {
        if (GameManager.CurrentPlayer == Player.Player_One)
        {
            board.PlayerOneSide.Deck.DrawCard();
        }
        else
        {
            board.PlayerTwoSide.Deck.DrawCard();
        }

    }
    public void MultiplyPower(Unit unit)
    {
        int cardAppearancesInBoard = board.PlayerOneSide.Battlefield.CardAppearances(unit) + board.PlayerTwoSide.Battlefield.CardAppearances(unit);
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
        if (GameManager.CurrentPlayer == Player.Player_One)
        {
            int getPositionCard = battlefields[0].GetPositionUnit(unit);
            Row inreaseRow = battlefields[0].PlayerBattlefield[getPositionCard];
            foreach (Unit card in inreaseRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power = card.Power + 2;
                }
            }
        }
        else
        {
            int getPositionCard = battlefields[1].GetPositionUnit(unit);
            Row inreaseRow = battlefields[1].PlayerBattlefield[getPositionCard];
            foreach (Unit card in inreaseRow.UnitCards)
            {
                if (card.UnitType == UnitType.Silver)
                {
                    card.Power = card.Power + 2;
                }
            }
        }
    }
    public void SetWeather(Unit unit)
    {
        if (GameManager.CurrentPlayer == Player.Player_One)
        {
            int getPositionCard = battlefields[0].GetPositionUnit(unit);
            if (getPositionCard == 0)
            {
                board.Weathers.ActivateRain();
            }
            else if (getPositionCard == 1)
            {
                board.Weathers.ActivateSnow();
            }
            else if (getPositionCard == 2)
            {
                board.Weathers.ActivateStorm();
            }
        }
        else
        {
            int getPositionCard = battlefields[1].GetPositionUnit(unit);
            if (getPositionCard == 0)
            {
                board.Weathers.ActivateRain();
            }
            else if (getPositionCard == 1)
            {
                board.Weathers.ActivateSnow();
            }
            else if (getPositionCard == 2)
            {
                board.Weathers.ActivateStorm();
            }
        }
    }
    public void ClearStrongestUnit(Board board)
    {
        Unit strongestUnitPlayerOne = board.PlayerOneSide.Battlefield.GetStrongestUnit();
        Unit strongestUnitPlayerTwo = board.PlayerTwoSide.Battlefield.GetStrongestUnit();
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
        Unit strongestUnitPlayerOne = board.PlayerOneSide.Battlefield.GetLessStrongUnit();
        Unit strongestUnitPlayerTwo = board.PlayerTwoSide.Battlefield.GetLessStrongUnit();
        Battlefield battlefieldPlayerOne = board.PlayerOneSide.Battlefield;
        Battlefield battlefieldPlayerTwo = board.PlayerTwoSide.Battlefield;
        int positionUnitPlayerOne = battlefieldPlayerOne.GetPositionLessStrongUnit();
        int positionUnitPlayerTwo = battlefieldPlayerTwo.GetPositionLessStrongUnit();

        if (GameManager.CurrentPlayer == Player.Player_One)
        {
            if (strongestUnitPlayerTwo != null)
            {
                battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
            }
            else if (strongestUnitPlayerTwo == null) { }
        }
        else
        {
            if (strongestUnitPlayerOne != null)
            {
                battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
            }
            else if (strongestUnitPlayerOne == null) { }
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
            leastPopulateRowPlayerOne.RemoveAllUnitCards();
        }
        else if (playerOneNumbersCards > playerTwoNumbersCards)
        {
            leastPopulateRowPlayerTwo.RemoveAllUnitCards();
        }
        else
        {
            leastPopulateRowPlayerOne.RemoveAllUnitCards();
            leastPopulateRowPlayerTwo.RemoveAllUnitCards();
        }
    }
}
