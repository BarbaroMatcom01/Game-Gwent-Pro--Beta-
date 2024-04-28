using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weathers : MonoBehaviour
{

    [SerializeField] GameObject weathers;
    public Battlefield Battlefield;
    public bool[] weatherIsActive = new bool[3];
    public bool Rain => weatherIsActive[0];
    public bool Storm => weatherIsActive[1];
    public bool Snow => weatherIsActive[2];

    public void ActivateClearing()
    {
          DeactivateRain();
          DeactivateSnow();
          DeactivateStorm();
    }
    public void ActivateRain()
    {
        weatherIsActive[0] = true;
        Battlefield.MeleeRow.WeatherIsActive = true;
    }
    public void DeactivateRain()
    {
        weatherIsActive[0] = false;
        Battlefield.MeleeRow.WeatherIsActive = false;
    }
    public void ActivateStorm()
    {
        weatherIsActive[1] = true;
        Battlefield.RangedRow.WeatherIsActive = true;
    }
    public void DeactivateStorm()
    {
        weatherIsActive[1] = false;
        Battlefield.RangedRow.WeatherIsActive = false;
    }
    public void ActivateSnow()
    {
        weatherIsActive[2] = true;
        Battlefield.SiegeRow.WeatherIsActive = true;
    }
    public void DeactivateSnow()
    {
        weatherIsActive[2] = false;
        Battlefield.SiegeRow.WeatherIsActive = false;
    }
}
