using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NickHead : Head
{
    //public override int rarity { get; set; } = 1;
    public Status_Effect dmg_root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(creature.level * 4, Globals.default_kb, dmg_root, true);
        }
        else
        {
            Globals.energy += 10; //refund energy if no target
        }
    }
    protected override void Initialize(){
        index = 2;
        ability_name = "Suffocate";
        ability_description = "Attacks an enemy with a vine tongue that applies a “strangulation” effect that stuns the enemy for a few seconds.";
        scientist_description = "Nina named this one herself. She never did let me study it, in the end.";
        base.Initialize();
    }
}
