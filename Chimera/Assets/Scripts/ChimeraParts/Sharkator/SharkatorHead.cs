using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SharkatorHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Sharkator Ability");
    }
    protected override void Initialize(){
        index = 1;
        base.Initialize();
    }
}
