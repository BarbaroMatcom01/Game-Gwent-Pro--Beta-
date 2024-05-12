using UnityEngine;


public class PlayerSide : MonoBehaviour
{
    [SerializeField] public PlayerInfo PlayerInfo;
    [SerializeField] public Battlefield Battlefield;
    [SerializeField] public Deck Deck;
    [SerializeField] public GameObject Hand;
    [SerializeField] GameObject Graveyard;
    public GameObject HandPlaceOrderIn;
    public GameObject HandPlaceOrderOut;

    public void ShowPlayerHand(bool IsActive)
    {
        if (IsActive)
        {
            LeanTween.move(Hand, HandPlaceOrderIn.transform, 1.5f);
        }
        else
        {
            LeanTween.move(Hand, HandPlaceOrderOut.transform, 1f);
        }
    }
}
