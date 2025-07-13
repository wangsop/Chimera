using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-100)]
public class LichenSlugBody : Body
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 0;
        health = 12;
        base.Initialize();
    }
    protected override void Update(){
        
    }
}
