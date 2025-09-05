using UnityEngine;

[DefaultExecutionOrder(-100)]
public class EyeCandyTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 3;
        attack = 1;
        base.Initialize();
    }
}
