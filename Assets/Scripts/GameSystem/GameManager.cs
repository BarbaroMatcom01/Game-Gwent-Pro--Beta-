using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Player
{
   Player_One, Player_Two
}
public enum GameState
{
   ChangeCards, GameStart, RoundStart, Turn, DecoyState, RoundEnd, GameEnd
}
public class GameManager : MonoBehaviour
{
   public Board Board;
   public Leader[] leaders = new Leader[2];
   public Player CurrentPlayer;
   public Player NextPlayer;
   public bool[] HasPassed = new bool[2];
   bool[] roundWinner = new bool[2];
   bool GameIsOver;
   public GameObject WinnerScreen;
   public TextMeshProUGUI WinnerScreenText;
   public GameState CurrentState;
   public static GameManager Instance;
   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else if (Instance != this)
      {
         Destroy(gameObject);
      }
   }

   void Start()
   {
      ChangeState(GameState.ChangeCards);
   }

   public void ChangeState(GameState newState)
   {
      CurrentState = newState;
      switch (newState)
      {
         case GameState.GameStart:
            GameStart();
            ChangeState(GameState.RoundStart);
            break;
         case GameState.RoundStart:
            ChangeState(GameState.Turn);
            break;
         case GameState.Turn:
            Update();
            break;
         case GameState.DecoyState:
            break;
         case GameState.RoundEnd:
            RoundWinner();
            RoundEnd();
            break;
         case GameState.GameEnd:
            GameEnd();
            GameIsOver = true;
            break;
      }
   }

   public void ChooseRandomPlayer()
   {
      int choosePlayer = UnityEngine.Random.Range(0, 2);
      CurrentPlayer = (Player)choosePlayer;
      int nextPlayer = (choosePlayer + 1) % 2;
      NextPlayer = (Player)nextPlayer;
   }

   public void DrawTwoCards(Board board)
   {
      board.PlayerOneSide.Deck.DrawCard();
      board.PlayerOneSide.Deck.DrawCard();
     
      board.PlayerTwoSide.Deck.DrawCard();
      board.PlayerTwoSide.Deck.DrawCard();
   }

   public void GameStart()
   {
      Debug.Log("GameStart");
      ChooseRandomPlayer();
      if (CurrentPlayer == Player.Player_One)
      {
         ChangeHand();
         Board.GameStatus.text = "Player One";
      }
      else
      {
         ChangeHand();
         Board.GameStatus.text = "Player Two";
      }
   }

   public void ChangeTurn()
   {

      if (!HasPassed[1] && (CurrentPlayer == Player.Player_One))
      {
         CurrentPlayer = Player.Player_Two;
         NextPlayer = Player.Player_One;
         ChangeHand();
         Board.GameStatus.text = "Player Two";
      }
      else if (!HasPassed[0] && (CurrentPlayer == Player.Player_Two))
      {
         CurrentPlayer = Player.Player_One;
         NextPlayer = Player.Player_Two;
         ChangeHand();
         Board.GameStatus.text = "Player One";
      }
      else if (HasPassed[0])
      {
         CurrentPlayer = Player.Player_Two;
         ChangeHand();
         Board.GameStatus.text = "Player Two";
      }
      else if (HasPassed[1])
      {
         CurrentPlayer = Player.Player_One;
         ChangeHand();
         Board.GameStatus.text = "Player One";
      }
   }

   public void PassTurn()
   {
      if ((CurrentPlayer == Player.Player_One) && (!HasPassed[0]))
      {
         ChangeTurn();
         HasPassed[0] = true;
         Board.GameStatus.text = "Player One has passed";
         LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player Two");
      }
      else if ((CurrentPlayer == Player.Player_Two) && (!HasPassed[1]))
      {
         ChangeTurn();
         HasPassed[1] = true;
         Board.GameStatus.text = "Player Two has passed";
         LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player One");
      }
      else Debug.Log("No Valid");
   }

   public void TurnEnd()
   {
      if (HasPassed[0] && HasPassed[1])
      {
         ChangeState(GameState.RoundEnd);
      }
   }

   void Update()
   {
      if (!GameIsOver)
      {
         TurnEnd();
      }
   }

   public void RoundWinner()
   {
      int playerOnePower = Board.PlayerOneSide.Battlefield.BattlefieldPower();
      int playerTwoPower = Board.PlayerTwoSide.Battlefield.BattlefieldPower();

      if (playerOnePower > playerTwoPower)
      {
         Debug.Log("Player One Win Round");
         roundWinner[0] = true;
         Board.PlayerOneSide.PlayerInfo.victories++;
      }
      else if (playerOnePower < playerTwoPower)
      {
         Debug.Log("Player Two Win Round");
         roundWinner[1] = true;
         Board.PlayerTwoSide.PlayerInfo.victories++;
      }
      else if (playerOnePower == playerTwoPower)
      {
         Debug.Log("Round is Draw");
         roundWinner[0] = true;
         roundWinner[1] = true;
         Board.PlayerOneSide.PlayerInfo.victories++;
         Board.PlayerTwoSide.PlayerInfo.victories++;
      }
   }

   public void RoundEnd()
   {
      Debug.Log("The End Round");
      int victoriesPlayerOne = Board.PlayerOneSide.PlayerInfo.victories;
      int victoriesPlayerTwo = Board.PlayerTwoSide.PlayerInfo.victories;
      CardManager.Instance.DestroyCard(Board);

      if ((victoriesPlayerOne < 2) && (victoriesPlayerTwo < 2))
      {
         ContinueGame();
         ChangeState(GameState.RoundStart);
      }
      else if (victoriesPlayerOne == 2 || victoriesPlayerTwo == 2)
      {
         ChangeState(GameState.GameEnd);
      }
   }

   public void ContinueGame()
   {
      DrawTwoCards(Board);
      HasPassed[0] = false;
      HasPassed[1] = false;
      leaders[0].IsUsableLeader = true;
      leaders[1].IsUsableLeader = true;

      if (roundWinner[0] && roundWinner[1])
      {
         Board.GameStatus.text = "Round is Draw";
         ChangeHand();
         ChooseRandomPlayer();
         if (CurrentPlayer == Player.Player_One)
         {
            LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player One");
         }
         else
         {
            LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player Two");
         }
      }
      else if (roundWinner[0])
      {
         CurrentPlayer = Player.Player_One;
         ChangeHand();
         Board.GameStatus.text = "Player One Win Round";
         LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player One ");
      }
      else
      {
         CurrentPlayer = Player.Player_Two;
         ChangeHand();
         Board.GameStatus.text = "Player Two Win Round";
         LeanTween.delayedCall(1.5f, () => Board.GameStatus.text = "Player Two ");
      }
   }

   public void GameEnd()
   {
      int victoriesPlayerOne = Board.PlayerOneSide.PlayerInfo.victories;
      int victoriesPlayerTwo = Board.PlayerTwoSide.PlayerInfo.victories;

      WinnerScreen.SetActive(true);
      WinnerScreen.transform.localScale = new Vector3(0, 1, 0);
      LeanTween.delayedCall(2f, () => LeanTween.scaleX(WinnerScreen, 1f, 1f));
      LeanTween.delayedCall(8f, () => SceneManager.LoadScene("Menu"));

      Debug.Log("The End Game");
      if (victoriesPlayerOne == victoriesPlayerTwo)
      {
         Debug.Log("Game Is Draw");
         WinnerScreenText.text = "Game Is Draw";
      }
      else if (victoriesPlayerOne == 2)
      {
         Debug.Log($" Winner Is {GameData.Player1Name}");

         WinnerScreenText.text = $" Winner Is {GameData.Player1Name}";
      }
      else if (victoriesPlayerTwo == 2)
      {
         Debug.Log($" Winner Is {GameData.Player2Name}");

         WinnerScreenText.text = $" Winner Is {GameData.Player2Name}";
      }
   }

   public void ChangeHand()
   {
      Board.PlayerOneSide.ShowPlayerHand(CurrentPlayer == Player.Player_One);
      Board.PlayerTwoSide.ShowPlayerHand(CurrentPlayer == Player.Player_Two);
   }

}