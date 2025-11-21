using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GalvanigarHead : Head
{
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(50, Globals.default_kb);
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
        ability_name = "Short Circuit";
        ability_description = "Galvanigar will electrocute one enemy to death, dealing massive damage that almost no creature could survive. However, there is a chance it will also kill itself in the process.";
        scientist_description = "A very unpredictable creature, Galvanigars are little better than suicide bombers - guaranteed kills, with a chance of death. For my purposes, however, they are just about perfect.";
        base.Initialize();
    }
}
