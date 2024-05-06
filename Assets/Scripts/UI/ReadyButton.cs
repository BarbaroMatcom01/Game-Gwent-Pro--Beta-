using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public string PlayerNumber;
    public Button Ready;
    void Start()
    {
        Ready.interactable = false;
    }

    void Update()
    {
        if (PlayerName.text.Length > 3 && ((PlayerNumber == "1" && GameData.Player1Faction != null) || (PlayerNumber == "2" && GameData.Player2Faction != null)))
        {
            Ready.interactable = true;
        }

        else
            Ready.interactable = false;
    }

    public void OnClickForPlayer1()
    {
        GameData.SetPlayer1Name(PlayerName.text);
        GameData.ReadyPlayer1 = true;
    }

    public void OnClickForPlayer2()
    {
        GameData.SetPlayer2Name(PlayerName.text);
        GameData.ReadyPlayer2 = true;
    }
}
