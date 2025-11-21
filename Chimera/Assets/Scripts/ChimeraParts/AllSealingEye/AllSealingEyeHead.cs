using UnityEngine;

[DefaultExecutionOrder(-100)]
public class AllSealingEyeHead : Head
{
    public Status_Effect stungaze;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(0, Globals.default_kb, stungaze, true);
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
