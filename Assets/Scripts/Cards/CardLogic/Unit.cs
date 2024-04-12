using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : Card
{
    public UnitCardData CardData;
    private Image UnitPower;
    [SerializeField] GameObject[] AttackTypesIcons;
    public int Power {get; private set;}
    
    void Start()
    {
        Power = CardData.Power;
        Image.sprite = CardData.CardImage;
        UnitPower.sprite = CardData.PowerImage;

        if(!CardData.attackTypes.Contains(AttackType.Melee)) 
            AttackTypesIcons[0].SetActive(false);
        if(!CardData.attackTypes.Contains(AttackType.Ranged)) 
            AttackTypesIcons[1].SetActive(false);
        if(!CardData.attackTypes.Contains(AttackType.Siege)) 
            AttackTypesIcons[2].SetActive(false);
    }
}
