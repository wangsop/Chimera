using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NickHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Nick Ability");
    }
    protected override void Initialize(){
        index = 2;
        base.Initialize();
    }
}
