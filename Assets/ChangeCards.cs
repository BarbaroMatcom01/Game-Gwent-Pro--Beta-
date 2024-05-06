using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCards : MonoBehaviour, IPointerClickHandler
{
    public Deck[] Decks = new Deck[2];
    public GameObject[] Hands = new GameObject[2];
    public GameObject[] InitialHandPanel = new GameObject[2];

    public TextMeshProUGUI[] cardCountText;
    public int[] cardCount;
    bool[] playersAreReady = new bool[2];

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Decks[0].DrawCard();
            Decks[1].DrawCard();
        }
    }
    void SetPlayerReady(int player)
    {
        playersAreReady[player] = true;
        if (playersAreReady[0] && playersAreReady[1])
            StartGame();
    }

    void StartGame() { }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
}
