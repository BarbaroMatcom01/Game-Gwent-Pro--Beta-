using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo ;
    [SerializeField] public Battlefield Battlefield ;
    [SerializeField] Deck deck; 
    [SerializeField] GameObject hand;
    [SerializeField] GameObject graveyard;
}
