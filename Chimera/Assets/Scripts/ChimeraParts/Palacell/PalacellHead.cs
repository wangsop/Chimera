using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PalacellHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Palacell Ability");
    }
    protected override void Initialize(){
        index = 5;
        base.Initialize();
    }
}
