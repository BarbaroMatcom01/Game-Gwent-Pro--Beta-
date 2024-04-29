using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Card", menuName = "LeaderCard")]
public class LeaderCard : CardData
{
    public Skills Skill;
}
public enum Skills
{
    None, Draw, IncreaseRowPower, SetWeather, ClearStrongestUnit,
    ClearLessStrongUnit, MultiplyPower, ClearLeastPopulatedRow, SetAveragePower,
}
