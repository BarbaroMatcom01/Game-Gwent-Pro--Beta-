using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField] public Row[] PlayerBattlefield = new Row[3];

    public Row MeleeRow => PlayerBattlefield[0];
    public Row RangedRow => PlayerBattlefield[1];
    public Row SiegeRow => PlayerBattlefield[2];

    public int BattlefieldPower()
    {
        int battlefieldPower = 0;
        foreach (Row row in PlayerBattlefield)
        {
            battlefieldPower += row.TotalRowPower();
        }
        return battlefieldPower;
    }
    public Row GetRowWithLeastUnits()
    {
        Row rowWithLeastUnit = PlayerBattlefield[0];
        int leastUnitsCount = int.MaxValue;
        foreach (Row row in PlayerBattlefield)
        {
            int newUnitsCount = row.CountCardsInRow();
            if (newUnitsCount < leastUnitsCount)
            {
                leastUnitsCount = newUnitsCount;
                rowWithLeastUnit = row;
            }
        }
        return rowWithLeastUnit;
    }
    public Unit GetLessStrongUnit()
    {
        Unit leesStrongUnit = null;
        int leastPower = int.MinValue;

        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit unit in row.UnitCards)
            {
                if (unit.Power < leastPower)
                {
                    leastPower = unit.Power;
                    leesStrongUnit = unit;
                }
            }
        }
        return leesStrongUnit;
    }
    public int GetPositionLessStrongUnit()
    {
        Unit lessStrongUnit = GetLessStrongUnit();
        SpecificTypeCard Name = lessStrongUnit.SpecificTypeCard;
        if (Name == SpecificTypeCard.MeleeUnit) { return 0; }
        else if (Name == SpecificTypeCard.RangedUnit) { return 1; }
        else if (Name == SpecificTypeCard.SiegeUnit) { return 2; }
        else { return -1; }

    }
    public Unit GetStrongestUnit()
    {
        Unit strongestUnit = null;
        int strongPower = int.MinValue;

        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit unit in row.UnitCards)
            {
                if (unit.Power > strongPower)
                {
                    strongPower = unit.Power;
                    strongestUnit = unit;
                }
            }
        }
        return strongestUnit;
    }
    public int GetPositionStrongestUnit()
    {
        Unit strongestUnit = GetStrongestUnit();
        SpecificTypeCard Name = strongestUnit.SpecificTypeCard;
        if (Name == SpecificTypeCard.MeleeUnit) { return 0; }
        else if (Name == SpecificTypeCard.RangedUnit) { return 1; }
        else if (Name == SpecificTypeCard.SiegeUnit) { return 2; }
        else { return -1; }
    }
    public int CardAppearances(Unit card)
    {
        int cardAppearances = 0;
        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit unit in row.UnitCards)
            {
                if (unit.Name == card.Name)
                {
                 cardAppearances ++ ;
                }
            }
        }
        return cardAppearances;
    }

    public int NumberOfCardsOnTheBattlefield()
    {
        int numberOfCardsOnTheBattlefield=0;
        
        foreach (Row row in PlayerBattlefield)
        {
           numberOfCardsOnTheBattlefield+=row.CountCardsInRow();            
        }
        return numberOfCardsOnTheBattlefield;
    }
}

