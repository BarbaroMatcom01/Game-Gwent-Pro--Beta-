using System.Collections;
using System.Collections.Generic;
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
  PlayerOneSide.PlayerInfo.playerName.text=GameData.Player1Name;
  PlayerTwoSide.PlayerInfo.playerName.text=GameData.Player2Name;
}
}
