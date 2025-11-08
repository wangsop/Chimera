using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NickHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Nick Ability");
    }
    protected override void Initialize(){
        index = 2;
        ability_name = "Suffocate";
        ability_description = "Attacks an enemy with a vine tongue that applies a “strangulation” effect that stuns the enemy for a few seconds.";
        base.Initialize();
    }
}
