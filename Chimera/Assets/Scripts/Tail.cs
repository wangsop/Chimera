using UnityEngine;

public abstract class Tail : BodyPart
{
    protected int attack = 1;
    protected override void Initialize(){
        image = GameObject.Find("Main Camera").GetComponent<Globals>().Tails[index];
    }
    public int getAttack(){
        return attack;
    }
}
