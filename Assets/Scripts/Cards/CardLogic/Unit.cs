using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : Card
{
    public UnitCardData UnitCardData;
    [SerializeField] private Image UnitPower;
    [SerializeField] private Image TypeIcon;
    [SerializeField] GameObject[] AttackTypesIcons;
    public int Power { get; set; }
    public UnitType UnitType;
    public AttackType AttackType;
    void Start()
    {
        this.gameObject.name = UnitCardData.name;
        Name = UnitCardData.Name;
        Faction=UnitCardData.Faction;
        Description=UnitCardData.Description;
        Power = UnitCardData.Power;
        Image.sprite = UnitCardData.CardImage;
        UnitPower.sprite = UnitCardData.PowerImage;
        UnitType = UnitCardData.UnitType;
        this.AttackType = UnitCardData.AttackType;
        TypeIcon.sprite = UnitCardData.TypeIcon;
    }
}
