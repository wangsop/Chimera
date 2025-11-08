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

    // ability event
    public static readonly UnityEvent<Creature, double> artillipedeAbility = new UnityEvent<Creature, double>();
    // this chimera's Creature instance
    private Creature myCreature;


    public override void UseAbility()
    {
        Debug.Log("Used Artillipede Ability");
        artillipedeAbility.Invoke(myCreature, ABILITY_DMG_PROB);
    }
    protected override void Initialize()
    {
        index = 6;
        myCreature = GetComponentInParent<Creature>();
        if (myCreature == null)
        {
            Debug.Log("Null Creature: Artillipede");
        }
        base.Initialize();
    }
}
