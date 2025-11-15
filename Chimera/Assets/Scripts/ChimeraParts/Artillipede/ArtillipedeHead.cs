using UnityEngine;
using UnityEngine.Events;

/*
Ability: Reinforcement
From the time this is used until Artillipede's death, simulates
increased defense by randomly not detracting from its own health
when attacked. To balance: edit ABILITY_DMG_PROB
*/

[DefaultExecutionOrder(-100)]
public class ArtillipedeHead : Head
{
    // probability that any attack will land once ability is activated
    private static readonly double ABILITY_DMG_PROB = 0.333;

    /*
    ability event
    param 1: This Chimera's Damageable_Testing object, the one that should change its hit probability
    param 2: The new hit probability
    */
    public static readonly UnityEvent<Damageable_Testing, double> artillipedeAbility = new UnityEvent<Damageable_Testing, double>();
    // this chimera's Creature instance
    private Damageable_Testing myDamageableTesting;


    public override void UseAbility()
    {
        Debug.Log("Used Artillipede Ability");
        artillipedeAbility.Invoke(myDamageableTesting, ABILITY_DMG_PROB);
    }
    protected override void Initialize()
    {
        index = 6;
        myDamageableTesting = (Damageable_Testing) GetComponentInParent<Creature>();
        if (myDamageableTesting == null)
        {
            Debug.Log("Null Creature: Artillipede");
        }
        base.Initialize();
    }
}
