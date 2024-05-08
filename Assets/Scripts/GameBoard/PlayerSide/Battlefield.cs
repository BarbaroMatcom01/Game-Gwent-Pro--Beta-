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
            if ((newUnitsCount < leastUnitsCount)&&(newUnitsCount!=0))
            {
                leastUnitsCount = newUnitsCount;
                rowWithLeastUnit = row;
            }
            else if  (newUnitsCount==leastUnitsCount)
            {  
                if(row.CountSilverUnitCards()>rowWithLeastUnit.CountSilverUnitCards())
                {
                   rowWithLeastUnit=row;
                }
            }

        }
        return rowWithLeastUnit;
    }
    public Unit GetLessStrongUnit()
    {
        Unit leesStrongUnit = null;
        int leastPower = int.MaxValue;

        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit unit in row.UnitCards)
            {
                if (unit.UnitType==UnitType.Silver && unit.Power < leastPower)
                {
                    leastPower = unit.Power;
                    leesStrongUnit = unit;
                }
            }
        }
        return leesStrongUnit;
    }
    public int GetPositionLessStrongUnit()
    {   if(GetLessStrongUnit()!=null)
        {
        Unit lessStrongUnit = GetLessStrongUnit();
        AttackType Name = lessStrongUnit.AttackType;
        if (Name == AttackType.Melee) { return 0; }
        else if (Name == AttackType.Ranged) { return 1; }
        else if (Name == AttackType.Siege) { return 2; }
        else return -1;
        }
        else return-1;
    }
    public Unit GetStrongestUnit()
    {
        Unit strongestUnit = null;
        int strongPower = int.MinValue;

        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit unit in row.UnitCards)
            {
                if (unit.UnitType==UnitType.Silver && unit.Power > strongPower)
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
        if(GetStrongestUnit()!=null)
        {
        Unit strongestUnit = GetStrongestUnit();
        AttackType Name = strongestUnit.AttackType;
        if (Name == AttackType.Melee) { return 0; }
        else if (Name == AttackType.Ranged) { return 1; }
        else if (Name == AttackType.Siege) { return 2; }
        else  return -1;
        }
        else return -1;
    }
    public int CardAppearances(Unit unit)
    {
        int cardAppearances = 0;
        string unitCard=unit.name;
        foreach (Row row in PlayerBattlefield)
        {
            foreach (Unit card in row.UnitCards)
            {
                if (unitCard==card.name)                
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
    public int GetPositionUnit(Unit unit)
    {   
        AttackType attackType= unit.AttackType;
        if (attackType == AttackType.Melee) { return 0; }
        else if (attackType== AttackType.Ranged) { return 1; }
        else if (attackType == AttackType.Siege) { return 2; }
        else { return -1; }
    }
}

