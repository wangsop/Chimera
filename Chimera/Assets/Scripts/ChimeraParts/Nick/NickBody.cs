using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class NickBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 2;
        health = 12;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
