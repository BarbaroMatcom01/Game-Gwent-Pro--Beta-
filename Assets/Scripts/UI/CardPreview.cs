using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardPreview : MonoBehaviour
{
    public static CardPreview Instance;
    public GameObject CardView;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Faction;
    public TextMeshProUGUI Description;
    private Card clone;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void ShowCardInfo(Card card)
    {
        Name.text = card.Name;
        Faction.text = card.Faction;
        Description.text = card.Description;
        clone = Instantiate(card,CardView.transform,false);
    }
    public void DestroyClone()
    {
        Destroy(clone.gameObject);
        Name.text = "Name";
        Faction.text = "";
        Description.text = "";
    }
}
