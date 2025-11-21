using UnityEngine;

[DefaultExecutionOrder(-100)]
public class CreedaceousHead : Head
{
    //public override int rarity { get; set; } = 1;
    public Status_Effect stun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(creature.level * 3, Globals.default_kb, stun, true);
        }
        else
        {
            Globals.energy += 10; //refund energy if no target
        }
    }
    protected override void Initialize(){
        ability_name = "Sucker Punch";
        ability_description = "Single-target KO that will stun any enemy hit by it for several seconds.";
        scientist_description = "A rather aggressive specimen, Creedaceous is a reptilian fighter with abnormally powerful arms. Exercise caution when approaching; they may take it as a challenge.";
        base.Initialize();
    }
}
