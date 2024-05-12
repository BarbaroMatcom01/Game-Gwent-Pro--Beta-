using UnityEngine;


public enum SpecialType
{
    Decoy, MeleeIncrease, RangedIncrease, SiegeIncrease, Rain, Storm, Snow, Clearing
}


[CreateAssetMenu(fileName = "New Card", menuName = "SpecialCard")]
public class SpecialCardData : CardData
{
    public SpecialType SpecialType;
}


