using UnityEngine;

[DefaultExecutionOrder(-100)]
public class ArtillipedeTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 6;
        attack = 2;
        base.Initialize();
    }
}
