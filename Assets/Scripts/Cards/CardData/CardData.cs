using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
    La clase  CardData que hereda un SciptableObject 
    lo que permite almacenar los datos de las cartas ya que no cambian durante la ejecución del programa 
*/
public class CardData : ScriptableObject
{
    public string Name;
    public CardType CardType;
    public string Faction;
    public string Description;
    public Sprite CardImage;
}
public enum CardType
{
    Leader,Unit,Special
     
}

