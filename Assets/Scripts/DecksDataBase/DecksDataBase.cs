using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Decks", menuName = "Decks")]
public class DecksDataBase : ScriptableObject 
{
    public List<DeckData> decks = new List<DeckData>();
}
