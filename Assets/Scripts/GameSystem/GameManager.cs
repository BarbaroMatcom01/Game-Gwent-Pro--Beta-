using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum Player
{
   PlayerOne,
   PlayerTwo
}

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public static Player CurrentPlayer = Player.PlayerOne;
   public static Player PreviousPlayer = Player.PlayerTwo;
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
   public void ChangerTurn()
   {
      if (CurrentPlayer == Player.PlayerOne)
      {
         CurrentPlayer = Player.PlayerTwo;
         PreviousPlayer = Player.PlayerOne;
      }
      else if (CurrentPlayer==Player.PlayerTwo)
      {
         CurrentPlayer = Player.PlayerOne;
         PreviousPlayer = Player.PlayerTwo;
      }
   }
}