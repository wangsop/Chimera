using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SharkatorTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Status_Effect fire_effect;
    protected override void Initialize(){
        index = 1;
        attack = 2;
        attackSpeed = 1.3f;
        base.Initialize();
    }
    public override void Attack(Creature target)
    {
        target.Hit(attack, Globals.default_kb, fire_effect);
    }
}
