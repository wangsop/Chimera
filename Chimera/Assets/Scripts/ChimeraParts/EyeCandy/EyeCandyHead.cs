using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[DefaultExecutionOrder(-100)]
public class EyeCandyHead : Head
{
    public static readonly UnityEvent<Creature, double> onEyeCandyTriggerAggro = new(); 
    public static readonly UnityEvent<Creature> onEyeCandyTriggerDisableAggro = new();
    public static readonly UnityEvent<Creature> onEyeCandyTriggerReenableAggro = new();
    //public override int rarity { get; set; } = 2;
    [SerializeField] private static readonly double distractRadius = 200;
    [SerializeField] private static readonly float abilityActiveDuration = 5;
    [SerializeField] private static readonly float abilityDisableAggroDuration = 5;
    private Creature thisCreature;

    // broadcasts event to make all enemies within a radius attack ("distract period" starts), then starts coroutine delay, then ends the effect and makes this eyeCandy un-aggroable to the enemies that were chasing it ("escape period" starts"), then starts coroutine delay, then reverts everything to normal
    public override void UseAbility() 
    {
        Debug.Log("Used Eye Candy Ability: start distract period");
        onEyeCandyTriggerAggro.Invoke(thisCreature, distractRadius);
        StartCoroutine(DelayAbilityDisableAggro());
    }

    // coroutine that, after ability duration ends, broadcasts the event onEyeCandyTriggerDisableAggro
    private IEnumerator DelayAbilityDisableAggro()
    {
        yield return new WaitForSeconds(abilityActiveDuration);
        Debug.Log("Used Eye Candy Ability: start escape period");
        onEyeCandyTriggerDisableAggro.Invoke(thisCreature);
        StartCoroutine(DelayAbilityReenableAggro());
    }

    // coroutine that, after second "escape" part of ability duration ends, broadcasts the event onEyeCandyTriggerReenableAggro
    private IEnumerator DelayAbilityReenableAggro()
    {
        yield return new WaitForSeconds(abilityDisableAggroDuration);
        Debug.Log("Used Eye Candy Ability: end");
        onEyeCandyTriggerReenableAggro.Invoke(thisCreature);
    }

    protected override void Initialize()
    {
        index = 3;
        ability_name = "Hypnosis";
        ability_description = "Makes all nearby enemies target it for a few seconds and ignore all other chimeras.";
        scientist_description = "This unsettling creature has dangerous mind-control abilities. I've heard that those who spend too long near one will slowly go insane and begin to hallucinate themselves as an eyeball...definitely should keep Nina away.";
        thisCreature = GetComponentInParent<Creature>();
        base.Initialize();
    }
}
