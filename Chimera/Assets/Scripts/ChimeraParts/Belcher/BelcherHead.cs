using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class BelcherHead : Head
{
    private static readonly int ABILITY_RADIUS = 75;
    private static readonly int ABILITY_DURATION = 5;
    private static readonly int ABILITY_DAMAGE_PER_SECOND = 1;
    public Status_Effect freeze_effect;
    public Status_Effect dot_effect;
    public static readonly UnityEvent<Creature, int, int, int> onHorselessAbility = new();
    //public override int rarity { get; set; } = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void UseAbility(){
        onHorselessAbility.Invoke(creature, ABILITY_RADIUS, ABILITY_DURATION, ABILITY_DAMAGE_PER_SECOND);
    }
    protected override void Initialize(){
        ability_name = "Noxious Gas";
        ability_description = "Any hostile monsters in a small radius will be poisoned and take continous damage over time.";
        scientist_description = "This toxic beast should be handled with extreme care. Nina should be kept as far as possible from these; a Belcher's acidic mucus can even melt glass...";
        base.Initialize();
    }
}
