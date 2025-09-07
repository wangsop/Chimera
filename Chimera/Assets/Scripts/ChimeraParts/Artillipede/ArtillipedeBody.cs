using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class ArtillipedeBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 6;
        health = 15;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
