using UnityEngine;

[DefaultExecutionOrder(-100)]
public class HorselessHead : Head
{
    public override void UseAbility(){
        Debug.Log("Used Horseless Ability");

        
    }
    protected override void Initialize(){
        index = 4;
        base.Initialize();
    }
}
