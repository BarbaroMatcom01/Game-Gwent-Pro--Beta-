using UnityEngine;
using TMPro;


public class Board : MonoBehaviour
{
  public PlayerSide PlayerOneSide;
  public PlayerSide PlayerTwoSide;
  public Weathers Weathers;
  public TextMeshProUGUI GameStatus;
  public static Board Instance;
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
    PlayerOneSide.PlayerInfo.PlayerName.text = GameData.Player1Name;
    PlayerTwoSide.PlayerInfo.PlayerName.text = GameData.Player2Name;
  }
}
