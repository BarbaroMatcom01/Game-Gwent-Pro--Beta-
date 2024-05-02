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
    public GameObject UnitCardsGrid => unitCardGrid;
    [SerializeField] public List<Unit> UnitCards;

    public bool IncreaseIsActive;
    public bool WeatherIsActive;

    public void ActiveWeather()
    {
        WeatherIsActive = true;

        foreach (Unit card in UnitCards)
        {
            if (card.UnitType == UnitType.Silver)
            {
                card.Power = card.Power - 2;
            }

        }
    }
    public void DeactivateWeather()
    {
        WeatherIsActive = false;
        foreach (Unit card in UnitCards)
        {
            if (card.UnitType == UnitType.Silver)
            {
                card.Power = card.Power + 2;
            }
        }
    }
    public void ActivateIncrease()
    {
        IncreaseIsActive = true;
        foreach (Unit card in UnitCards)
        {
            if (card.UnitType == UnitType.Silver)
            {
                card.Power = card.Power + 2;
            }
        }
    }
    public void DeactivateIncrease()
    {
        IncreaseIsActive = false;
        foreach (Unit card in UnitCards)
        {
            if (card.UnitType == UnitType.Silver)
            {
                card.Power = card.Power - 2;
            }
        }
    }
    public void AddUnitCard(Unit newCard)
    {

        if (!IncreaseIsActive && !WeatherIsActive)
        {
            UnitCards.Add(newCard);
        }
        else if (IncreaseIsActive && !WeatherIsActive)
        {
            DeactivateIncrease();
            UnitCards.Add(newCard);
            ActivateIncrease();

        }
        else if (!IncreaseIsActive && WeatherIsActive)
        {
            DeactivateWeather();
            UnitCards.Add(newCard);
            ActiveWeather();
        }
        else if (!IncreaseIsActive && !WeatherIsActive)
        {
            DeactivateIncrease();
            DeactivateWeather();
            UnitCards.Add(newCard);
            ActivateIncrease();
            ActiveWeather();
        }
    }

    public void RemoveUnitCard(Unit removeCard)
    {
        UnitCards.Remove(removeCard);
    }

    public void RemoveAllUnitCards()
    {
        UnitCards.Clear();
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

    public int TotalRowPower()
    {
        int totalRowPower = 0;
        foreach (Unit card in UnitCards)
        {
            totalRowPower += card.Power;
        }
        return totalRowPower;

    }

    void Update()
    {
        rowPowerText.text = TotalRowPower().ToString();
    }
}