using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InkyubonTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        attack = 2;
        base.Initialize();
    }
}
