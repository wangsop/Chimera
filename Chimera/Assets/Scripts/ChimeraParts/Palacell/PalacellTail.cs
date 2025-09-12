using UnityEngine;

[DefaultExecutionOrder(-100)]
public class PalacellTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 5;
        attack = 1;
        base.Initialize();
    }
}
