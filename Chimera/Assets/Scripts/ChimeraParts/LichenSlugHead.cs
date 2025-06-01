using UnityEngine;

[DefaultExecutionOrder(-100)]
public class LichenSlugHead : Head
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void UseAbility(){
        Debug.Log("Used Lichen Slug Ability");
    }
    protected override void Initialize(){
        index = 0;
        base.Initialize();
    }
}
