using UnityEngine;

[DefaultExecutionOrder(-100)]
public class BuzzHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Status_Effect poison;
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(creature.level * 2, Globals.default_kb, poison, true);
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
