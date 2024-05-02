using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] Image gema1;
   [SerializeField] Image gema2;
   [SerializeField] TextMeshProUGUI playerName;
   [SerializeField] TextMeshProUGUI fieldPower;
   [SerializeField] GameObject leaderSlot;
   public GameObject LeaderSlot => leaderSlot;
   public Battlefield Battlefield;
   public void UpdatePlayerName(string newName)
   {
      playerName.text = newName;
   }
 
   void Update()
    {
       fieldPower.text = Battlefield.BattlefieldPower().ToString();
      
    }
   private int victories = 0;
   public void IncrementVictories()
   {
      victories++;
      UpdateGemImages();
   }
   private void UpdateGemImages()
   {
      if (victories == 1)
      {
         gema1.enabled = true;
      }
      else if (victories == 2)
      {
         gema1.enabled = true;
         gema2.enabled = true;
      }
   }

}
