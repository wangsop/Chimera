using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[DefaultExecutionOrder(-100)]
public class EyeCandyHead : Head
{
    public static readonly UnityEvent<Creature, double> onEyeTriggerAggro = new(); 
    public static readonly UnityEvent<Creature, double> onEyeTriggerDisableAggro = new();
    [SerializeField] private static readonly double distractRadius = 200;
    [SerializeField] private static readonly float abilityDuration = 5;
    private Creature thisCreature;

    // broadcasts event to attract attention, then starts coroutine delay then end the effect
    public override void UseAbility()
    {
        Debug.Log("Used Eye Candy Ability");
        onEyeTriggerAggro.Invoke(thisCreature, distractRadius);
        StartCoroutine(DelayStopAbility());
    }

    // coroutine that, after ability duration ends, broadcasts the event onEyeTriggerEnd
    private IEnumerator DelayStopAbility()
    {
        yield return new WaitForSeconds(abilityDuration);
        onEyeTriggerDisableAggro.Invoke(thisCreature, distractRadius);
    }

    protected override void Initialize()
    {
        index = 3;
        thisCreature = GetComponentInParent<Creature>();
        base.Initialize();
    }
}
