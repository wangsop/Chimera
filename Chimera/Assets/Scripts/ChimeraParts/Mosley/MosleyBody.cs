using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class MosleyBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        health = 99;
        speed = 5;
        base.Initialize();
    }
    protected override void Update(){
        
    }

    public override int takeDamage(int damage)
    {
        return 1;
    }
}
