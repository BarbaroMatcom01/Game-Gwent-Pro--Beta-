using TMPro;
using UnityEngine;


public class ChangeCards : MonoBehaviour
{
    public Deck[] Decks;
    public GameObject[] Hands = new GameObject[2];
    public GameObject[] ChangeCardPanels = new GameObject[2];
    public TextMeshProUGUI[] ChangedCardsText;
    int[] ChangedCardsCount = { 0, 0 };
    bool[] playersAreReady = new bool[2];
    public static ChangeCards Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Decks[0].DrawCard();
            Decks[1].DrawCard();
        }

        Hands[0].transform.SetParent(ChangeCardPanels[0].transform);
        Hands[1].transform.SetParent(ChangeCardPanels[1].transform);
    }

    public void ChangeCard(Card card)
    {
        int player = (card.transform.parent.name == "PlayerOneHand") ? 0 : 1;

        if (ChangedCardsCount[player] == 2) return;

        ChangedCardsCount[player]++;
        ChangedCardsText[player].text = $"{ChangedCardsCount[player]}/2";

        Decks[player].AddCard(card.CardData);
        Destroy(card.gameObject);
        Decks[player].DrawCard();
    }

    public void SetPlayerReady(int player)
    {
        playersAreReady[player] = true;
        if (playersAreReady[0] && playersAreReady[1])
        {
            GameManager.Instance.ChangeState(GameState.GameStart);
            Hands[0].transform.SetParent(GameManager.Instance.Board.transform);
            Hands[1].transform.SetParent(GameManager.Instance.Board.transform);
            this.gameObject.SetActive(false);
        }
    }
}
