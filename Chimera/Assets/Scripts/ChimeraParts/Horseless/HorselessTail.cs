using UnityEngine;

[DefaultExecutionOrder(-100)]
public class HorselessTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 4;
        attack = 2;
        base.Initialize();
    }
}
