using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class EyeCandyBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        health = 25;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
