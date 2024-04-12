using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Card", menuName = "UnitCard")]
public class UnitCardData : CardData
{
    public UnitType UnitType;
    public AttackType[] attackTypes;
    public int Power ;
    public Sprite PowerImage;
    public Skill skill;
}
public enum UnitType
{
    Golden,Silver
}
public enum AttackType{
    Melee,Ranged,Siege
}
public enum Skill
{
    None,Draw, DestroyStrongestUnit, DestroyWeakestUnit, DestroyLeastPopulatedRow, 
    SetIncrease, MultiplyPower, SetRain, SetSnow, SetStorm
}

