using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PalacellHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //public override int rarity { get; set; } = 1;
    public override void UseAbility(){
        creature.heal(0.25f);
    }
    protected override void Initialize(){
        index = 5;
        base.Initialize();
    }
}
