using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class DeckData : ScriptableObject
{
    /*
      Define una propiedad pública de tipo List<CardData> llamada DeckCards.
      Esta propiedad almacenará una lista de objetos CardData, representando las cartas que componen el mazo.
    */
    public List<CardData> DeckCards ;
    
    // Esta propiedad almacenará un objeto LeaderCard, representando la carta líder del mazo.
    public LeaderCard LeaderCard;
    
}
