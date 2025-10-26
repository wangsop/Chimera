using UnityEngine;

public abstract class Head : BodyPart
{
    public string ability_description = "Ability description has not been implemented yet";
    public string scientist_description = "Scientist description not been implemented yet";
    //public abstract int rarity { get; set; }
    public int rarity = 1;

    public abstract void UseAbility();
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Heads[index];
    }
}