using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SharkatorTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 1;
        attack = 2;
        base.Initialize();
    }
}
