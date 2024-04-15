using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Unit : Card
{
    public UnitCardData CardData;
    private Image unitPower;
    /* 
     La propiedad serializada de tipo array de GameObject llamada AttackTypesIcons
     permie almacenar una referencia a los objetos de la interfaz de usuario que representan los íconos de los tipos de ataque.
    */
    [SerializeField] GameObject[] AttackTypesIcons;
    public int Power {get; private set;}
    
    void Start()
    {
        Power = CardData.Power;
        Image.sprite = CardData.CardImage;
        unitPower.sprite = CardData.PowerImage;

        if(!CardData.AttackTypes.Contains(AttackType.Melee)) 
            AttackTypesIcons[0].SetActive(false);
        if(!CardData.AttackTypes.Contains(AttackType.Ranged)) 
            AttackTypesIcons[1].SetActive(false);
        if(!CardData.AttackTypes.Contains(AttackType.Siege)) 
            AttackTypesIcons[2].SetActive(false);
    }
}
