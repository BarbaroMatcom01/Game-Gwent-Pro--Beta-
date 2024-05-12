using UnityEngine;
using TMPro;


public class Board : MonoBehaviour
{
  public PlayerSide PlayerOneSide;
  public PlayerSide PlayerTwoSide;
  public Weathers Weathers;
  public TextMeshProUGUI GameStatus;
 
  void Start()
  {
    PlayerOneSide.PlayerInfo.PlayerName.text = GameData.Player1Name;
    PlayerTwoSide.PlayerInfo.PlayerName.text = GameData.Player2Name;
  }
}
