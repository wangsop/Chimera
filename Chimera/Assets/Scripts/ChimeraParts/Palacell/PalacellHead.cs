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
        ability_name = "Cell Wall";
        ability_description = " Immediately heal for around a quarter of its health.";
        scientist_description = "Palace of plant cells… Good survival properties, though not very strong. Useful, I suppose…";
        base.Initialize();
    }
}
