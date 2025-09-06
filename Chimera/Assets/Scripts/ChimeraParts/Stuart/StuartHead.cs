using UnityEngine;

[DefaultExecutionOrder(-100)]
public class StuartHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Stuart Ability");
    }
    protected override void Initialize(){
        index = 4;
        base.Initialize();
    }
}
