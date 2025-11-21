using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GalvanigarHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(100, Globals.default_kb);
            if (Random.Range(0, 1) < 0.1f)
            {
                creature.Die();
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
