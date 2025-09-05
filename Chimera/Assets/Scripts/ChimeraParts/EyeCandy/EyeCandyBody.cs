using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class EyeCandyBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 3;
        health = 2;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
