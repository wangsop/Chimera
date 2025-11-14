using UnityEngine;

public abstract class Body : BodyPart
{
    protected int health = 10;
    protected int speed = 80;
    




    public int getHealth()
    {
        return this.health;
    }
    public int getSpeed(){ return speed; }
    protected override void Initialize(){
        if (myCreature == null)
        {
            Debug.Log("null creature: body");
        }
    }
    
}
