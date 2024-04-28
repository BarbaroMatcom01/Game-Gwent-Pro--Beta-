using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardData : ScriptableObject
{
    public string Name;
    public CardType CardType;
    public string Faction;
    public string Description;
    public Sprite CardImage;
    public SpecificTypeCard SpecificTypeCard;
}
public enum CardType
{
    Leader, Unit, Special
}
public enum SpecificTypeCard
{
    IncreaseMelee,
    IncreaseRanged,
    IncreaseSiege,
    Rain,
    Storm,
    Snow,
    Clearing,
    Decoy,
    MeleeUnit,
    RangedUnit,
    SiegeUnit
}