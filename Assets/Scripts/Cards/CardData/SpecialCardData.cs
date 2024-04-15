using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

/*
    El atributo [CreateAssetMenu(fileName = "New Card", menuName = "SpecialCard")] permite crear nuevas instancias 
    de SpecialCard directamente desde el editor de Unity, seleccionando "SpecialCard" en el menú de creación de activos.
    La clase SpecialCard hereda de CardData para conservar sus propiedades y definir las propias
*/

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