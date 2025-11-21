using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class InkyubonHead : Head
{
    public GameObject baby;
    public override void UseAbility(){
        GameObject actualBaby = Instantiate(baby, creature.transform.position, Quaternion.identity);
        InkyubonBaby script = actualBaby.GetComponent<InkyubonBaby>();
        if (script != null)
        {
            script.father = this.gameObject;
        }
    }
    protected override void Initialize(){
        base.Initialize();
    }
}
