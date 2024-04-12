using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
   [SerializeField] Image gema1 ;
   [SerializeField] Image gema2 ;
   [SerializeField] TextMeshProUGUI playerName;
   [SerializeField] TextMeshProUGUI fieldPower;
   [SerializeField] GameObject leaderSlot;
   public GameObject LeaderSlot => leaderSlot;
}
