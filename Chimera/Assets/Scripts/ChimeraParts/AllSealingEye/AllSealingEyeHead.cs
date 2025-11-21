using UnityEngine;

[DefaultExecutionOrder(-100)]
public class AllSealingEyeHead : Head
{
    public Status_Effect stungaze;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        if (creature != null && creature.aggro != null)
        {
            creature.aggro.Hit(0, Globals.default_kb, stungaze, true);
        }
        else
        {
            Globals.energy += 10; //refund energy if no target
        }
    }
    protected override void Initialize(){
        ability_name = "Gaze";
        ability_description = "Stuns the first enemy to make eye contact with it for 10 seconds.";
        scientist_description = "This beast possesses an abnormally long stun ability, and is rather unsettling to look at. Nina always found comfort in it, somehow, but I can't stand to be near.";
        base.Initialize();
    }
}
