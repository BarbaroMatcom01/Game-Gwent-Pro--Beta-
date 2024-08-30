using System.Collections.Generic;
using UnityEngine;
public class CardData : ScriptableObject 
{
    public string Name;
    public string Faction;
    public string Description;
    public int Owner;
    public Sprite CardImage;
    public Sprite TypeIcon;
    public List<OnActivationObject> OnActivation;
     

}