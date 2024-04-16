using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField] Row[] playerBattlefield = new Row[3];

    public Row MeeleRow => playerBattlefield[0];
    public Row RangedRow => playerBattlefield[1];
    public Row SiegeRow => playerBattlefield[2];

    public int PowerBattlefield()
    {
        int powerBattlefield = 0;
        foreach (Row row in playerBattlefield)
        {
            powerBattlefield += row.TotalRowPower();
        }
        return powerBattlefield;
    }

    public Row GetRowWithLeastUnits()
    {
        Row rowWithLeastUnit = null;
        int leastUnitsCount = 15;
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
