using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weathers : MonoBehaviour
{
    [SerializeField] public GameObject[] weathers=new GameObject[4] ;

    public Battlefield[] Battlefields = new Battlefield[2];
    public bool[] weatherIsActive = new bool[3];
    public bool Rain => weatherIsActive[0];
    public bool Storm => weatherIsActive[1];
    public bool Snow => weatherIsActive[2];

    public void ActivateClearing()
    {
        if (weatherIsActive[0]) { DeactivateRain();}
        if (weatherIsActive[1]) { DeactivateStorm(); }
        if (weatherIsActive[2]) { DeactivateSnow(); }
    }
    public void ActivateRain()
    {
        if (!weatherIsActive[0])
        {
            weatherIsActive[0] = true;
            Battlefields[0].MeleeRow.ActiveWeather();
            Battlefields[1].MeleeRow.ActiveWeather();
        }
    }
    public void DeactivateRain()
    {
        if (weatherIsActive[0])
        {
            weatherIsActive[0] = false;
            Battlefields[0].MeleeRow.DeactivateWeather();
            Battlefields[1].MeleeRow.DeactivateWeather();
        }
    }
    public void ActivateStorm()
    {
        if (!weatherIsActive[1])
        {
            weatherIsActive[1] = true;
            Battlefields[0].RangedRow.ActiveWeather();
            Battlefields[1].RangedRow.ActiveWeather();
        }
    }
    public void DeactivateStorm()
    {
        if (weatherIsActive[1])
        {
            weatherIsActive[1] = false;
            Battlefields[0].RangedRow.DeactivateWeather();
            Battlefields[1].RangedRow.DeactivateWeather();
        }
    }
    public void ActivateSnow()
    {
        if (!weatherIsActive[2])
        {
            weatherIsActive[2] = true;
            Battlefields[0].SiegeRow.ActiveWeather();
            Battlefields[1].SiegeRow.ActiveWeather();
        }
    }
    public void DeactivateSnow()
    {
        if (weatherIsActive[2])
        {
            weatherIsActive[2] = false;
            Battlefields[0].SiegeRow.DeactivateWeather();
            Battlefields[1].SiegeRow.DeactivateWeather();
        }
    }
}
