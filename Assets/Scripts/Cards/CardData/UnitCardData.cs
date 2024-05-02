using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Golden, Silver }
public enum AttackType { Melee, Ranged, Siege }
public enum Skills
{
    None, Draw, IncreaseRowPower, SetWeather, ClearStrongestUnit,
    ClearLessStrongUnit, MultiplyPower, ClearLeastPopulatedRow, SetAveragePower,
}

[CreateAssetMenu(fileName = "New Card", menuName = "UnitCard")]
public class UnitCardData : CardData
{
    public UnitType UnitType;
    public int Power;
    public Sprite PowerImage;
    public Skills Skill;
    public AttackType AttackType;
}

