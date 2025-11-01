using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class PalacellBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 5;
        health = 15;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
