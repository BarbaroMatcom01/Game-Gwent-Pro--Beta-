using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    El atributo [CreateAssetMenu(fileName = "New Card", menuName = "LeaderCard")] permite crear nuevas instancias 
    de LeaderCard directamente desde el editor de Unity, seleccionando "LeaderCard" en el menú de creación de activos.
    La clase LeaderCard hereda de CardData para conservar sus propiedades y definir las propias.
*/

[CreateAssetMenu(fileName = "New Card", menuName = "LeaderCard")]
public class LeaderCard : CardData
{
    public Skill skill;
    public enum Skill
    {
        None, Draw, DestroyStrongestUnit, DestroyWeakestUnit, DestroyLeastPopulatedRow,
        SetBuff, MultiplyPower, SetRain, SetSnow, SetStorm
    }
}
