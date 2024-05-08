using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSide : MonoBehaviour
{
    [SerializeField] public PlayerInfo PlayerInfo;
    [SerializeField] public Battlefield Battlefield;
    [SerializeField] public Deck Deck;
    [SerializeField] public GameObject hand;
    [SerializeField] GameObject graveyard;
    public GameObject HandPlaceOrderIn;
    public GameObject HandPlaceOrderOut;

    public void ShowPlayerHand(bool IsActive)
    {
        if (IsActive)
        {
            LeanTween.move(hand, HandPlaceOrderIn.transform, 1.5f);
        }
        else
        {
            LeanTween.move(hand, HandPlaceOrderOut.transform, 1f);
        }
    }
}
