using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EyeCandyHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Eye Candy Ability");
    }
    protected override void Initialize(){
        index = 3;
        base.Initialize();
    }
}
