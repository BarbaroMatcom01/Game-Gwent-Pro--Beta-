using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
   [SerializeField] Row[] playerBattlefield = new Row[3];

    public Row MeeleRow => playerBattlefield[0];
    public Row RangedRow => playerBattlefield[1];
    public Row SiegeRow => playerBattlefield[2];

}