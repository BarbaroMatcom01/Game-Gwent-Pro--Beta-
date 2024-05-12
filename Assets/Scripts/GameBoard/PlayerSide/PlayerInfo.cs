using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] public TextMeshProUGUI PlayerName;
   [SerializeField] TextMeshProUGUI fieldPower;
   [SerializeField] GameObject leaderSlot;
   [SerializeField] Image gema1;
   [SerializeField] Image gema2;
   public Battlefield Battlefield;
   public int victories = 0;

   void Update()
   {
      fieldPower.text = Battlefield.BattlefieldPower().ToString();
      UpdateGemImages();
   }

   public void UpdateGemImages()
   {
      if (victories == 0)
      {
         gema1.enabled = false;
         gema2.enabled = false;
      }
      else if (victories == 1)
      {
         gema1.enabled = true;
      }
      else if (victories == 2)
      {
         gema2.enabled = true;
      }
   }
}
