using UnityEngine;

public abstract class Head : BodyPart
{
    protected string ability_description = "Ability description has not been implemented yet";
    protected string scientist_description = "Scientist description not been implemented yet";
    public abstract void UseAbility();
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Heads[index];
    }
}