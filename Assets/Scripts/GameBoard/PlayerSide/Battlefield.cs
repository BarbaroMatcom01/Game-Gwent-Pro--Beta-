using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField] Row[] playerBattlefield = new Row[3];

    public Row MeeleRow => playerBattlefield[0];
    public Row RangedRow => playerBattlefield[1];
    public Row SiegeRow => playerBattlefield[2];

    public int BattlefieldPower()
    {
        int battlefieldPower = 0;
        foreach (Row row in playerBattlefield)
        {
            battlefieldPower += row.TotalRowPower();
        }
        return battlefieldPower;
    }

    public Row GetRowWithLeastUnits()
    {
        Row rowWithLeastUnit = playerBattlefield[0] ;
        int leastUnitsCount = int.MaxValue;
        foreach (Row row in playerBattlefield)
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

}
