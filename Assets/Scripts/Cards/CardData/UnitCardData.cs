using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Card", menuName = "UnitCard")]
public class UnitCardData : CardData
{
    public UnitType UnitType;
    public AttackType[] AttackTypes;
    public int Power ;
    public Sprite PowerImage;
    public UnitSkills Skill;
}
public enum UnitType
{
    Golden,Silver
}
public enum AttackType
{
    Melee,Ranged,Siege
}
public enum UnitSkills
{
  None,Draw,IncreaseRowPower,SetWeather,ClearStrongestUnit,
  ClearLessStrongUnit,MultiplyPower,ClearLeastPopulatedRow,SetAveragePower,
}

