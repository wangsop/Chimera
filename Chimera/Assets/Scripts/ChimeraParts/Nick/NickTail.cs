using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NickTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 2;
        attack = 1;
        base.Initialize();
    }
}
