using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : Card
{
    public UnitCardData UnitCardData;
    private Image UnitPower;
    [SerializeField] GameObject[] AttackTypesIcons;
    public int Power {get; set;}

    void Start()
    {
        Power = UnitCardData.Power;
        Image.sprite = UnitCardData.CardImage;
        UnitPower.sprite = UnitCardData.PowerImage;

        if(!UnitCardData.AttackTypes.Contains(AttackType.Melee)) 
            AttackTypesIcons[0].SetActive(false);
        if(!UnitCardData.AttackTypes.Contains(AttackType.Ranged)) 
            AttackTypesIcons[1].SetActive(false);
        if(!UnitCardData.AttackTypes.Contains(AttackType.Siege)) 
            AttackTypesIcons[2].SetActive(false);
    }
}
