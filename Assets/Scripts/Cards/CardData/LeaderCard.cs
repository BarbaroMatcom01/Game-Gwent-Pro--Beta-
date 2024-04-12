using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
