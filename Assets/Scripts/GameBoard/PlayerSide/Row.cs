using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Row : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowPowerText;
    [SerializeField] GameObject increaseSlot;
    [SerializeField] List<Card> unitCards;
    [SerializeField] GameObject unitCardGrid;
    public GameObject UnitCardsGrid => unitCardGrid;
    public GameObject IncreaseSlot => increaseSlot;

    bool increaseIsActive;
    bool weatherIsActive;
}
