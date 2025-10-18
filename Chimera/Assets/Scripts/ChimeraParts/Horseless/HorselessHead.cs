using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class HorselessHead : Head
{
    private static readonly int ABILITY_RADIUS = 75;
    private static readonly int ABILITY_DURATION = 5;
    private static readonly int ABILITY_DAMAGE_PER_SECOND = 1;
    public Status_Effect freeze_effect;
    public Status_Effect dot_effect;
    // params: thisCreature, radius, duration, damage per second
    public static readonly UnityEvent<Creature, int, int, int> onHorselessAbility = new();

    private Creature thisCreature;


    // ABILITY ("Puppet Master"): every enemy within a radius of 75, along with Horseless itself, becomes unable to move for 5 seconds. Every second during that time, the enemies take 1 damage.
    public override void UseAbility(){
        Debug.Log("Used Horseless Ability");
        onHorselessAbility.Invoke(thisCreature, ABILITY_RADIUS, ABILITY_DURATION, ABILITY_DAMAGE_PER_SECOND);
    }
    protected override void Initialize(){
        index = 4;
        thisCreature = this.GetComponentInParent<Creature>();
        base.Initialize();
    }
}
