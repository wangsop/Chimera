using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class InkyubonHead : Head
{
    public override void UseAbility(){
        Debug.Log("Used Inkyubon Ability");
    }
    protected override void Initialize(){
        base.Initialize();
    }
}
