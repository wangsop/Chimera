using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SharkatorHead : Head
{
    //public override int rarity { get; set; } = 1;
    public Status_Effect dmg_root;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature!= null && creature.aggro != null)
        {
            creature.aggro.Hit(creature.level * 4, Globals.default_kb, dmg_root, true);
        }
        else
        {
            Globals.energy += 10; //refund energy if no target
        }
    }
    protected override void Initialize(){
        index = 1;
        ability_name = "Jaws";
        ability_description = "Sharkator will clamp its prey in its jaws, dealing massive damage and rooting the target briefly.";
        scientist_description = "A pseudoelectronic cyborg with an open heart. You'd think the heart is an easy target, but it's a bait - attack it, and you're in for a nasty shock.";
        base.Initialize();
    }
}
