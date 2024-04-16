using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Row : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rowPowerText;
    [SerializeField] GameObject increaseSlot;
    public GameObject IncreaseSlot => increaseSlot;
    [SerializeField] GameObject unitCardGrid;
    [SerializeField] List<UnitCardData> unitCards;
    public GameObject UnitCardsGrid => unitCardGrid;

    bool increaseIsActive;
    bool weatherIsActive;


    public void RowPowerText()
    {
        int totalPower = TotalRowPower();
        rowPowerText.SetText(totalPower.ToString());

    }
    public void AddUnitCard(UnitCardData newCard)
    {
        unitCards.Add(newCard);
    }

    public void RemoveUnitCard(UnitCardData removeCard)
    {
        unitCards.Remove(removeCard);
    }

    public int CountCardsInRow()
    {
        return unitCards.Count;
    }
    public int CountGoldenUnitCards()
    {
        int goldenCount = 0;
        foreach (UnitCardData unitCardData in unitCards)
        {
            if (unitCardData.UnitType == UnitType.Golden)
            {
                goldenCount++;
            }
        }
        return goldenCount;
    }

    public int CountSilverUnitCards()
    {
        int silverCount = 0;
        foreach (UnitCardData unitCardData in unitCards)
        {
            if (unitCardData.UnitType == UnitType.Silver)
            {
                silverCount++;
            }
        }
        return silverCount;
    }

    public int RowPower()
    {
        int rowPower = 0;
        foreach (UnitCardData unitCardData in unitCards)
        {
            rowPower += unitCardData.Power;
        }
        return rowPower;
    }

    public int TotalRowPower()
    {
        int totalRowPower = 0;

        if (increaseIsActive && weatherIsActive)
        {
            totalRowPower = RowPower();
        }
        else if (!increaseIsActive && !weatherIsActive)
        {
            totalRowPower = RowPower();
        }
        else if (!increaseIsActive && weatherIsActive)
        {
            totalRowPower = RowPower() - (2 * CountSilverUnitCards());
        }
        else if (increaseIsActive && !weatherIsActive)
        {
            totalRowPower = RowPower() + 2 * CountSilverUnitCards();
        }
        return totalRowPower;
    }
}

