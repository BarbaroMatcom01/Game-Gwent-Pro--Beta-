using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "SpecialCard")]
public class SpecialCardData : CardData
{
    public SpecialType SpecialType;
    public Sprite TypeIcon;
}
public enum SpecialType
{
    Decoy, Increase, Rain, Storm, Snow, Clearing
}

