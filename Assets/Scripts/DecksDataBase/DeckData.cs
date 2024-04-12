using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class DeckData : ScriptableObject
{
    
    public List<CardData> DeckCards ;
    
    public LeaderCard LeaderCard;
    
}
