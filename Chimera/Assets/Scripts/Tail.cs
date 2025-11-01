using UnityEngine;

public abstract class Tail : BodyPart
{
    protected int attack = 1;
    protected float attackSpeed = 0.7f;
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Tails[index];
    }
    public int getAttack(){
        return attack;
    }
    public float getAttackSpeed()
    {
        return attackSpeed;
    }
    public virtual void Attack(Creature target)
    {
        //if you want to implement a status effect on normal attack, do so here
        target.Hit(attack, Globals.default_kb);
    }
}
