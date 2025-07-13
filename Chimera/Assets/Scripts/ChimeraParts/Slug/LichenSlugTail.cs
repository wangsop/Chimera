using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LichenSlugTail : Tail
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Initialize(){
        index = 0;
        attack = 1;
        base.Initialize();
    }
}
