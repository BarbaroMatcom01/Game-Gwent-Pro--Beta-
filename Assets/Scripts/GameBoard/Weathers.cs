using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weathers : MonoBehaviour
{
    [SerializeField] GameObject weathers;
    public Battlefield [] Battlefields = new Battlefield[2];
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
        Battlefields[0].MeleeRow.ActiveWeather();
        Battlefields[1].MeleeRow.ActiveWeather();
    }
    public void DeactivateRain()
    {
        weatherIsActive[0] = false;
        Battlefields[0].MeleeRow.DeactivateWeather();
        Battlefields[1].MeleeRow.DeactivateWeather();
    }
    public void ActivateStorm()
    {
        weatherIsActive[1] = true;
        Battlefields[0].RangedRow.ActiveWeather();
        Battlefields[1].RangedRow.ActiveWeather();
    }
    public void DeactivateStorm()
    {
        weatherIsActive[1] = false;
        Battlefields[0].RangedRow.DeactivateWeather();
        Battlefields[1].RangedRow.DeactivateWeather();
    }
    public void ActivateSnow()
    {
        weatherIsActive[2] = true;
        Battlefields[0].SiegeRow.ActiveWeather();
        Battlefields[1].SiegeRow.ActiveWeather();
    }
    public void DeactivateSnow()
    {
        weatherIsActive[2] = false;
        Battlefields[0].SiegeRow.DeactivateWeather();
        Battlefields[1].SiegeRow.DeactivateWeather();
    }
}
