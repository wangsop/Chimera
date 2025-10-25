using UnityEngine;

public abstract class Body : BodyPart
{
    protected int health = 10;
    protected int speed = 300;
    public int getHealth(){
        return this.health;
    }
    public int getSpeed(){ return speed; }
    protected override void Initialize(){
        //image = GameObject.Find("Main Camera").GetComponent<Globals>().Bodies[index];
    }
    public virtual int takeDamage(int damage)
    {
        return damage;
    }
}
