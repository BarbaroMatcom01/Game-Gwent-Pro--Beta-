using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class ChangeCards : MonoBehaviour, IPointerClickHandler
{   
    public Board board;
    public Deck[] Decks = new Deck[2];
    public GameObject[] HandsOfChangeCard = new GameObject[2];
    public GameObject ChangeCard;
    public bool[] playersAreReady = new bool[2];
    public TextMeshProUGUI[] cardCountText;
    public int[] cardCount;
    void Update()
    {
        HandsOfChangeCard[0]=board.PlayerOneSide.hand;
        HandsOfChangeCard[1]=board.PlayerTwoSide.hand;
    }
     public void SetPlayerReady(int player)
    {
        playersAreReady[player] = true;
        if (playersAreReady[0] && playersAreReady[1])
        {
            StartGame();
        }
    }
    void StartGame()
    {
        GameManager.Instance.ChangeState(GameState.GameStart);
        ChangeCard.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        
        Destroy(this.transform.gameObject);
        Decks[0].DrawCard();
    } 

}
