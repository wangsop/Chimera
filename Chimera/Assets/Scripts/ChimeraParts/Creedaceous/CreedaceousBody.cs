using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class CreedaceousBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        health = 45;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
