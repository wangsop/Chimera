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
    //public override int rarity { get; set; } = 3;
    // params: thisCreature, radius, duration, damage per second
    public static readonly UnityEvent<Creature, int, int, int> onHorselessAbility = new();

    private Creature thisCreature;


    // ABILITY ("Puppet Master"): every enemy within a radius of 75, along with Horseless itself, becomes unable to move for 5 seconds. Every second during that time, the enemies take 1 damage.
    public override void UseAbility(){
        Debug.Log("Used Horseless Ability");
        onHorselessAbility.Invoke(thisCreature, ABILITY_RADIUS, ABILITY_DURATION, ABILITY_DAMAGE_PER_SECOND);
        //thisCreature.Hit(0, Globals.default_kb, this.freeze_effect, true);
    }
    protected override void Initialize(){
        index = 4;
        scientist_description = "I had taught tamed monsters to puppet Nina’s beloved horse plushies. She had so much fun with them that when she was gone, I threw them out, unable to watch them move and play without her.";
        thisCreature = this.GetComponentInParent<Creature>();
        base.Initialize();
    }
}
