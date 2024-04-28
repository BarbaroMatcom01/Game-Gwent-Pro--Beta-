using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    [SerializeField]  public Row[] PlayerBattlefield = new Row[3];

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
        Row rowWithLeastUnit = PlayerBattlefield[0] ;
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

}
