using UnityEngine;

public abstract class Head : BodyPart
{
    public abstract void UseAbility();
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Heads[index];
    }
}
