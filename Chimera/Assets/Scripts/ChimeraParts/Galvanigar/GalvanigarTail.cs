using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GalvanigarTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Status_Effect fire_effect;
    protected override void Initialize(){
        attack = 1;
        attackSpeed = 1.3f;
        base.Initialize();
    }
    public override void Attack(Creature target)
    {
        target.Hit(attack, Globals.default_kb, fire_effect, true);
    }
}
