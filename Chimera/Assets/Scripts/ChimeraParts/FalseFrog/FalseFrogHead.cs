using UnityEngine;

[DefaultExecutionOrder(-100)]
public class FalseFrogHead : Head
{
    //public override int rarity { get; set; } = 1;
    public Status_Effect stun;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            if (Random.Range(0, 1) > 0.3f) //70% chance to invoke stun; otherwise, do more damage
            {
                creature.aggro.Hit(creature.level * 3, Globals.default_kb, stun, true);
            }
            else
            {
                creature.aggro.Hit(creature.level * 5, Globals.default_kb);
            }
        }
        else
        {
            Globals.energy += 10; //refund energy if no target
        }
    }
    protected override void Initialize(){
        base.Initialize();
    }
}
