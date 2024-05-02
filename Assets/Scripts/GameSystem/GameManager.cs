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
   public static Player CurrentPlayer = Player.PlayerTwo;
}
