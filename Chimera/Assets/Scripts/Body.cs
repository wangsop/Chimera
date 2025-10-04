using UnityEngine;

public abstract class Body : BodyPart
{
    protected int health = 10;
    public int getHealth(){
        return health;
    }
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Bodies[index];
    }
    public virtual int takeDamage(int damage)
    {
        return damage;
    }
}
