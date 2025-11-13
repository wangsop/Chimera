using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CreedaceousHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Creedaceous Ability");
    }
    protected override void Initialize(){
        base.Initialize();
    }
}
