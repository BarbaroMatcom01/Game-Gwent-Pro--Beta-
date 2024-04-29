using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.Common;
using UnityEditor.Build;
public class Row : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowPowerText;
    [SerializeField] GameObject increaseSlot;
    public GameObject IncreaseSlot => increaseSlot;
    [SerializeField] GameObject unitCardGrid;
    [SerializeField] public List<Unit> UnitCards;
    public GameObject UnitCardsGrid => unitCardGrid;
    public Weathers Weathers;
    public Battlefield Battlefield;
   
    public bool IncreaseIsActive;
    public bool WeatherIsActive ;
    

    public void ActivateIncrease()
    {
        IncreaseIsActive = true;
    }
    public void DeactivateIncrease()
    {
        IncreaseIsActive = false;
    }
    public void RowPowerText()
    {
        int totalPower = TotalRowPower();
        rowPowerText.SetText(totalPower.ToString());

    }
    public void AddUnitCard(Unit newCard)
    {
        UnitCards.Add(newCard);
    }

    public void RemoveUnitCard(Unit removeCard)
    {
        UnitCards.Remove(removeCard);
    }
    
    public void RemoveAllUnitCards()
    {
        foreach (Unit card in UnitCards)
        {
            RemoveUnitCard(card);
        }
    }
    public int CountCardsInRow()
    {
        return UnitCards.Count;
    }
    public int CountGoldenUnitCards()
    {
        int goldenCount = 0;
        foreach (Unit unitCard in UnitCards)
        {
            if (unitCard.UnitType == UnitType.Golden)
            {
                goldenCount++;
            }
        }
        return goldenCount;
    }
    public int CountSilverUnitCards()
    {
        int silverCount = 0;
        foreach (Unit unitCard in UnitCards)
        {
            if (unitCard.UnitType == UnitType.Silver)
            {
                silverCount++;
            }
        }
        return silverCount;
    }
    public int RowPower()
    {
        int rowPower = 0;
        foreach (Unit unitCard in UnitCards)
        {
            rowPower += unitCard.Power;
        }
        return rowPower;
    }
    public int TotalRowPower()
    {
        int totalRowPower = 0;

        if (IncreaseIsActive && WeatherIsActive)
        {
            totalRowPower = RowPower();
        }
        else if (!IncreaseIsActive && !WeatherIsActive)
        {
            totalRowPower = RowPower();
        }
        else if (!IncreaseIsActive && WeatherIsActive)
        {
            totalRowPower = RowPower() - (2 * CountSilverUnitCards());
        }
        else if (IncreaseIsActive && !WeatherIsActive)
        {
            totalRowPower = RowPower() + 2 * CountSilverUnitCards();
        }
        return totalRowPower;
    }
}

