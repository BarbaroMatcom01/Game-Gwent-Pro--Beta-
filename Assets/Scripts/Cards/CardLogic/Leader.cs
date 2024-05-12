using UnityEngine;


public class Leader : Card
{
    [SerializeField] LeaderCard cardData;
    public Skills Skills;
    public bool IsUsableLeader = true;
   
    void Start()
    {
        Name = cardData.Name;
        Faction = cardData.Faction;
        Description = cardData.Description;
        Skills = cardData.Skill;
    }
}
