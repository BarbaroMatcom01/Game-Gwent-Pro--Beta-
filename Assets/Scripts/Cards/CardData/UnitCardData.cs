using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    El atributo [CreateAssetMenu(fileName = "New Card", menuName = "UnitCard")] permite crear nuevas instancias 
    de UnitCard directamente desde el editor de Unity, seleccionando "UnitCard" en el menú de creación de activos.
    La clase UnitCard hereda de CardData para conservar sus propiedades y definir las propias
*/
[CreateAssetMenu(fileName = "New Card", menuName = "UnitCard")]
public class UnitCardData : CardData
{
    public UnitType UnitType;
    public AttackType[] AttackTypes;
    public int Power ;
    public Sprite PowerImage;
    public Skill Skill;
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

