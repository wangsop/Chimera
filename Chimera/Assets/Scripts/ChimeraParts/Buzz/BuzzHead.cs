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
        ability_name = "Bulldog Bulldozer";
        ability_description = "Single-target high damage attack that leaves a poison damage-over-time effect.";
        scientist_description = "Buzz is somewhat of a \"mascot\" among researchers due to its very recognizable form and awe-inspiring colors. Naturally peace-loving creatures. Nina used to love catching them...";
        base.Initialize();
    }
}
