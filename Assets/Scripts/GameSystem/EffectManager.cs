using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    Board board;
    Deck Deck;
    Battlefield battlefield;
    Battlefield [] battlefields = new Battlefield[2];
    public void ActivateUnitEffect(Unit unit)
    {
        switch (unit.UnitCardData.Skill)
        {
            case Skills.Draw:
                Draw();
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
            case Skills.MultiplyPower:
                MultiplyPower(unit);
                break;
            case Skills.ClearLeastPopulatedRow:
                ClearLeastPopulatedRow(board);
                break;
            case Skills.SetAveragePower:
                SetAveragePower(board);
                break;
        }
    }

    public void ActivateLeaderEffect(Leader leader)
    {
        switch (leader.LeaderCard.Skill)
        {
            case Skills.Draw:
                Draw();
                break;
            case Skills.ClearStrongestUnit:
                ClearStrongestUnit(board);
                break;
            case Skills.ClearLeastPopulatedRow:
                ClearLeastPopulatedRow(board);
                break;
        }
    }
    public void Draw()
    {   
        
        Deck.DrawCard();
    }
    public void IncreaseRowPower(Unit unit)
    {
        int getPositionCard = battlefield.GetPositionUnit(unit);
        Row inreaseRow = battlefield.PlayerBattlefield[getPositionCard];
        foreach (Unit card in inreaseRow.UnitCards)
        {
            card.Power = card.Power + 2;
        }
    }
    public void SetWeather(Unit unit) 
    {
        int getPositionCard = battlefield.GetPositionUnit(unit);
        Row inreaseRowPlayerOne = battlefields[0].PlayerBattlefield[getPositionCard];
        Row inreaseRowPlayerTwo = battlefields[1].PlayerBattlefield[getPositionCard];
        
        foreach (Unit card in inreaseRowPlayerOne.UnitCards)
        {
            card.Power = card.Power - 2;
        }
         foreach (Unit card in inreaseRowPlayerTwo.UnitCards)
        {
            card.Power = card.Power - 2;
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

        if (strongestUnitPlayerOne.Power < strongestUnitPlayerTwo.Power)
        {
            battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
        }
        else if (strongestUnitPlayerOne.Power > strongestUnitPlayerTwo.Power)
        {
            battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
        }
        else
        {
            battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);

            battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
        }
    }
    public void ClearLessStrongUnit(Board board)
    {
        Unit strongestUnitPlayerOne = board.PlayerOneSide.Battlefield.GetStrongestUnit();
        Unit strongestUnitPlayerTwo = board.PlayerTwoSide.Battlefield.GetStrongestUnit();
        Battlefield battlefieldPlayerOne = board.PlayerOneSide.Battlefield;
        int positionUnitPlayerOne = battlefieldPlayerOne.GetPositionStrongestUnit();
        Battlefield battlefieldPlayerTwo = board.PlayerTwoSide.Battlefield;
        int positionUnitPlayerTwo = battlefieldPlayerTwo.GetPositionStrongestUnit();


        if (strongestUnitPlayerOne.Power < strongestUnitPlayerTwo.Power)
        {
            battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);
        }
        else if (strongestUnitPlayerOne.Power > strongestUnitPlayerTwo.Power)
        {
            battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
        }
        else
        {
            battlefieldPlayerOne.PlayerBattlefield[positionUnitPlayerOne].RemoveUnitCard(strongestUnitPlayerOne);

            battlefieldPlayerTwo.PlayerBattlefield[positionUnitPlayerTwo].RemoveUnitCard(strongestUnitPlayerTwo);
        }
    }
    public void MultiplyPower(Unit card)
    {
        int cardAppearancesInBoard = board.PlayerOneSide.Battlefield.CardAppearances(card) + board.PlayerTwoSide.Battlefield.CardAppearances(card);
        card.Power = card.Power * cardAppearancesInBoard;
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
                card.Power = averagePower;
            }
        }
        foreach (Row row in battlefieldPlayerTwo)
        {
            foreach (Unit card in row.UnitCards)
            {
                card.Power = averagePower;
            }
        }
    }
}