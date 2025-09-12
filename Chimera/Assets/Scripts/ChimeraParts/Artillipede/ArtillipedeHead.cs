using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ArtillipedeHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Artillipede Ability");
    }
    protected override void Initialize(){
        index = 6;
        base.Initialize();
    }
}
