using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : MonoBehaviour
{
    [SerializeField] public PlayerInfo PlayerInfo ;
    [SerializeField] public Battlefield Battlefield ;
    [SerializeField] public Deck Deck; 
    [SerializeField] GameObject hand;
    [SerializeField] GameObject graveyard;
}
