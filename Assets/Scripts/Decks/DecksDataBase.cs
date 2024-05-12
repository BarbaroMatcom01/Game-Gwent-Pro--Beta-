using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Decks", menuName = "Decks")]
public class DecksDataBase : ScriptableObject 
{
    public List<DeckData> Decks = new List<DeckData>();
}
